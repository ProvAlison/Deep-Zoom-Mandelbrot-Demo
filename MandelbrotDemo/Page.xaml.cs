using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace MandelbrotDemo
{
    public partial class Page : UserControl
    {
        private bool _Dragging = false;
        private Point _LastViewportOrigin;
        private Point _LastMousePosition;

        public Page()
        {
            InitializeComponent();
            
            // Point MultiScaleImage control to dynamic tile source
            MSI.Source = new MandelbrotTileSource((int)Math.Pow(2, 30), (int)Math.Pow(2, 30), 128, 128);

            // Register mousewheel event handler
            HtmlPage.Window.AttachEvent("DOMMouseScroll", OnMouseWheelTurned);
            HtmlPage.Window.AttachEvent("onmousewheel", OnMouseWheelTurned);
            HtmlPage.Document.AttachEvent("onmousewheel", OnMouseWheelTurned);
        }

        private void MSI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _LastViewportOrigin = MSI.ViewportOrigin;
            _LastMousePosition = e.GetPosition(MSI);
            _Dragging = true;
        }

        private void MSI_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Dragging)
            {
                // If the left mouse button is down, pan the Deep Zoom scene
                Point pos = e.GetPosition(MSI);
                Point origin = _LastViewportOrigin;
                origin.X += (_LastMousePosition.X - pos.X) / MSI.ActualWidth * MSI.ViewportWidth;
                origin.Y += (_LastMousePosition.Y - pos.Y) / MSI.ActualWidth * MSI.ViewportWidth;
                MSI.ViewportOrigin = _LastViewportOrigin = origin;
                _LastMousePosition = pos;
            }
        }

        private void MSI_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _Dragging = false;
        }

        private void MSI_MouseLeave(object sender, MouseEventArgs e)
        {
            _Dragging = false;
        }

        private void OnMouseWheelTurned(Object sender, HtmlEventArgs args)
        {
            double delta = 0;
            ScriptObject e = args.EventObject;

            if (e.GetProperty("wheelDelta") != null) // IE and Opera
            {
                delta = ((double)e.GetProperty("wheelDelta"));
                if (HtmlPage.Window.GetProperty("opera") != null)
                    delta = -delta;
            }
            else if (e.GetProperty("detail") != null) // Mozilla and Safari
            {
                delta = -((double)e.GetProperty("detail"));
            }

            if (delta > 0)
            {
                ZoomIn(args.ClientX, args.ClientY);
            }
            else if (delta < 0)
            {
                ZoomOut(args.ClientX, args.ClientY);
            }

            if (delta != 0)
            {
                args.PreventDefault();
                e.SetProperty("returnValue", false);
            }
        }

        private void ZoomIn(double x, double y)
        {
            if (!_Dragging)
            {
                Point point = MSI.ElementToLogicalPoint(new Point(x, y));
                MSI.ZoomAboutLogicalPoint(1.25, point.X, point.Y);
            }
        }

        private void ZoomOut(double x, double y)
        {
            if (!_Dragging)
            {
                Point point = MSI.ElementToLogicalPoint(new Point(x, y));
                MSI.ZoomAboutLogicalPoint(0.8, point.X, point.Y);
            }
        }
        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> eventArgs)
        {
            Point origin = _LastViewportOrigin;
            double change = eventArgs.NewValue - eventArgs.OldValue;

            origin.X += change / MSI.ActualWidth * MSI.ViewportWidth;
            MSI.ViewportOrigin = _LastViewportOrigin = origin;
        }

        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> eventArgs)
        {
            Point origin = _LastViewportOrigin;
            double change = eventArgs.NewValue - eventArgs.OldValue;

            origin.Y += change / MSI.ActualWidth * MSI.ViewportWidth;
            MSI.ViewportOrigin = _LastViewportOrigin = origin;
        }

    }
}
