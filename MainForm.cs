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
        public SerialMetodos metodosSerial { get; set; }
        public Calculos calculos { get; set; }      
        
        #endregion

        #region Variaveis
        // list of video devices
        FilterInfoCollection videoDevices;
        // form to show detected objects
        //DetectedObjectsForm detectedObjectsForm;
        // form to tune object detection filter
        TuneObjectFilterForm tuneObjectFilterForm;

        ColorFiltering colorFilter = new ColorFiltering();
        GrayscaleBT709 grayFilter = new GrayscaleBT709();
        // use two blob counters, so the could run in parallel in two threads
        BlobCounter blobCounter1 = new BlobCounter();
        BlobCounter blobCounter2 = new BlobCounter();

        private AutoResetEvent camera1Acquired = null;
        // private AutoResetEvent camera2Acquired = null;
        private Thread trackingThread = null;

        // object coordinates in both cameras
        private float x1m, y1m, x1e, x1d;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            calculos= new Calculos();

            var postas = SerialPort.GetPortNames();
            foreach (var post in postas)
            {
                portSerial.Items.Add(post);
            }

            

            // show device list
            try
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    throw new Exception();
                }

                for (int i = 1, n = videoDevices.Count; i <= n; i++)
                {
                    string cameraName = i + " : " + videoDevices[i - 1].Name;

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

            //
            colorFilter.Red = new IntRange(0, 255);
            colorFilter.Green = new IntRange(0, 80);
            colorFilter.Blue = new IntRange(0, 80);

            // configure blob counters
            blobCounter1.MinWidth = 25;
            blobCounter1.MinHeight = 25;
            blobCounter1.FilterBlobs = true;
            blobCounter1.ObjectsOrder = ObjectsOrder.Size;

            blobCounter2.MinWidth = 25;
            blobCounter2.MinHeight = 25;
            blobCounter2.FilterBlobs = true;
            blobCounter2.ObjectsOrder = ObjectsOrder.Size;

            // create first video source
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[camera1Combo.SelectedIndex].MonikerString);
            videoSource1.DesiredFrameRate = 10;

            pic1.VideoSource = videoSource1;
            pic1.Start();


            camera1Acquired = new AutoResetEvent(false);

            if (tuneObjectFilterForm == null)
            {
                tuneObjectFilterForm = new TuneObjectFilterForm();
                tuneObjectFilterForm.OnFilterUpdate += new EventHandler(tuneObjectFilterForm_OnFilterUpdate);

                tuneObjectFilterForm.RedRange = colorFilter.Red;
                tuneObjectFilterForm.GreenRange = colorFilter.Green;
                tuneObjectFilterForm.BlueRange = colorFilter.Blue;
            }
            tuneObjectFilterForm.Show();
        }


        #region Eventos
        // Main form closing - stop cameras
        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    StopCameras();
        //}

        // On "Tune Object Filter" button click - show filter tuning dialog
     

        // Object filter properties are updated
        private void tuneObjectFilterForm_OnFilterUpdate(object sender, EventArgs eventArgs)
        {
            colorFilter.Red = tuneObjectFilterForm.RedRange;
            colorFilter.Green = tuneObjectFilterForm.GreenRange;
            colorFilter.Blue = tuneObjectFilterForm.BlueRange;
        }

        // Turn on/off object detection



        //-------------------------------------------------------


        private void button1_Click(object sender, EventArgs e)
        {
            if (texSDistancia.Text == null || texSDistancia.Text == "")
            {

            }
            else
            {
                conCamera.Text = calculos.calConstante(float.Parse(texSDistancia.Text)).ToString();                
            }
        }


        private void moverMotor_Click(object sender, EventArgs e)
        {
            //inicia a serial 
            metodosSerial = new SerialMetodos(portSerial.Text, 11520);
            //Comunica'
            metodosSerial._serialPort.Write("go");
            metodosSerial._serialPort.Close();
        }

        // received frame from the 1st camera
        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            if (objectDetectionCheck.Checked)
            {
                Bitmap objectImage1 = colorFilter.Apply(image);
                Bitmap objectImage2 = colorFilter.Apply(image);

                BitmapData objectData1 = objectImage1.LockBits(new Rectangle(0, 0, image.Width / 2, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
                BitmapData objectData2 = objectImage2.LockBits(new Rectangle(image.Width / 2, 0, image.Width / 2, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);

                objectImage1.UnlockBits(objectData1);
                objectImage2.UnlockBits(objectData2);

                UnmanagedImage grayImage1 = grayFilter.Apply(new UnmanagedImage(objectData1));
                UnmanagedImage grayImage2 = grayFilter.Apply(new UnmanagedImage(objectData2));


                blobCounter1.ProcessImage(grayImage1);
                var xy1 = desenhaQuadrado(image, objectImage1, 0, blobCounter1, image.Width / 2);
                X1Meio = xy1[0];
                X1Esquerda = xy1[1];
                X1Direita = xy1[2];
                Y1 = xy1[3];
                //Console.WriteLine("X1meio: " + X1Meio + " X1Esquerda: " + X1Esquerda + " X1Direta: " + X1Direita);




                blobCounter2.ProcessImage(grayImage2);
                var xy2 = desenhaQuadrado(image, objectImage2, image.Width / 2, blobCounter2, 0);
                X2Meio = xy2[0];
                X2Esquerda = xy2[1];
                X2Direita = xy2[2];
                Y2 = xy2[3];

                //Console.WriteLine("X2meio: " + X2Meio + " X2Esquerda: " + X2Esquerda + " X2Direta: " + X2Direita);

                if (calculos.ConstanteCamera != 0)
                {
                    listaDistancia.Add(calculos.calDistancia());
                    if (listaDistancia.Count == 10)
                    {
                        float distancias = 0;
                        foreach (var item in listaDistancia)
                        {
                            distancias += item;

                        }

                        distancias /= 10;
                        Console.WriteLine(distancias);
                        listaDistancia = new List<float>();
                    }
                }
            }
        }
        public List<float> desenhaQuadrado(Bitmap image, Bitmap objectImage, int x, BlobCounter blobCounter, int xdireita)
        {
            Rectangle[] rects = blobCounter.GetObjectsRectangles();

            if (rects.Length > 0)
            {
                Rectangle objectRect = rects[0];

                // draw rectangle around derected object
                Graphics g = Graphics.FromImage(image);

                using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 3))
                {
                    var posicao = new Point(x, 0);
                    objectRect.Offset(posicao);
                    g.DrawRectangle(pen, objectRect);
                }

                g.Dispose();
                // get object's center coordinates relative to image center
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
