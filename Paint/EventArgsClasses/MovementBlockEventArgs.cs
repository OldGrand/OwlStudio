using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.EventArgsClasses
{
    public class MovementBlockEventArgs
    {
        public bool MouseMoveBlock { get; set; }

        public MovementBlockEventArgs(bool mouseMoveBlock)
        {
            MouseMoveBlock = mouseMoveBlock;
        }
    }
}
