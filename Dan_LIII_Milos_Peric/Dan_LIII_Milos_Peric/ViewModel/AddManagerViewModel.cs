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
            if (string.IsNullOrEmpty(manager.FullName) || string.IsNullOrEmpty(manager.UserName))
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
