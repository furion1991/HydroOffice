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

namespace HydroOffice.Views
{
    /// <summary>
    /// Interaction logic for ContractorListView.xaml
    /// </summary>
    public partial class ContractorListView : UserControl
    {
        public ContractorListView()
        {
            InitializeComponent();
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row)
            {
                row.IsSelected = true;
            }
        }
    }
}
