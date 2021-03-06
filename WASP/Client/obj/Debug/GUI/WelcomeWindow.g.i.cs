﻿#pragma checksum "..\..\..\GUI\WelcomeWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B35909F976179C0E442B4C2885E1F469"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Client.DataClasses;
using Client.GUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Client.GUI {
    
    
    /// <summary>
    /// WelcomeWindow
    /// </summary>
    public partial class WelcomeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgForums;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label headlineLabel;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoginSU;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogOut;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEnterForum;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewForum;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\GUI\WelcomeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Client;component/gui/welcomewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\GUI\WelcomeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dgForums = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.headlineLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnLoginSU = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\GUI\WelcomeWindow.xaml"
            this.btnLoginSU.Click += new System.Windows.RoutedEventHandler(this.btnLoginSU_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnLogOut = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\GUI\WelcomeWindow.xaml"
            this.btnLogOut.Click += new System.Windows.RoutedEventHandler(this.btnLogOutSU_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnEnterForum = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\GUI\WelcomeWindow.xaml"
            this.btnEnterForum.Click += new System.Windows.RoutedEventHandler(this.btnEnterForum_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnNewForum = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\GUI\WelcomeWindow.xaml"
            this.btnNewForum.Click += new System.Windows.RoutedEventHandler(this.btnNewForum_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\GUI\WelcomeWindow.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

