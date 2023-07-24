namespace LagEngine3D
{
    partial class RenderView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FramerateTimer = new System.Windows.Forms.Timer(this.components);
            this.InfoText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FramerateTimer
            // 
            this.FramerateTimer.Enabled = true;
            this.FramerateTimer.Interval = 17;
            this.FramerateTimer.Tick += new System.EventHandler(this.FramerateTimer_Tick);
            // 
            // InfoText
            // 
            this.InfoText.AutoSize = true;
            this.InfoText.BackColor = System.Drawing.SystemColors.Control;
            this.InfoText.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InfoText.ForeColor = System.Drawing.Color.Green;
            this.InfoText.Location = new System.Drawing.Point(12, 9);
            this.InfoText.Name = "InfoText";
            this.InfoText.Size = new System.Drawing.Size(64, 28);
            this.InfoText.TabIndex = 0;
            this.InfoText.Text = "Info";
            // 
            // RenderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.InfoText);
            this.Name = "RenderView";
            this.Text = "Render View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RenderView_FormClosed);
            this.Load += new System.EventHandler(this.RenderView_Load);
            this.SizeChanged += new System.EventHandler(this.RenderView_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer FramerateTimer;
        private Label InfoText;
    }
}