
namespace UI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.SetupPicture = new System.Windows.Forms.PictureBox();
            this.SavePicture = new System.Windows.Forms.PictureBox();
            this.CreatePicture = new System.Windows.Forms.PictureBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.DescriptLabel = new System.Windows.Forms.Label();
            this.UpDownLabel = new System.Windows.Forms.Label();
            this.FillPanel = new System.Windows.Forms.Panel();
            this.NumberPictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NumberTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.LeftArrowTimer = new System.Windows.Forms.Timer(this.components);
            this.RightArrowTimer = new System.Windows.Forms.Timer(this.components);
            this.LeftArrowButton = new UI.ArrowButton();
            this.RightArrowButton = new UI.ArrowButton();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetupPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SavePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreatePicture)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.FillPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.White;
            this.LeftPanel.Controls.Add(this.SetupPicture);
            this.LeftPanel.Controls.Add(this.SavePicture);
            this.LeftPanel.Controls.Add(this.CreatePicture);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(83, 418);
            this.LeftPanel.TabIndex = 1;
            // 
            // SetupPicture
            // 
            this.SetupPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SetupPicture.Location = new System.Drawing.Point(13, 171);
            this.SetupPicture.Name = "SetupPicture";
            this.SetupPicture.Size = new System.Drawing.Size(55, 55);
            this.SetupPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SetupPicture.TabIndex = 2;
            this.SetupPicture.TabStop = false;
            this.SetupPicture.MouseEnter += new System.EventHandler(this.SetupPicture_MouseEnter);
            this.SetupPicture.MouseLeave += new System.EventHandler(this.SetupPicture_MouseLeave);
            // 
            // SavePicture
            // 
            this.SavePicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePicture.Location = new System.Drawing.Point(13, 95);
            this.SavePicture.Name = "SavePicture";
            this.SavePicture.Size = new System.Drawing.Size(55, 55);
            this.SavePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SavePicture.TabIndex = 1;
            this.SavePicture.TabStop = false;
            this.SavePicture.MouseEnter += new System.EventHandler(this.SavePicture_MouseEnter);
            this.SavePicture.MouseLeave += new System.EventHandler(this.SavePicture_MouseLeave);
            // 
            // CreatePicture
            // 
            this.CreatePicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreatePicture.Location = new System.Drawing.Point(13, 20);
            this.CreatePicture.Name = "CreatePicture";
            this.CreatePicture.Size = new System.Drawing.Size(55, 55);
            this.CreatePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CreatePicture.TabIndex = 0;
            this.CreatePicture.TabStop = false;
            this.CreatePicture.Click += new System.EventHandler(this.CreatePicture_Click);
            this.CreatePicture.MouseEnter += new System.EventHandler(this.CreatePicture_MouseEnter);
            this.CreatePicture.MouseLeave += new System.EventHandler(this.CreatePicture_MouseLeave);
            // 
            // TopPanel
            // 
            this.TopPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(106)))), ((int)(((byte)(153)))));
            this.TopPanel.Controls.Add(this.LeftArrowButton);
            this.TopPanel.Controls.Add(this.RightArrowButton);
            this.TopPanel.Controls.Add(this.HeaderPanel);
            this.TopPanel.Controls.Add(this.DescriptLabel);
            this.TopPanel.Controls.Add(this.UpDownLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(83, 0);
            this.TopPanel.MaximumSize = new System.Drawing.Size(0, 200);
            this.TopPanel.MinimumSize = new System.Drawing.Size(0, 90);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(633, 123);
            this.TopPanel.TabIndex = 2;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.HeaderPanel.AutoSize = true;
            this.HeaderPanel.BackColor = System.Drawing.Color.Transparent;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(235, 88);
            this.HeaderPanel.TabIndex = 14;
            // 
            // DescriptLabel
            // 
            this.DescriptLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(77)))), ((int)(((byte)(116)))));
            this.DescriptLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DescriptLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.DescriptLabel.Location = new System.Drawing.Point(0, 88);
            this.DescriptLabel.Name = "DescriptLabel";
            this.DescriptLabel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.DescriptLabel.Size = new System.Drawing.Size(633, 30);
            this.DescriptLabel.TabIndex = 0;
            // 
            // UpDownLabel
            // 
            this.UpDownLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.UpDownLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.UpDownLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UpDownLabel.Location = new System.Drawing.Point(0, 118);
            this.UpDownLabel.Name = "UpDownLabel";
            this.UpDownLabel.Size = new System.Drawing.Size(633, 5);
            this.UpDownLabel.TabIndex = 11;
            // 
            // FillPanel
            // 
            this.FillPanel.BackColor = System.Drawing.Color.Transparent;
            this.FillPanel.Controls.Add(this.NumberPictureBox);
            this.FillPanel.Controls.Add(this.label4);
            this.FillPanel.Controls.Add(this.label3);
            this.FillPanel.Controls.Add(this.NumberTrackBar);
            this.FillPanel.Controls.Add(this.label1);
            this.FillPanel.Controls.Add(this.ColorLabel);
            this.FillPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FillPanel.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.FillPanel.Location = new System.Drawing.Point(83, 123);
            this.FillPanel.Name = "FillPanel";
            this.FillPanel.Size = new System.Drawing.Size(633, 295);
            this.FillPanel.TabIndex = 3;
            this.FillPanel.Visible = false;
            // 
            // NumberPictureBox
            // 
            this.NumberPictureBox.Image = global::UI.Properties.Resources.Numbers1;
            this.NumberPictureBox.Location = new System.Drawing.Point(4, 217);
            this.NumberPictureBox.Name = "NumberPictureBox";
            this.NumberPictureBox.Size = new System.Drawing.Size(137, 20);
            this.NumberPictureBox.TabIndex = 7;
            this.NumberPictureBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(-4, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Палитра";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(-4, 164);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Размер";
            // 
            // NumberTrackBar
            // 
            this.NumberTrackBar.AutoSize = false;
            this.NumberTrackBar.Location = new System.Drawing.Point(0, 187);
            this.NumberTrackBar.Maximum = 3;
            this.NumberTrackBar.Name = "NumberTrackBar";
            this.NumberTrackBar.Size = new System.Drawing.Size(147, 30);
            this.NumberTrackBar.TabIndex = 3;
            this.NumberTrackBar.Value = 1;
            this.NumberTrackBar.ValueChanged += new System.EventHandler(this.NumberTrackBar_ValueChanged);
            this.NumberTrackBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NumberTrackBar_KeyUp);
            this.NumberTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NumberTrackBar_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(200, 17);
            this.label1.MaximumSize = new System.Drawing.Size(430, 0);
            this.label1.MinimumSize = new System.Drawing.Size(150, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 200);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // ColorLabel
            // 
            this.ColorLabel.BackColor = System.Drawing.Color.Transparent;
            this.ColorLabel.Location = new System.Drawing.Point(0, 35);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(110, 83);
            this.ColorLabel.TabIndex = 1;
            // 
            // LeftArrowTimer
            // 
            this.LeftArrowTimer.Enabled = true;
            this.LeftArrowTimer.Tick += new System.EventHandler(this.LeftArrowTimer_Tick);
            // 
            // RightArrowTimer
            // 
            this.RightArrowTimer.Enabled = true;
            this.RightArrowTimer.Tick += new System.EventHandler(this.RightArrowTimer_Tick);
            // 
            // LeftArrowButton
            // 
            this.LeftArrowButton.Arrow = UI.ArrowButton.Ar.Left;
            this.LeftArrowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LeftArrowButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftArrowButton.Location = new System.Drawing.Point(0, 0);
            this.LeftArrowButton.Name = "LeftArrowButton";
            this.LeftArrowButton.Size = new System.Drawing.Size(22, 88);
            this.LeftArrowButton.TabIndex = 15;
            this.LeftArrowButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftArrowButton_MouseDown);
            this.LeftArrowButton.MouseEnter += new System.EventHandler(this.LeftArrowButton_MouseEnter);
            this.LeftArrowButton.MouseLeave += new System.EventHandler(this.LeftArrowButton_MouseLeave);
            this.LeftArrowButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LeftArrowButton_MouseMove);
            // 
            // RightArrowButton
            // 
            this.RightArrowButton.Arrow = UI.ArrowButton.Ar.Right;
            this.RightArrowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.RightArrowButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightArrowButton.Location = new System.Drawing.Point(611, 0);
            this.RightArrowButton.Name = "RightArrowButton";
            this.RightArrowButton.Size = new System.Drawing.Size(22, 88);
            this.RightArrowButton.TabIndex = 16;
            this.RightArrowButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightArrowButton_MouseDown);
            this.RightArrowButton.MouseEnter += new System.EventHandler(this.RightArrowButton_MouseEnter);
            this.RightArrowButton.MouseLeave += new System.EventHandler(this.RightArrowButton_MouseLeave);
            this.RightArrowButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RightArrowButton_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(716, 418);
            this.Controls.Add(this.FillPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.LeftPanel);
            this.Name = "MainForm";
            this.Text = "Just Shell2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.LeftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SetupPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SavePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreatePicture)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.FillPanel.ResumeLayout(false);
            this.FillPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.PictureBox SetupPicture;
        private System.Windows.Forms.PictureBox SavePicture;
        private System.Windows.Forms.PictureBox CreatePicture;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label DescriptLabel;
        private System.Windows.Forms.Label UpDownLabel;
        private System.Windows.Forms.Panel FillPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar NumberTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ColorLabel;
        private ArrowButton RightArrowButton;
        private ArrowButton LeftArrowButton;
        private System.Windows.Forms.Timer LeftArrowTimer;
        private System.Windows.Forms.Timer RightArrowTimer;
        private System.Windows.Forms.PictureBox NumberPictureBox;
    }
}

