using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Helpers
{
    public static class OTPGenerator
    {
        public static string GenerateOTP(int length = 6)
        {
            Random random = new Random();
            string otp = "";

            for (int i = 0; i < length; i++)
            {
                otp += random.Next(0, 7).ToString();
            }

            return otp;
        }
    }
}
