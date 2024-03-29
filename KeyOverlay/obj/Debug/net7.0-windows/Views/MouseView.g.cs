﻿#pragma checksum "..\..\..\..\Views\MouseView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5837E3A680C856646B26F9D85859B8DF287AF5C7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Programowanie.Views {
    
    
    /// <summary>
    /// MouseView
    /// </summary>
    public partial class MouseView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\..\Views\MouseView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ScrollClicks;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\MouseView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox MouseClicks;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\MouseView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ScrollCps;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\MouseView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox MouseCps;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Programowanie;component/views/mouseview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MouseView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 15 "..\..\..\..\Views\MouseView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonBase_OnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 46 "..\..\..\..\Views\MouseView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.MouseStatisticsCheck);
            
            #line default
            #line hidden
            
            #line 47 "..\..\..\..\Views\MouseView.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.MouseStatisticsUncheck);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ScrollClicks = ((System.Windows.Controls.CheckBox)(target));
            
            #line 56 "..\..\..\..\Views\MouseView.xaml"
            this.ScrollClicks.Checked += new System.Windows.RoutedEventHandler(this.ScrollClicksCheck);
            
            #line default
            #line hidden
            
            #line 57 "..\..\..\..\Views\MouseView.xaml"
            this.ScrollClicks.Unchecked += new System.Windows.RoutedEventHandler(this.ScrollClicksUncheck);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MouseClicks = ((System.Windows.Controls.CheckBox)(target));
            
            #line 66 "..\..\..\..\Views\MouseView.xaml"
            this.MouseClicks.Checked += new System.Windows.RoutedEventHandler(this.MouseClicksCheck);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\..\Views\MouseView.xaml"
            this.MouseClicks.Unchecked += new System.Windows.RoutedEventHandler(this.MouseClicksUncheck);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ScrollCps = ((System.Windows.Controls.CheckBox)(target));
            
            #line 76 "..\..\..\..\Views\MouseView.xaml"
            this.ScrollCps.Checked += new System.Windows.RoutedEventHandler(this.ScrollCpsCheck);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\..\Views\MouseView.xaml"
            this.ScrollCps.Unchecked += new System.Windows.RoutedEventHandler(this.ScrollCpsUncheck);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MouseCps = ((System.Windows.Controls.CheckBox)(target));
            
            #line 86 "..\..\..\..\Views\MouseView.xaml"
            this.MouseCps.Checked += new System.Windows.RoutedEventHandler(this.MouseCpsCheck);
            
            #line default
            #line hidden
            
            #line 87 "..\..\..\..\Views\MouseView.xaml"
            this.MouseCps.Unchecked += new System.Windows.RoutedEventHandler(this.MouseCpsUncheck);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

