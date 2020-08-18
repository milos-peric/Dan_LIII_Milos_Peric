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
    class ManagerViewModel : ViewModelBase
    {
        private ManagerView managerView;
        DataBaseService dataBaseService = new DataBaseService();

        public ManagerViewModel(ManagerView managerView)
        {
            this.managerView = managerView;
            employee = new vwEmployee();
            employeeList = dataBaseService.GetAllEmployees().ToList();
        }

        public ManagerViewModel(ManagerView managerView, vwManager manager)
        {
            this.managerView = managerView;
            this.manager = manager;
            employee = new vwEmployee();
            employeeList = dataBaseService.GetAllEmployees().ToList();
            EmployeeListOnManagerFloor = new List<vwEmployee>();
            foreach (var e in employeeList)
            {
                if (e.FloorNumber == manager.FloorNumber)
                {
                    EmployeeListOnManagerFloor.Add(e);
                }
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

        private List<vwEmployee> employeeList;
        public List<vwEmployee> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private List<vwEmployee> employeeListOnManagerFloor;
        public List<vwEmployee> EmployeeListOnManagerFloor
        {
            get { return employeeListOnManagerFloor; }
            set
            {
                employeeListOnManagerFloor = value;
                OnPropertyChanged("EmployeeListOnManagerFloor");
            }
        }

        private bool _isUpdateSalary;
        public bool IsUpdateSalary
        {
            get
            {
                return _isUpdateSalary;
            }
            set
            {
                _isUpdateSalary = value;
            }
        }


        private ICommand setSalaryCommand;
        public ICommand SetSalaryCommand
        {
            get
            {
                if (setSalaryCommand == null)
                {
                    setSalaryCommand = new RelayCommand(param => SetSalaryCommandExecute(), param => CanSetSalaryCommandExecute());
                }
                return setSalaryCommand;
            }
        }

        private void SetSalaryCommandExecute()
        {
            try
            {
                SetSalaryView setSalary = new SetSalaryView(manager, employee);
                setSalary.ShowDialog();
                if ((setSalary.DataContext as SetSalaryViewModel).IsUpdateSalary == true)
                {
                    EmployeeList = dataBaseService.GetAllEmployees().ToList();
                    List<vwEmployee> employeesOnSameFloor = new List<vwEmployee>();
                    foreach (var e in employeeList)
                    {
                        if (e.FloorNumber == manager.FloorNumber)
                        {
                            employeesOnSameFloor.Add(e);
                        }
                    }
                    EmployeeListOnManagerFloor = employeesOnSameFloor;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSetSalaryCommandExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand setAllSalariesCommand;
        public ICommand SetAllSalariesCommand
        {
            get
            {
                if (setAllSalariesCommand == null)
                {
                    setAllSalariesCommand = new RelayCommand(param => SetAllSalariesCommandExecute(), param => CanSetAllSalariesCommandExecute());
                }
                return setAllSalariesCommand;
            }
        }

        private void SetAllSalariesCommandExecute()
        {
            try
            {
                MessageBox.Show("Feature not implemented yet, sorry for inconvenience.", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSetAllSalariesCommandExecute()
        {
            if (EmployeeListOnManagerFloor == null)
            {
                return false;
            }
            else
            {
                return true;
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
                        managerView.Close();
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
