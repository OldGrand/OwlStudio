using Paint.EventArgsClasses;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public delegate void ToolSelectionDelegate(object sender, ToolSelectionEventArgs args);
    public delegate void DrawingMethod(ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush);
    public partial class MainForm : Form
    {
        private Color choosedColor;
        private DrawingMethod drawingMethod;
        private float x, y, polyLineX, polyLineY;
        private ColorConverter converter = new ColorConverter();
        public event ToolSelectionDelegate ToolSelection;
        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            colorDialog1.FullOpen = true;
            fontDialog1.ShowEffects = true;
            fontDialog1.ShowColor = true;
            choosedColor = Color.Black;
            toolStrip1.BackColor = (Color)converter.ConvertFromString("#6E6E6E");
            menuStrip1.BackColor = (Color)converter.ConvertFromString("#6E6E6E");
            файлToolStripMenuItem.ForeColor = (Color)converter.ConvertFromString("#DDDDDD");
        }

        private void ФайлToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            файлToolStripMenuItem.ForeColor = (Color)converter.ConvertFromString("#535353");
        }

        private void ФайлToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            файлToolStripMenuItem.ForeColor = (Color)converter.ConvertFromString("#DDDDDD");
        }

        public void Status(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Status(e.X, e.Y);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(MdiClient))
                {
                    MdiClient mdi = c as MdiClient;
                    mdi.MouseMove += new MouseEventHandler(MainForm_MouseMove);
                }
            }
        }

        private void СоздатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasForm canvasForm = new CanvasForm();
            canvasForm.MdiParent = this;
            ToolSelection += canvasForm.ToolSelectionHandler;
            if (drawingMethod != null)
                ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
            canvasForm.Show();
        }

        private void ЗакрытьТекущийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
        }

        private void ЗакрытьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (MdiChildren.Length > 0)
            {
                MdiChildren[0].Close();
            }
        }

        private void ToolStripButton12_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            choosedColor = colorDialog1.Color;
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate(ref Graphics graphics,Bitmap bitmap,PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                if (finish.X > start.X && finish.Y > start.Y)
                    graphics.DrawRectangle(pen, start.X, start.Y, finish.X - start.X, finish.Y - start.Y);
                else if (finish.X < start.X && finish.Y < start.Y)
                    graphics.DrawRectangle(pen, finish.X, finish.Y, start.X - finish.X, start.Y - finish.Y);
                else if (finish.X < start.X && finish.Y > start.Y)
                    graphics.DrawRectangle(pen, finish.X, start.Y, start.X - finish.X, finish.Y - start.Y);
                else
                    graphics.DrawRectangle(pen, start.X, finish.Y, finish.X - start.X, start.Y - finish.Y);
                canvas.Image = bitmap2; 
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                Rectangle rectangle;
                if (finish.X > start.X && finish.Y > start.Y)
                    rectangle = new Rectangle(start.X, start.Y, finish.X - start.X, finish.Y - start.Y);
                else if (finish.X < start.X && finish.Y < start.Y)
                    rectangle = new Rectangle(finish.X, finish.Y, start.X - finish.X, start.Y - finish.Y);
                else if (finish.X < start.X && finish.Y > start.Y)
                    rectangle = new Rectangle(finish.X, start.Y, start.X - finish.X, finish.Y - start.Y);
                else
                    rectangle = new Rectangle(start.X, finish.Y, finish.X - start.X, start.Y - finish.Y);
                graphics.DrawRectangle(pen, rectangle);
                graphics.FillRectangle(brush, rectangle);
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                graphics.DrawLine(pen, start, finish);
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                if (finish.X > start.X && finish.Y > start.Y)
                    graphics.DrawEllipse(pen, start.X, start.Y, finish.X - start.X, finish.Y - start.Y);
                else if (finish.X < start.X && finish.Y < start.Y)
                    graphics.DrawEllipse(pen, finish.X, finish.Y, start.X - finish.X, start.Y - finish.Y);
                else if (finish.X < start.X && finish.Y > start.Y)
                    graphics.DrawEllipse(pen, finish.X, start.Y, start.X - finish.X, finish.Y - start.Y);
                else
                    graphics.DrawEllipse(pen, start.X, finish.Y, finish.X - start.X, start.Y - finish.Y);
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                Rectangle rectangle;
                if (finish.X > start.X && finish.Y > start.Y)
                    rectangle = new Rectangle(start.X, start.Y, finish.X - start.X, finish.Y - start.Y);
                else if (finish.X < start.X && finish.Y < start.Y)
                    rectangle = new Rectangle(finish.X, finish.Y, start.X - finish.X, start.Y - finish.Y);
                else if (finish.X < start.X && finish.Y > start.Y)
                    rectangle = new Rectangle(finish.X, start.Y, start.X - finish.X, finish.Y - start.Y);
                else
                    rectangle = new Rectangle(start.X, finish.Y, finish.X - start.X, start.Y - finish.Y);
                graphics.DrawEllipse(pen, rectangle);
                graphics.FillEllipse(brush, rectangle);
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton10_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                Font font = fontDialog1.Font;
                string text = Interaction.InputBox("Введите текст", "Текст", "Lorem ipsum dolor sit amet");
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                graphics.DrawString(text, font, brush, start);
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton11_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                Font font = fontDialog1.Font;
                Brush fontBrush = new SolidBrush(fontDialog1.Color);
                string text = Interaction.InputBox("Введите текст", "Текст", "Lorem ipsum dolor sit amet");
                SizeF size = graphics.MeasureString(text,font);
                Rectangle rectangle = new Rectangle(start.X, start.Y, (int)size.Width, (int)size.Height);
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                graphics.DrawRectangle(pen, rectangle);
                graphics.FillRectangle(brush, rectangle);
                graphics.DrawString(text, font, fontBrush, start);
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)//копирование
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                int LeftX = start.X < finish.X ? start.X : finish.X;
                int TopY = start.Y < finish.Y ? start.Y : finish.Y;
                if (Math.Abs(start.X - finish.X) > 0 && Math.Abs(start.Y - finish.Y) > 0)
                {
                    Bitmap bm = new Bitmap(Math.Abs(start.X - finish.X), Math.Abs(start.Y - finish.Y));
                    Bitmap ist = new Bitmap((ActiveMdiChild as CanvasForm).CanvasPictureBox.Image);
                    for (int i = LeftX; i < LeftX + bm.Width; i++)
                    {
                        for (int j = TopY; j < TopY + bm.Height; j++)
                        {
                            bm.SetPixel(i - LeftX, j - TopY, ist.GetPixel(i, j));
                        }
                    }
                    Clipboard.SetImage(bm);
                    canvas.Invalidate();
                }
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                pen = new Pen(pen.Color, 8);
                graphics.DrawLine(pen, x, y, finish.X, finish.Y);
                x = finish.X;
                y = finish.Y;
                canvas.Image = bitmap;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }


        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                graphics.DrawLine(pen, x, y, finish.X, finish.Y);
                x = finish.X;
                y = finish.Y;
                canvas.Image = bitmap;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }

        private void ToolStripButton9_Click(object sender, EventArgs e)//поли линия
        {
            drawingMethod = delegate (ref Graphics graphics, Bitmap bitmap, PictureBox canvas, Point start, Point finish, Pen pen, Brush brush)
            {
                Bitmap bitmap2 = new Bitmap(bitmap);
                graphics = Graphics.FromImage(bitmap2);
                graphics.DrawLine(pen, polyLineX, polyLineY, finish.X, finish.Y);
                polyLineX = finish.X;
                polyLineY = finish.Y;
                canvas.Image = bitmap2;
            };
            ToolSelection?.Invoke(this, new ToolSelectionEventArgs(new Pen(choosedColor), new SolidBrush(choosedColor), drawingMethod));
        }
    }
}
