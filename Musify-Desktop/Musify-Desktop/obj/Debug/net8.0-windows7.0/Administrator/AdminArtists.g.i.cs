﻿#pragma checksum "..\..\..\..\Administrator\AdminArtists.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "11019E967309B726CE5BAD0E862F9F5E26FC5820"
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
    /// AdminArtists
    /// </summary>
    public partial class AdminArtists : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\Administrator\AdminArtists.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvArtists;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Administrator\AdminArtists.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNom;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Administrator\AdminArtists.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAnyNeixament;
        
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
            System.Uri resourceLocater = new System.Uri("/Musify-Desktop;V1.0.0.0;component/administrator/adminartists.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Administrator\AdminArtists.xaml"
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
            
            #line 20 "..\..\..\..\Administrator\AdminArtists.xaml"
            this.lvArtists.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvArtists_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtNom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtAnyNeixament = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 48 "..\..\..\..\Administrator\AdminArtists.xaml"
            ((MusiFy_Library.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btCreateArtistClick);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 49 "..\..\..\..\Administrator\AdminArtists.xaml"
            ((MusiFy_Library.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btUpdateArtistClick);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 50 "..\..\..\..\Administrator\AdminArtists.xaml"
            ((MusiFy_Library.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btDeleteArtistClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

