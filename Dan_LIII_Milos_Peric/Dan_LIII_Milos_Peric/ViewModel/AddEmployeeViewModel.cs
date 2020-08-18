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
            managerList = dataBaseService.GetAllManagers().ToList();
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

        private List<vwManager> managerList;
        public List<vwManager> ManagerList
        {
            get { return managerList; }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }

        public bool IsManagerOnGivenFloor(int? floorNumber)
        {
            if (managerList == null)
            {
                return false;
            }
            else
            {
                foreach (var m in managerList)
                {
                    if (floorNumber == m.FloorNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
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
                if (EntryValidation.ValidateForLetters(employee.FullName) == false)
                {
                    MessageBox.Show("Name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateDate(employee.DateOfBirth) == false)
                {
                    MessageBox.Show("Person must be at least 16 years old. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateEmail(employee.Email) == false)
                {
                    MessageBox.Show("Invalid Email format. Please try again", "Invalid input");
                    return;
                }
                if (employee.UserName.Length < 8)
                {
                    MessageBox.Show("Username must be at least 8 characters", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateNumber(employee.FloorNumber.ToString()) == false)
                {
                    MessageBox.Show("Floor must be a number", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateForLetters(employee.Citizenship) == false)
                {
                    MessageBox.Show("Citizenship can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (IsManagerOnGivenFloor(employee.FloorNumber) == false)
                {
                    MessageBox.Show("No manager present on a given floor, please choose another floor number.", "Invalid input");
                    return;
                }
                string password = (obj as PasswordBox).Password;
                string encryptPassword = EncryptionHelper.Encrypt(password);
                employee.Password = encryptPassword;
                dataBaseService.AddEmployee(employee);
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
            if (string.IsNullOrEmpty(employee.FullName) || string.IsNullOrEmpty(employee.UserName) || string.IsNullOrEmpty(employee.DateOfBirth.ToString()) ||
                string.IsNullOrEmpty(employee.Email) || string.IsNullOrEmpty(employee.FloorNumber.ToString()) ||
                string.IsNullOrEmpty(employee.Gender) || string.IsNullOrEmpty(employee.WorkType))
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
