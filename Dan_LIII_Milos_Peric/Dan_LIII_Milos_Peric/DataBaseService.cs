using Dan_LIII_Milos_Peric.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_LIII_Milos_Peric
{
    class DataBaseService
    {
        internal vwManager AddManager(vwManager manager)
        {
            try
            {
                using (HotelServiceEntities context = new HotelServiceEntities())
                {
                    tblUser newUser = new tblUser
                    {
                        FullName = manager.FullName,
                        DateOfBirth = manager.DateOfBirth,
                        Email = manager.Email,
                        UserName = manager.UserName,
                        Password = manager.Password
                    };
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    manager.UserID = newUser.UserID;
                    tblManager newManager = new tblManager
                    {
                        FloorNumber = manager.FloorNumber,
                        WorkExperience = manager.WorkExperience,
                        LevelOfEducation = manager.LevelOfEducation,
                        UserID = manager.UserID
                    };
                    context.tblManagers.Add(newManager);
                    context.SaveChanges();
                    manager.ManagerID = newManager.ManagerID;
                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwEmployee AddEmployee(vwEmployee employee)
        {
            try
            {
                using (HotelServiceEntities context = new HotelServiceEntities())
                {
                    tblUser newUser = new tblUser
                    {
                        FullName = employee.FullName,
                        DateOfBirth = employee.DateOfBirth,
                        Email = employee.Email,
                        UserName = employee.UserName,
                        Password = employee.Password
                    };
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    employee.UserID = newUser.UserID;
                    tblEmployee newEmployee = new tblEmployee
                    {
                        FloorNumber = employee.FloorNumber,
                        Gender = employee.Gender,
                        Citizenship = employee.Citizenship,
                        WorkType = employee.WorkType,
                        UserID = employee.UserID
                    };
                    context.tblEmployees.Add(newEmployee);
                    context.SaveChanges();
                    employee.EmployeeID = newEmployee.EmployeeID;
                    return employee;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal vwManager FindManagerCredentials(string userName, string password)
        {
            try
            {
                using (HotelServiceEntities context = new HotelServiceEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    vwManager managerToFind = (from m in context.vwManagers where m.UserName == userName && m.Password == encryptedPassword select m).First();
                    return managerToFind;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Manager not found." + ex.Message.ToString());
                return null;
            }
        }

        internal vwEmployee FindEmployeeCredentials(string userName, string password)
        {
            try
            {
                using (HotelServiceEntities context = new HotelServiceEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    vwEmployee adminToFind = (from e in context.vwEmployees where e.UserName == userName && e.Password == encryptedPassword select e).First();
                    return adminToFind;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Employee not found." + ex.Message.ToString());
                return null;
            }
        }
    }
}
