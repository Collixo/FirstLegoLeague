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
            ((System.ComponentModel.ISupportInitialize)(this.backwardIndi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftIndi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightIndi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forwardIndi)).BeginInit();
            this.SuspendLayout();
            // 
            // backwardIndi
            // 
            this.backwardIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowDown;
            this.backwardIndi.Location = new System.Drawing.Point(86, 191);
            this.backwardIndi.Name = "backwardIndi";
            this.backwardIndi.Size = new System.Drawing.Size(100, 90);
            this.backwardIndi.TabIndex = 3;
            this.backwardIndi.TabStop = false;
            this.backwardIndi.Visible = false;
            // 
            // leftIndi
            // 
            this.leftIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowLeft;
            this.leftIndi.Location = new System.Drawing.Point(-5, 102);
            this.leftIndi.Name = "leftIndi";
            this.leftIndi.Size = new System.Drawing.Size(97, 84);
            this.leftIndi.TabIndex = 2;
            this.leftIndi.TabStop = false;
            this.leftIndi.Visible = false;
            // 
            // rightIndi
            // 
            this.rightIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowFixedRight;
            this.rightIndi.Location = new System.Drawing.Point(183, 102);
            this.rightIndi.Name = "rightIndi";
            this.rightIndi.Size = new System.Drawing.Size(118, 73);
            this.rightIndi.TabIndex = 1;
            this.rightIndi.TabStop = false;
            this.rightIndi.Visible = false;
            // 
            // forwardIndi
            // 
            this.forwardIndi.Image = global::RemoteControlServer.Properties.Resources.AlphaArrowUp;
            this.forwardIndi.Location = new System.Drawing.Point(86, 12);
            this.forwardIndi.Name = "forwardIndi";
            this.forwardIndi.Size = new System.Drawing.Size(91, 92);
            this.forwardIndi.TabIndex = 0;
            this.forwardIndi.TabStop = false;
            this.forwardIndi.Visible = false;
            // 
            // RemoteControlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 287);
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

        }

        #endregion

        private System.Windows.Forms.PictureBox forwardIndi;
        private System.Windows.Forms.PictureBox rightIndi;
        private System.Windows.Forms.PictureBox leftIndi;
        private System.Windows.Forms.PictureBox backwardIndi;
    }
}

