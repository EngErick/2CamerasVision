namespace TwoCamerasVision
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.camera1Combo = new System.Windows.Forms.ComboBox();
            this.pic1 = new AForge.Controls.VideoSourcePlayer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.moverMotor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.portSerial = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.texSDistancia = new System.Windows.Forms.TextBox();
            this.showDetectedObjectsButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.conCamera = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.texDistancia = new System.Windows.Forms.TextBox();
            this.tuneObjectFilterButton = new System.Windows.Forms.Button();
            this.objectDetectionCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.camera1Combo);
            this.groupBox1.Controls.Add(this.pic1);
            this.groupBox1.Location = new System.Drawing.Point(13, 160);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(920, 373);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera 1";
            // 
            // camera1Combo
            // 
            this.camera1Combo.FormattingEnabled = true;
            this.camera1Combo.Location = new System.Drawing.Point(13, 25);
            this.camera1Combo.Margin = new System.Windows.Forms.Padding(4);
            this.camera1Combo.Name = "camera1Combo";
            this.camera1Combo.Size = new System.Drawing.Size(885, 24);
            this.camera1Combo.TabIndex = 3;
            // 
            // pic1
            // 
            this.pic1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pic1.ForeColor = System.Drawing.Color.White;
            this.pic1.Location = new System.Drawing.Point(13, 62);
            this.pic1.Margin = new System.Windows.Forms.Padding(4);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(885, 298);
            this.pic1.TabIndex = 0;
            this.pic1.VideoSource = null;
            this.pic1.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.VideoSourcePlayer1_NewFrame);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.moverMotor);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.portSerial);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.texSDistancia);
            this.groupBox3.Location = new System.Drawing.Point(13, 12);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(923, 68);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cameras Control";
            // 
            // moverMotor
            // 
            this.moverMotor.Location = new System.Drawing.Point(558, 32);
            this.moverMotor.Name = "moverMotor";
            this.moverMotor.Size = new System.Drawing.Size(103, 23);
            this.moverMotor.TabIndex = 18;
            this.moverMotor.Text = "&Mover Motor";
            this.moverMotor.UseVisualStyleBackColor = true;
            this.moverMotor.Click += new System.EventHandler(this.MoverMotor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "&Porta Serial:";
            // 
            // portSerial
            // 
            this.portSerial.FormattingEnabled = true;
            this.portSerial.Location = new System.Drawing.Point(379, 30);
            this.portSerial.Name = "portSerial";
            this.portSerial.Size = new System.Drawing.Size(121, 24);
            this.portSerial.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(845, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "&Confirmar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ConfirmarConstanteDistancia_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(773, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "&Setar:";
            // 
            // texSDistancia
            // 
            this.texSDistancia.Location = new System.Drawing.Point(739, 32);
            this.texSDistancia.Name = "texSDistancia";
            this.texSDistancia.Size = new System.Drawing.Size(100, 22);
            this.texSDistancia.TabIndex = 13;
            // 
            // showDetectedObjectsButton
            // 
            this.showDetectedObjectsButton.Location = new System.Drawing.Point(433, 27);
            this.showDetectedObjectsButton.Margin = new System.Windows.Forms.Padding(4);
            this.showDetectedObjectsButton.Name = "showDetectedObjectsButton";
            this.showDetectedObjectsButton.Size = new System.Drawing.Size(185, 28);
            this.showDetectedObjectsButton.TabIndex = 10;
            this.showDetectedObjectsButton.Text = "&Mostrar Objetos Detctados";
            this.showDetectedObjectsButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.conCamera);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.texDistancia);
            this.groupBox4.Controls.Add(this.tuneObjectFilterButton);
            this.groupBox4.Controls.Add(this.objectDetectionCheck);
            this.groupBox4.Controls.Add(this.showDetectedObjectsButton);
            this.groupBox4.Location = new System.Drawing.Point(13, 86);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(923, 68);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Object Detection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(692, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "ConstanteCamera:";
            // 
            // conCamera
            // 
            this.conCamera.Location = new System.Drawing.Point(816, 43);
            this.conCamera.Name = "conCamera";
            this.conCamera.Size = new System.Drawing.Size(100, 22);
            this.conCamera.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(744, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Distancia:";
            // 
            // texDistancia
            // 
            this.texDistancia.Location = new System.Drawing.Point(816, 15);
            this.texDistancia.Name = "texDistancia";
            this.texDistancia.Size = new System.Drawing.Size(100, 22);
            this.texDistancia.TabIndex = 16;
            // 
            // tuneObjectFilterButton
            // 
            this.tuneObjectFilterButton.Location = new System.Drawing.Point(280, 26);
            this.tuneObjectFilterButton.Margin = new System.Windows.Forms.Padding(4);
            this.tuneObjectFilterButton.Name = "tuneObjectFilterButton";
            this.tuneObjectFilterButton.Size = new System.Drawing.Size(145, 28);
            this.tuneObjectFilterButton.TabIndex = 12;
            this.tuneObjectFilterButton.Text = "&Filtro de Cor";
            this.tuneObjectFilterButton.UseVisualStyleBackColor = true;
            // 
            // objectDetectionCheck
            // 
            this.objectDetectionCheck.AutoSize = true;
            this.objectDetectionCheck.Location = new System.Drawing.Point(13, 31);
            this.objectDetectionCheck.Margin = new System.Windows.Forms.Padding(4);
            this.objectDetectionCheck.Name = "objectDetectionCheck";
            this.objectDetectionCheck.Size = new System.Drawing.Size(183, 20);
            this.objectDetectionCheck.TabIndex = 11;
            this.objectDetectionCheck.Text = "Ligar Detecção de Objeto";
            this.objectDetectionCheck.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 544);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Visão Duas Cameras";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox camera1Combo;
        private AForge.Controls.VideoSourcePlayer pic1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button showDetectedObjectsButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button tuneObjectFilterButton;
        private System.Windows.Forms.CheckBox objectDetectionCheck;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox texSDistancia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox texDistancia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox portSerial;
        private System.Windows.Forms.Button moverMotor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox conCamera;
    }
}

