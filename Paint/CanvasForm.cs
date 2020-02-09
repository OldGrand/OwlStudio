using Paint.EventArgsClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class CanvasForm : Form
    {
        private Graphics graphics;
        private Pen pen;
        private Brush brush;
        private Bitmap bitmap;
        private Point start;
        private bool drawing;
        private DrawingMethod drawingMethod;

        public CanvasForm()
        {
            InitializeComponent();

            bitmap = new Bitmap(CanvasPictureBox.Width, CanvasPictureBox.Height);
            drawing = false;

            CanvasPictureBox.Image = new Bitmap(CanvasPictureBox.Width, CanvasPictureBox.Height);

            CanvasPictureBox.MouseDown += CanvasForm_MouseDown;
            CanvasPictureBox.MouseMove += CanvasForm_MouseMove;
            CanvasPictureBox.MouseUp += CanvasForm_MouseUp;
        }

        private void CanvasForm_Resize(object sender, EventArgs e)
        {
            //TODO настроить масштабирование
            if (bitmap != null)
            {
                Bitmap resizedBitMap = new Bitmap(ClientSize.Width, ClientSize.Height);
                graphics = Graphics.FromImage(resizedBitMap);
                graphics.DrawImage(bitmap, 0, 0);
                bitmap = resizedBitMap;
                graphics = Graphics.FromImage(bitmap);
                CanvasPictureBox.Image = bitmap;
            }
        }

        private void CanvasForm_Load(object sender, EventArgs e)
        {
            if (IsMdiChild)
            {
                this.MouseMove += new MouseEventHandler(CanvasForm_MouseMove);
            }
        }

        public void ToolSelectionHandler(object sender, ToolSelectionEventArgs args)
        {
            pen = args.Pen;
            brush = args.Brush;
            drawingMethod = args.DrawingMethod;
        }

        private void CanvasForm_MouseDown(object sender, MouseEventArgs e)
        {
            ((MainForm)MdiParent).MainForm_MouseMove(sender, e);
            start = new Point(e.X, e.Y);
            drawing = true;
        }

        private void CanvasForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawing) return;
            if(drawingMethod != null)
            {
                graphics = Graphics.FromImage(bitmap);
                drawingMethod(ref graphics, bitmap, CanvasPictureBox, start, new Point(e.X, e.Y), pen, brush);
                graphics.Dispose();
                CanvasPictureBox.Invalidate();
            }
        }

        private void CanvasForm_MouseUp(object sender, MouseEventArgs e)
        {
            if(drawingMethod != null)
            {
                graphics = Graphics.FromImage(bitmap);
                drawingMethod(ref graphics, bitmap, CanvasPictureBox, start, new Point(e.X, e.Y), pen, brush);
                bitmap = (Bitmap)CanvasPictureBox.Image;
                graphics.Dispose();
                CanvasPictureBox.Invalidate();
                drawing = false;
            }
        }

        private void ВставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(Clipboard.GetImage());
            Bitmap res = new Bitmap(CanvasPictureBox.Image);
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    if (start.X + i < res.Width && start.Y + j < res.Height)
                        res.SetPixel(start.X + i, start.Y + j, bm.GetPixel(i, j));
                }
            }
            bitmap = res;
            this.CanvasPictureBox.Image = res;
        }

        private void НегативToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap dasd = new Bitmap(CanvasPictureBox.Image);
            for (int i = 0; i < dasd.Width; i++)
            {
                for (int j = 0; j < dasd.Height; j++)
                {
                    dasd.SetPixel(i, j, Color.FromArgb(255 - dasd.GetPixel(i, j).A, 255 - dasd.GetPixel(i, j).R, 255 - dasd.GetPixel(i, j).G, 255 - dasd.GetPixel(i, j).B));
                }
            }
            bitmap = dasd;
            CanvasPictureBox.Image = dasd;
        }
    }
}
