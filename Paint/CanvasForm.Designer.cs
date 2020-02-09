namespace Paint
{
    partial class CanvasForm
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
            this.components = new System.ComponentModel.Container();
            this.CanvasPictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.негативToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.CanvasPictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CanvasPictureBox
            // 
            this.CanvasPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CanvasPictureBox.Location = new System.Drawing.Point(0, 0);
            this.CanvasPictureBox.Name = "CanvasPictureBox";
            this.CanvasPictureBox.Size = new System.Drawing.Size(800, 450);
            this.CanvasPictureBox.TabIndex = 0;
            this.CanvasPictureBox.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьToolStripMenuItem,
            this.негативToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 52);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.ВставитьToolStripMenuItem_Click);
            // 
            // негативToolStripMenuItem
            // 
            this.негативToolStripMenuItem.Name = "негативToolStripMenuItem";
            this.негативToolStripMenuItem.Size = new System.Drawing.Size(139, 24);
            this.негативToolStripMenuItem.Text = "Негатив ";
            this.негативToolStripMenuItem.Click += new System.EventHandler(this.НегативToolStripMenuItem_Click);
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.CanvasPictureBox);
            this.Name = "CanvasForm";
            this.Text = "CanvasForm";
            this.Load += new System.EventHandler(this.CanvasForm_Load);
            this.Resize += new System.EventHandler(this.CanvasForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.CanvasPictureBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox CanvasPictureBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem негативToolStripMenuItem;
    }
}