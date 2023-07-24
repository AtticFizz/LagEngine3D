namespace LagEngine3D
{
    partial class DebugView
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
            this.DebugText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DebugText
            // 
            this.DebugText.AutoSize = true;
            this.DebugText.BackColor = System.Drawing.Color.Black;
            this.DebugText.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DebugText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.DebugText.Location = new System.Drawing.Point(0, 0);
            this.DebugText.Name = "DebugText";
            this.DebugText.Size = new System.Drawing.Size(54, 19);
            this.DebugText.TabIndex = 0;
            this.DebugText.Text = "RenderDebug";
            // 
            // DebugView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.DebugText);
            this.Name = "DebugView";
            this.Text = "DebugView";
            this.Load += new System.EventHandler(this.DebugView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label DebugText;
    }
}