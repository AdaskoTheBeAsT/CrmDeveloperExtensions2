﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CrmDeveloperExtensions.Core.Connection;

namespace WebResourceDeployer
{
    /// <summary>
    /// Interaction logic for WebResourceDeployerWindow.xaml
    /// </summary>
    public partial class WebResourceDeployerWindow : UserControl
    {
        public WebResourceDeployerWindow()
        {
            InitializeComponent();
        }

        private void ConnPane_OnConnected(object sender, ConnectEventArgs e)
        {
           //MessageBox.Show(CrmDeveloperExtensions.Core.Crm.Test.DoWhoAmI(e.ServiceClient));
        }
    }
}
