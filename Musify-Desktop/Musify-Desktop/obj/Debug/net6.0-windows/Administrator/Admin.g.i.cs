﻿#pragma checksum "..\..\..\..\Administrator\Admin.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9106F24DA6BAF9E96D33DE5A27B43B6B850E5A64"
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
    /// Admin
    /// </summary>
    public partial class Admin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button songButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button artistButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button instrumentButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button groupButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button extensionButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button participaButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button artistGroupButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button songAlbumbutton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button mongoSong;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Administrator\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MusiFy_Library.Button mongoHistorial;
        
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
            System.Uri resourceLocater = new System.Uri("/Musify-Desktop;component/administrator/admin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Administrator\Admin.xaml"
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
            this.songButton = ((MusiFy_Library.Button)(target));
            
            #line 17 "..\..\..\..\Administrator\Admin.xaml"
            this.songButton.Click += new System.Windows.RoutedEventHandler(this.BtnSongs_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.artistButton = ((MusiFy_Library.Button)(target));
            
            #line 18 "..\..\..\..\Administrator\Admin.xaml"
            this.artistButton.Click += new System.Windows.RoutedEventHandler(this.BtnArtists_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.instrumentButton = ((MusiFy_Library.Button)(target));
            return;
            case 4:
            this.groupButton = ((MusiFy_Library.Button)(target));
            return;
            case 5:
            this.extensionButton = ((MusiFy_Library.Button)(target));
            return;
            case 6:
            this.participaButton = ((MusiFy_Library.Button)(target));
            return;
            case 7:
            this.artistGroupButton = ((MusiFy_Library.Button)(target));
            return;
            case 8:
            this.songAlbumbutton = ((MusiFy_Library.Button)(target));
            return;
            case 9:
            this.mongoSong = ((MusiFy_Library.Button)(target));
            return;
            case 10:
            this.mongoHistorial = ((MusiFy_Library.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

