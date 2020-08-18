using Dan_LIII_Milos_Peric.Command;
using Dan_LIII_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dan_LIII_Milos_Peric.ViewModel
{
    class EmployeeViewModel : ViewModelBase
    {
        private EmployeeView employeeView;
        DataBaseService dataBaseService = new DataBaseService();

        public EmployeeViewModel(EmployeeView employeeView)
        {
            this.employeeView = employeeView;
            employee = new vwEmployee();
        }

        public EmployeeViewModel(EmployeeView employeeView, vwEmployee employee)
        {
            this.employeeView = employeeView;
            this.employee = employee;
        }

        private vwEmployee employee;
        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new RelayCommand(param => Logout());
                    return logoutCommand;
                }
                return logoutCommand;
            }
        }

        public void Logout()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        LoginView loginView = new LoginView();
                        Thread.Sleep(1000);
                        employeeView.Close();
                        loginView.Show();
                        return;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
