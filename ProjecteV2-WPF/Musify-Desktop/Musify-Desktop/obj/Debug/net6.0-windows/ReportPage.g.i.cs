﻿#pragma checksum "..\..\..\ReportPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4890BAF4439EAA87CF52AF81AA23FDCF1EFAA143"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MusiFy_Library;
using Musify_Desktop;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Musify_Desktop {
    
    
    /// <summary>
    /// ReportPage
    /// </summary>
    public partial class ReportPage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\ReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button songButtonIn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\ReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button artistButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\ReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button instrumentButton;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\ReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button groupButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\ReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button albumButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Musify-Desktop;V1.0.0.0;component/reportpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ReportPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.songButtonIn = ((MusiFy_Library.Button)(target));
            
            #line 20 "..\..\..\ReportPage.xaml"
            this.songButtonIn.Click += new System.Windows.RoutedEventHandler(this.BtnSongs_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.artistButton = ((MusiFy_Library.Button)(target));
            
            #line 21 "..\..\..\ReportPage.xaml"
            this.artistButton.Click += new System.Windows.RoutedEventHandler(this.BtnArtists_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.instrumentButton = ((MusiFy_Library.Button)(target));
            
            #line 22 "..\..\..\ReportPage.xaml"
            this.instrumentButton.Click += new System.Windows.RoutedEventHandler(this.BtnInstruments_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.groupButton = ((MusiFy_Library.Button)(target));
            
            #line 26 "..\..\..\ReportPage.xaml"
            this.groupButton.Click += new System.Windows.RoutedEventHandler(this.BtnGrups_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.albumButton = ((MusiFy_Library.Button)(target));
            
            #line 27 "..\..\..\ReportPage.xaml"
            this.albumButton.Click += new System.Windows.RoutedEventHandler(this.BtnAlbums_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

