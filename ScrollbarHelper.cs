using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

namespace MSZDialougeManager
{
    internal class ScrollbarHelper
    {
        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_BOTH = 3;

        public enum Scrollbar
        {
            Horz,
            Vert,
            Both
        }

        public static void Set(Control control, Scrollbar bar, bool show)
        {
            int wBar;
            switch (bar)
            {
                case Scrollbar.Horz:
                    wBar = SB_HORZ;
                    break;
                case Scrollbar.Vert:
                    wBar = SB_VERT;
                    break;
                case Scrollbar.Both:
                    wBar = SB_BOTH;
                    break;
                default:
                    wBar = SB_BOTH;
                    break;
            }

            ShowScrollBar(control.Handle, wBar, show);
        }
    }
}