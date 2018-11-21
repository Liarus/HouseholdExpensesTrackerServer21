using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Helper
{
    public static class Md5HashHelper
    {
        public static string ComputeHash(string data)
        {
            return BitConverter.ToString(
              MD5.Create().ComputeHash(
                Encoding.UTF8.GetBytes(data)
              )
            );
        }
    }
}
