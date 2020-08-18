using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dan_LIII_Milos_Peric.Utility
{
    class EntryValidation
    {
        public static bool ValidateForLetters(string nameSurname)
        {
            bool isOnlyLetters = Regex.IsMatch(nameSurname, @"^[a-zA-Z ]+$");
            return isOnlyLetters;
        }

        public static bool ValidateEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool ValidateNumber(string number)
        {
            bool isValidNumberEntry = Regex.IsMatch(number, @"^[0-9]+$");
            return isValidNumberEntry;
        }

        public static bool ValidateDate(DateTime? date)
        {
            if (date == null)
            {
                return false;
            }
            DateTime minimumAge = DateTime.Today;
            minimumAge = minimumAge.AddYears(-16);
            int compareDate = DateTime.Compare((DateTime)date, minimumAge);
            if (compareDate >= 0)
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
