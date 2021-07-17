using System;
namespace XamIntJul2021.AppBase.Constants
{
    public static class ValidationConstants
    {
        public const string EMAIL = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string PHONE = @"\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})? ?(\w{1,10}\s?\d{1,6})?";
        public const string PASSWORD = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}";
        public const string EMPTY = @"(.|\s)*\S(.|\s)*";
    }
}
