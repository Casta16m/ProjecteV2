﻿#pragma checksum "..\..\..\..\Administrator\AdminInstrument.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FBD44C8010268DF78825BDB3D9F2DDB6B4C12E4E"
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
    /// AdminInstrument
    /// </summary>
    public partial class AdminInstrument : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Administrator\AdminInstrument.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvArtists;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Administrator\AdminInstrument.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNom;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Administrator\AdminInstrument.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtModel;
        
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
            System.Uri resourceLocater = new System.Uri("/Musify-Desktop;component/administrator/admininstrument.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Administrator\AdminInstrument.xaml"
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
            this.lvArtists = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.txtNom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtModel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 43 "..\..\..\..\Administrator\AdminInstrument.xaml"
            ((MusiFy_Library.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btCreateInstrumentClick);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 44 "..\..\..\..\Administrator\AdminInstrument.xaml"
            ((MusiFy_Library.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btUpdateInstrumentClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 45 "..\..\..\..\Administrator\AdminInstrument.xaml"
            ((MusiFy_Library.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btDeleteInstrumentClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

