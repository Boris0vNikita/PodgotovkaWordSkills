﻿#pragma checksum "..\..\..\Pages\OrderDetailes.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "701D898BE618591D2C1CE8746447795B1B235658A803891509C11D6C1E8FD1EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TestWSRus.Pages;


namespace TestWSRus.Pages {
    
    
    /// <summary>
    /// OrderDetailes
    /// </summary>
    public partial class OrderDetailes : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Pages\OrderDetailes.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDateOrder;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\OrderDetailes.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCostOrder;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Pages\OrderDetailes.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgrdDetails;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\OrderDetailes.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/TestWSRus;component/pages/orderdetailes.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\OrderDetailes.xaml"
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
            this.lblDateOrder = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lblCostOrder = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.dgrdDetails = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Pages\OrderDetailes.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

