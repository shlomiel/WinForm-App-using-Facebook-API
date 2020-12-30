using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace DP_20C_OmriShlomi_Logic
{
    public class UserAgeProxy : IAgeCalculator
    {
         public UserAge TheUsersAge { get; set; }

        private string CalcAndGetAge()
        {
            string theAgeString = TheUsersAge.OnCalcAndGetAge();
            int birthDay = int.Parse(theAgeString.Split('/')[1]);
            int birthMonth = int.Parse(theAgeString.Split('/')[0]);
            int birthYear = int.Parse(theAgeString.Split('/')[2]);
            DateTime userBirthDate = new DateTime(birthYear, birthMonth, birthDay);
            float userAge = (float)(DateTime.Now - userBirthDate).TotalDays / 365;
            return userAge.ToString();
        }

        public string OnCalcAndGetAge()
        {
            return CalcAndGetAge();
        }
    }
}
