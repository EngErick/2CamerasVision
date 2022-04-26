// Two Cameras Vision
//
// Copyright © Andrew Kirillov, 2009
// andrew.kirillov@aforgenet.com
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;

namespace TwoCamerasVision
{
    public partial class MainForm : Form
    {
        #region Propriedade   
        public SerialMetodos MetodosSerial { get; set; }
        public Calculos Calculos { get; set; }
        public string DistanciaText { get; set; }

        #endregion

        #region Variaveis
        // Lista Dispositivos de Video
        FilterInfoCollection VideoDevices;
        //Filtro das Cores
        TuneObjectFilterForm TuneObjectFilterForm;

        ColorFiltering ColorFilter = new ColorFiltering();
        GrayscaleBT709 GrayFilter = new GrayscaleBT709();
        // Cria dois objetos para processamento simultaneo
        BlobCounter BlobCounter1 = new BlobCounter();
        BlobCounter BlobCounter2 = new BlobCounter();

        private AutoResetEvent camera1Acquired = null;
        private Thread trackingThread = null;

        // Cordenadas do Objeto em Ambas as Cameras (Ponto Medio, Extremidade Esquerda, Extremidade Direita
        private float x1m, y1m, x1e, x1d;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            objectDetectionCheck.Checked = true;
            Calculos = new Calculos();

            var postas = SerialPort.GetPortNames();
            foreach (var post in postas)
            {
                portSerial.Items.Add(post);
            }

            

            // Mostra Lista Dispositivos 
            try
            {
                // Enumera lista de Dispositivos
                VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (VideoDevices.Count == 0)
                {
                    throw new Exception();
                }

                for (int i = 1, n = VideoDevices.Count; i <= n; i++)
                {
                    string cameraName = i + " : " + VideoDevices[i - 1].Name;

                    camera1Combo.Items.Add(cameraName);
                }
                camera1Combo.SelectedIndex = 0;
            }
            catch
            {                
                camera1Combo.Items.Add("No cameras found");

                camera1Combo.SelectedIndex = 0;

                camera1Combo.Enabled = false;
            }
            camera1Combo.SelectedIndex = 1;

            ColorFilter.Red = new IntRange(145, 255);
            ColorFilter.Green = new IntRange(75, 145);
            ColorFilter.Blue = new IntRange(75, 145);

            // configura o contorno do Objeto
            BlobCounter1.MinWidth = 25;
            BlobCounter1.MinHeight = 25;
            BlobCounter1.FilterBlobs = true;
            BlobCounter1.ObjectsOrder = ObjectsOrder.Size;

            BlobCounter2.MinWidth = 25;
            BlobCounter2.MinHeight = 25;
            BlobCounter2.FilterBlobs = true;
            BlobCounter2.ObjectsOrder = ObjectsOrder.Size;

            // Cria fonte do video
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(VideoDevices[camera1Combo.SelectedIndex].MonikerString)
            {
                DesiredFrameRate = 10
            };

            pic1.VideoSource = videoSource1;
            pic1.Start();


            camera1Acquired = new AutoResetEvent(false);

            if (TuneObjectFilterForm == null)
            {
                TuneObjectFilterForm = new TuneObjectFilterForm();
                TuneObjectFilterForm.OnFilterUpdate += new EventHandler(TuneObjectFilterForm_OnFilterUpdate);

                TuneObjectFilterForm.RedRange = ColorFilter.Red;
                TuneObjectFilterForm.GreenRange = ColorFilter.Green;
                TuneObjectFilterForm.BlueRange = ColorFilter.Blue;
            }
            TuneObjectFilterForm.Show();




        }


        #region Eventos
        // Atualiza cores do Filtro
        private void TuneObjectFilterForm_OnFilterUpdate(object sender, EventArgs eventArgs)
        {
            ColorFilter.Red = TuneObjectFilterForm.RedRange;
            ColorFilter.Green = TuneObjectFilterForm.GreenRange;
            ColorFilter.Blue = TuneObjectFilterForm.BlueRange;
        }
        private void ConfirmarConstanteDistancia_Click(object sender, EventArgs e)
        {
            if (texSDistancia.Text == null || texSDistancia.Text == "")
            {

            }
            else
            {
                conCamera.Text = Calculos.CalConstante(float.Parse(texSDistancia.Text)).ToString();                
            }
        }

        //Atualiza a distancia do objeto a cada 1 segundo
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(DistanciaText == "+Infinito" || DistanciaText == "-Infinito")
            {
                texDistancia.Text = "Objeto Perdido";
            }
            else
            {
                texDistancia.Text = DistanciaText;
            }

            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        //Envia serial para o esp32
        private void MoverMotor_Click(object sender, EventArgs e)
        {
            //inicia a serial 
            MetodosSerial = new SerialMetodos(portSerial.Text, 11520);
            //Comunica'
            MetodosSerial._serialPort.Write("go");
            MetodosSerial._serialPort.Close();
        }

        // Recebe frame do video
        private void VideoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            if (objectDetectionCheck.Checked)
            {
                Bitmap objectImage1 = ColorFilter.Apply(image);
                Bitmap objectImage2 = ColorFilter.Apply(image);

                //divide a imagem da camera em duas para o processamento
                BitmapData objectData1 = objectImage1.LockBits(new Rectangle(0, 0, image.Width / 2, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
                BitmapData objectData2 = objectImage2.LockBits(new Rectangle(image.Width / 2, 0, image.Width / 2, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

                objectImage1.UnlockBits(objectData1);
                objectImage2.UnlockBits(objectData2);

                //binariza imagem do video
                UnmanagedImage grayImage1 = GrayFilter.Apply(new UnmanagedImage(objectData1));
                UnmanagedImage grayImage2 = GrayFilter.Apply(new UnmanagedImage(objectData2));


                BlobCounter1.ProcessImage(grayImage1);
                //Desenha e define o objeto da camera 2
                var xy1 = DesenhaQuadrado(image, objectImage1, 0, BlobCounter1);
                //Define os pontos do objeto da camera 2
                Calculos.X1Meio = xy1[0];
                Calculos.X1Esquerda = xy1[1];
                Calculos.X1Direita = xy1[2];
                Calculos.Y1 = xy1[3];




                BlobCounter2.ProcessImage(grayImage2);
                //Desenha e define o objeto da camera 2
                var xy2 = DesenhaQuadrado(image, objectImage2, image.Width / 2, BlobCounter2);
                //Define os pontos do objeto da camera 2
                Calculos.X2Meio = xy2[0];
                Calculos.X2Esquerda = xy2[1];
                Calculos.X2Direita = xy2[2];
                Calculos.Y2 = xy2[3];


                if (Calculos.ConstanteCamera != 0)
                {
                    Calculos.ListaDistancia.Add(Calculos.CalDistancia());
                    if (Calculos.ListaDistancia.Count == 10)
                    {
                        float distancias = 0;
                        foreach (var item in Calculos.ListaDistancia)
                        {
                            distancias += item;

                        }

                        distancias /= 10;
                        try
                        {
                            DistanciaText = distancias.ToString();
                        }
                        catch(Exception ex) 
                        {
                        
                        }
                        
                        Console.WriteLine(distancias);
                        Calculos.ListaDistancia = new List<float>();
                    }
                }
            }
        }

       

        public List<float> DesenhaQuadrado(Bitmap image, Bitmap objectImage, int x, BlobCounter blobCounter)
        {
            Rectangle[] rects = blobCounter.GetObjectsRectangles();

            if (rects.Length > 0)
            {
                Rectangle objectRect = rects[0];

                // desenha retangulo envolta do objeto
                Graphics g = Graphics.FromImage(image);

                using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 3))
                {
                    var posicao = new Point(x, 0);
                    objectRect.Offset(posicao);
                    g.DrawRectangle(pen, objectRect);
                }

                g.Dispose();
                lock (this)
                {
                    var metadeImagem = objectImage.Width / 2;

                    if (objectRect.Left < (metadeImagem) && objectRect.Right < (metadeImagem))
                    {

                        x1e = objectRect.Left;
                        x1d = objectRect.Right;
                        x1m = (x1e + x1d) / 2;
                        y1m = (objectImage.Height - (objectRect.Top + objectRect.Bottom)) / 2;
                    }
                    else if ((objectRect.Left >= (metadeImagem) && objectRect.Right < (metadeImagem)) || (objectRect.Left < (metadeImagem) && objectRect.Right >= (metadeImagem)))
                    {

                    }
                    else
                    {
                        x1e = (objectRect.Left - metadeImagem);
                        x1d = (objectRect.Right - metadeImagem);

                        x1m = (x1e + x1d) / 2;
                        y1m = (objectImage.Height - (objectRect.Top + objectRect.Bottom)) / 2;

                    }

                    if (x1e < 0) x1m *= -1;
                    if (x1d < 0) x1m *= -1;
                    if (x1m < 0) x1m *= -1;
                    if (y1m < 0) y1m *= -1;
                    camera1Acquired.Set();


                }
            }

            return new List<float> { x1m, x1e, x1d, y1m };
        }
        #endregion

    }
}
