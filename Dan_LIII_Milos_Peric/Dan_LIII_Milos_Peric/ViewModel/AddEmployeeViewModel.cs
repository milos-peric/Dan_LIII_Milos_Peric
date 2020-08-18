using Dan_LIII_Milos_Peric.Command;
using Dan_LIII_Milos_Peric.Utility;
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
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployeeView employeeView;
        DataBaseService dataBaseService = new DataBaseService();
        public AddEmployeeViewModel(AddEmployeeView viewEmployee)
        {
            employeeView = viewEmployee;
            employee = new vwEmployee();
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

        private ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand
        {
            get
            {
                if (addEmployeeCommand == null)
                {
                    addEmployeeCommand = new RelayCommand(AddEmployeeCommandExecute, param => CanAddEmployeeCommandExecute());
                }
                return addEmployeeCommand;
            }
        }

        private void AddEmployeeCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                string encryptPassword = EncryptionHelper.Encrypt(password);
                employee.Password = encryptPassword;
                dataBaseService.AddEmployee(employee);
                //check if floor manager exits before assigning employee to specific floor
                MessageBox.Show("New employee registered successfully!", "Info");
                employeeView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddEmployeeCommandExecute()
        {
            if (string.IsNullOrEmpty(employee.FullName) || string.IsNullOrEmpty(employee.UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => CancelCommandExecute());
                }
                return cancelCommand;
            }
        }

        private void CancelCommandExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to close window?", "Close Window", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        employeeView.Close();
                        break;
                    case MessageBoxResult.No:
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
