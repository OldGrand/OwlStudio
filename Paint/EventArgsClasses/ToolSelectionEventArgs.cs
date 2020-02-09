using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public class ToolSelectionEventArgs : EventArgs
    {
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public DrawingMethod DrawingMethod { get; set; }

        public ToolSelectionEventArgs(Pen pen, Brush brush, DrawingMethod drawingMethod)
        {
            Pen = pen;
            Brush = brush;
            DrawingMethod = drawingMethod;
        }
    }
}
