using Dan_LIII_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_LIII_Milos_Peric.ViewModel
{
    class EmployeeViewModel : ViewModelBase
    {
        private EmployeeView employeeView;

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
    }
}
