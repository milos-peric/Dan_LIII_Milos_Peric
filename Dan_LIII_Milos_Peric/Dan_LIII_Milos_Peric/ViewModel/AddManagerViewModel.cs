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
    class AddManagerViewModel : ViewModelBase
    {
        AddManagerView managerView;
        DataBaseService dataBaseService = new DataBaseService();
        public AddManagerViewModel(AddManagerView viewManager)
        {
            managerView = viewManager;
            manager = new vwManager();
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

        private ICommand addManagerCommand;
        public ICommand AddManagerCommand
        {
            get
            {
                if (addManagerCommand == null)
                {
                    addManagerCommand = new RelayCommand(AddManagerCommandExecute, param => CanAddManagerCommandExecute());
                }
                return addManagerCommand;
            }
        }

        private void AddManagerCommandExecute(object obj)
        {
            try
            {
                if (EntryValidation.ValidateForLetters(manager.FullName) == false)
                {
                    MessageBox.Show("Name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateDate(manager.DateOfBirth) == false)
                {
                    MessageBox.Show("Person must be at least 16 years old. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateEmail(manager.Email) == false)
                {
                    MessageBox.Show("Invalid Email format. Please try again", "Invalid input");
                    return;
                }
                if (manager.UserName.Length < 8)
                {
                    MessageBox.Show("Username must be at least 8 characters", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateNumber(manager.FloorNumber.ToString()) == false)
                {
                    MessageBox.Show("Floor must be a number", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateNumber(manager.WorkExperience.ToString()) == false)
                {
                    MessageBox.Show("Work experience must be a number", "Invalid input");
                    return;
                }
                string password = (obj as PasswordBox).Password;
                string encryptPassword = EncryptionHelper.Encrypt(password);
                manager.Password = encryptPassword;
                dataBaseService.AddManager(manager);
                MessageBox.Show("New manager registered successfully!", "Info");
                managerView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddManagerCommandExecute()
        {
            if (string.IsNullOrEmpty(manager.FullName) || string.IsNullOrEmpty(manager.UserName) || string.IsNullOrEmpty(manager.DateOfBirth.ToString()) ||
                string.IsNullOrEmpty(manager.Email) || string.IsNullOrEmpty(manager.FloorNumber.ToString()) ||
                string.IsNullOrEmpty(manager.WorkExperience.ToString()) || string.IsNullOrEmpty(manager.LevelOfEducation))
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
                        managerView.Close();
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
