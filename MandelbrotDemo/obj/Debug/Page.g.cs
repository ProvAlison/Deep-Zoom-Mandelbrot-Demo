﻿#pragma checksum "C:\git\Deep-Zoom-Mandelbrot-Demo\MandelbrotDemo\Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C9E2D6B8B51CC9789079C2EE0E5B014F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MandelbrotDemo {
    
    
    public partial class Page : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.MultiScaleImage MSI;
        
        internal System.Windows.Controls.Slider BlueSlider;
        
        internal System.Windows.Controls.Slider RedSlider;
        
        internal System.Windows.Controls.Slider ZoomSlider;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MandelbrotDemo;component/Page.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MSI = ((System.Windows.Controls.MultiScaleImage)(this.FindName("MSI")));
            this.BlueSlider = ((System.Windows.Controls.Slider)(this.FindName("BlueSlider")));
            this.RedSlider = ((System.Windows.Controls.Slider)(this.FindName("RedSlider")));
            this.ZoomSlider = ((System.Windows.Controls.Slider)(this.FindName("ZoomSlider")));
        }
    }
}

