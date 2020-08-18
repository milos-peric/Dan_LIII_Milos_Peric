using Dan_LIII_Milos_Peric.Command;
using Dan_LIII_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dan_LIII_Milos_Peric.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        LoginView login;
        DataBaseService dataBaseService = new DataBaseService();
        public LoginViewModel(LoginView viewLogin)
        {
            login = viewLogin;
            manager = new vwManager();
            employee = new vwEmployee();
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private vwManager manager;
        public vwManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
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

        private ICommand submit;
        public ICommand Submit
        {
            get
            {
                if (submit == null)
                {
                    submit = new RelayCommand(SubmitCommandExecute, param => CanSubmitCommandExecute());
                }
                return submit;
            }
        }

        private void SubmitCommandExecute(object obj)
        {
            try
            {
                List<string> strList = Utility.PasswordLoader.LoadPassword();
                string password = (obj as PasswordBox).Password;
                employee = dataBaseService.FindEmployeeCredentials(UserName, password);
                manager = dataBaseService.FindManagerCredentials(UserName, password);
                if (UserName.Equals(strList.ElementAt(0)) && password.Equals(strList.ElementAt(1)))
                {
                    OwnerView ownerView = new OwnerView();
                    login.Close();
                    ownerView.Show();
                    return;
                }
                else if (manager != null)
                {
                    ManagerView managerView = new ManagerView(manager);
                    login.Close();
                    managerView.Show();
                    return;
                }
                else if (employee != null)
                {
                    EmployeeView employeeView = new EmployeeView(employee);
                    login.Close();
                    employeeView.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong usename or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSubmitCommandExecute()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
