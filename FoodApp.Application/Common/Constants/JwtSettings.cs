using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Constants
{
    public static class JwtSettings
    {
        public const string SecretKey = "ABYREQ$EWIEOUSLHT#@!WPIDTREFRSEE*&^%DHGFDREE";
        public const string Issuer = "FoodApp";
        public const string Audience = "FoodApp-Users";
        public const int DurationInMinutes = 60;
    }
}
