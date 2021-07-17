using System;
using System.Text.RegularExpressions;
using XamIntJul2021.AppBase.Enums;

namespace XamIntJul2021.AppBase.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateString(ValidationType validationType, string value)
        {
            Regex regex;

            switch (validationType)
            {
                case ValidationType.None:
                    return true;
                    
                case ValidationType.Email:
                    regex = new(Constants.ValidationConstants.EMAIL);
                    break;
                case ValidationType.Password:
                    regex = new(Constants.ValidationConstants.PASSWORD);
                    break;
                case ValidationType.PhoneNumber:
                    regex = new(Constants.ValidationConstants.PHONE);
                    break;
                case ValidationType.Empty:
                    regex = new(Constants.ValidationConstants.EMPTY);
                    break;
                default:
                    throw new ArgumentException("Validation is not valid");
                    
            }

            Match match = regex.Match(value);
            bool success = match.Success;
            return success;
        }
    }
}