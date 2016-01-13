using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace HCHSoft.WPF.Misc
{
    public class InteropHelper
    {
        public static IntPtr GetWindowHnadle(Window win)
        {
            return new WindowInteropHelper(win).Handle;
        }

        public static IntPtr GetVisualHandle(UIElement element)
        {
            return ((HwndSource)PresentationSource.FromVisual(element)).Handle;
        }
    }
}
