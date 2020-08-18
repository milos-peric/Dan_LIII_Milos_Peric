using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_LIII_Milos_Peric.Utility
{
    class PasswordLoader
    {
        private const string pathPasswordAccess = @"..\..\OwnerAccess.txt";
        private static List<string> passwordData = new List<string>();
        public static List<string> LoadPassword()
        {
            string line;
            try
            {
                using (StreamReader streamReader = new StreamReader(pathPasswordAccess))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] lines = line.Split(':');
                        line = lines.ElementAt(1).ToString();
                        passwordData.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Not possible to read from file {0} or file doesn't exist. ", pathPasswordAccess);
                Debug.WriteLine(e.Message);
            }
            return passwordData;
        }
    }
}
