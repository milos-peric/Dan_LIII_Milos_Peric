using Dan_LIII_Milos_Peric.Command;
using Dan_LIII_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dan_LIII_Milos_Peric.ViewModel
{
    class SetSalaryViewModel : ViewModelBase
    {
        private SetSalaryView setSalaryView;
        DataBaseService dataBaseService = new DataBaseService();
        public SetSalaryViewModel(SetSalaryView setSalaryView, vwManager manager, vwEmployee employee)
        {
            this.setSalaryView = setSalaryView;
            this.manager = manager;
            this.employee = employee;
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

        private int salary;
        public int Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
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
                bool isInProperRange = salary > 1 && salary < 1001;
                if (isInProperRange == false)
                {
                    MessageBox.Show("Salary must be in range between 2 and 1000. Please try again.", "Invalid input");
                    return;
                }
                double? workExperianceQuotient = 0.75d * manager.WorkExperience;
                //Debug.WriteLine("workExperianceQuotient: " + workExperianceQuotient);
                int? levelOfEducation = GetLevelOfEducation(manager.LevelOfEducation);
                double? levelOfEducationQuotient = 0.15d * levelOfEducation;
                //Debug.WriteLine("levelOfEducationQuotient: " + levelOfEducationQuotient);
                double? genderQuotient = GetGenderQuotient(employee.Gender);
                //Debug.WriteLine("genderQuotient: " + genderQuotient);
                //Debug.WriteLine("Salary: " + salary);
                decimal? totalSalary = (decimal?)(1000 * workExperianceQuotient * levelOfEducationQuotient * genderQuotient + salary);
                if (totalSalary != null)
                {
                    employee.Salary = totalSalary;
                    dataBaseService.EditEmployeeSalary(employee);
                    IsUpdateSalary = true;
                    MessageBox.Show("Salary set successfull.", "Info");
                    setSalaryView.Close();
                }
                else
                {
                    MessageBox.Show("Error setting salary.", "Info");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSetSalaryCommandExecute()
        {
            if (string.IsNullOrEmpty(Salary.ToString()))
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
                        setSalaryView.Close();
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

        public int? GetLevelOfEducation(string education)
        {
            int? levelOfEducation;
            switch (education)
            {
                case "I":
                    levelOfEducation = 1;
                    break;
                case "II":
                    levelOfEducation = 2;
                    break;
                case "III":
                    levelOfEducation = 3;
                    break;
                case "IV":
                    levelOfEducation = 4;
                    break;
                case "V":
                    levelOfEducation = 5;
                    break;
                case "VI":
                    levelOfEducation = 6;
                    break;
                case "VII":
                    levelOfEducation = 7;
                    break;
                default:
                    levelOfEducation = null;
                    break;
            }
            return levelOfEducation;
        }

        public double? GetGenderQuotient(string gender)
        {
            double? genderQuotient;
            switch (gender)
            {
                case "Male":
                    genderQuotient = 1.12d;
                    break;
                case "Female":
                    genderQuotient = 1.15d;
                    break;
                default:
                    genderQuotient = null;
                    break;
            }
            return genderQuotient;
        }
    }
}
