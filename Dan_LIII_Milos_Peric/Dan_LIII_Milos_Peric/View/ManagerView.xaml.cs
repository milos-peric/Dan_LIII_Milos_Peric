﻿using Dan_LIII_Milos_Peric.ViewModel;
using System;
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
using System.Windows.Shapes;

namespace Dan_LIII_Milos_Peric.View
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView()
        {
            InitializeComponent();
            DataContext = new ManagerViewModel(this);
        }

        public ManagerView(vwManager manager)
        {
            InitializeComponent();
            DataContext = new ManagerViewModel(this, manager);
        }
    }
}
