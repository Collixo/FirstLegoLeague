namespace RemoteControlServer
{
    partial class RemoteControlServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backwardIndi = new System.Windows.Forms.PictureBox();
            this.leftIndi = new System.Windows.Forms.PictureBox();
            this.rightIndi = new System.Windows.Forms.PictureBox();
            this.forwardIndi = new System.Windows.Forms.PictureBox();
            this.grabbingLabel = new System.Windows.Forms.Label();
            this.releasingLabel = new System.Windows.Forms.Label();
            this.repeatButton = new System.Windows.Forms.Button();
            this.repeatLabel = new System.Windows.Forms.Label();
            this.initButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.backwardIndi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftIndi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightIndi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardIndi)).BeginInit();
            this.SuspendLayout();
            // 
            // backwardIndi
            // 
            this.backwardIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowDown;
            this.backwardIndi.Location = new System.Drawing.Point(110, 205);
            this.backwardIndi.Name = "backwardIndi";
            this.backwardIndi.Size = new System.Drawing.Size(100, 90);
            this.backwardIndi.TabIndex = 3;
            this.backwardIndi.TabStop = false;
            this.backwardIndi.Visible = false;
            // 
            // leftIndi
            // 
            this.leftIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowLeft;
            this.leftIndi.Location = new System.Drawing.Point(19, 116);
            this.leftIndi.Name = "leftIndi";
            this.leftIndi.Size = new System.Drawing.Size(97, 84);
            this.leftIndi.TabIndex = 2;
            this.leftIndi.TabStop = false;
            this.leftIndi.Visible = false;
            // 
            // rightIndi
            // 
            this.rightIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowFixedRight;
            this.rightIndi.Location = new System.Drawing.Point(207, 116);
            this.rightIndi.Name = "rightIndi";
            this.rightIndi.Size = new System.Drawing.Size(118, 73);
            this.rightIndi.TabIndex = 1;
            this.rightIndi.TabStop = false;
            this.rightIndi.Visible = false;
            // 
            // forwardIndi
            // 
            this.forwardIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowUp;
            this.forwardIndi.Location = new System.Drawing.Point(110, 26);
            this.forwardIndi.Name = "forwardIndi";
            this.forwardIndi.Size = new System.Drawing.Size(91, 92);
            this.forwardIndi.TabIndex = 0;
            this.forwardIndi.TabStop = false;
            this.forwardIndi.Visible = false;
            // 
            // grabbingLabel
            // 
            this.grabbingLabel.AutoSize = true;
            this.grabbingLabel.Location = new System.Drawing.Point(19, 26);
            this.grabbingLabel.Name = "grabbingLabel";
            this.grabbingLabel.Size = new System.Drawing.Size(67, 17);
            this.grabbingLabel.TabIndex = 4;
            this.grabbingLabel.Text = "Grabbing";
            this.grabbingLabel.Visible = false;
            // 
            // releasingLabel
            // 
            this.releasingLabel.AutoSize = true;
            this.releasingLabel.Location = new System.Drawing.Point(208, 26);
            this.releasingLabel.Name = "releasingLabel";
            this.releasingLabel.Size = new System.Drawing.Size(71, 17);
            this.releasingLabel.TabIndex = 5;
            this.releasingLabel.Text = "Releasing";
            this.releasingLabel.Visible = false;
            // 
            // repeatButton
            // 
            this.repeatButton.Location = new System.Drawing.Point(217, 205);
            this.repeatButton.Name = "repeatButton";
            this.repeatButton.Size = new System.Drawing.Size(96, 90);
            this.repeatButton.TabIndex = 6;
            this.repeatButton.Text = "File found, repeat now!";
            this.repeatButton.UseVisualStyleBackColor = true;
            this.repeatButton.Visible = false;
            this.repeatButton.Click += new System.EventHandler(this.repeatButton_Click);
            // 
            // repeatLabel
            // 
            this.repeatLabel.AutoSize = true;
            this.repeatLabel.Location = new System.Drawing.Point(190, 298);
            this.repeatLabel.Name = "repeatLabel";
            this.repeatLabel.Size = new System.Drawing.Size(135, 17);
            this.repeatLabel.TabIndex = 7;
            this.repeatLabel.Text = "No source file found";
            this.repeatLabel.Visible = false;
            // 
            // initButton
            // 
            this.initButton.Location = new System.Drawing.Point(19, 225);
            this.initButton.Name = "initButton";
            this.initButton.Size = new System.Drawing.Size(85, 70);
            this.initButton.TabIndex = 8;
            this.initButton.Text = "init";
            this.initButton.UseVisualStyleBackColor = true;
            this.initButton.Click += new System.EventHandler(this.initButton_Click);
            // 
            // RemoteControlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 315);
            this.Controls.Add(this.initButton);
            this.Controls.Add(this.repeatLabel);
            this.Controls.Add(this.repeatButton);
            this.Controls.Add(this.releasingLabel);
            this.Controls.Add(this.grabbingLabel);
            this.Controls.Add(this.backwardIndi);
            this.Controls.Add(this.leftIndi);
            this.Controls.Add(this.rightIndi);
            this.Controls.Add(this.forwardIndi);
            this.Name = "RemoteControlServer";
            this.Text = "RemoteControlServer";
            this.Load += new System.EventHandler(this.RemoteControlServer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.backwardIndi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftIndi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightIndi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardIndi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox forwardIndi;
        private System.Windows.Forms.PictureBox rightIndi;
        private System.Windows.Forms.PictureBox leftIndi;
        private System.Windows.Forms.PictureBox backwardIndi;
        private System.Windows.Forms.Label grabbingLabel;
        private System.Windows.Forms.Label releasingLabel;
        private System.Windows.Forms.Button repeatButton;
        private System.Windows.Forms.Label repeatLabel;
        private System.Windows.Forms.Button initButton;
    }
}

