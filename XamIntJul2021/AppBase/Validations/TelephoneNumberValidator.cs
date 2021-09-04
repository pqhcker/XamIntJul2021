using System;
namespace XamIntJul2021.AppBase.Validations
{
    public class TelephoneNumberValidator : FieldValidation
    {
        public object ObjectToValidate
        {
            get;
            set;
        }

        public TelephoneNumberValidator(object objectToValidate, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
        }

        public TelephoneNumberValidator(object objectToValidate)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = $"Este campo debe cumplir con el formato de número de teléfono";
        }

        public override void Validate()
        {
            if (ObjectToValidate is string text)
                IsValid = Helpers.ValidationHelper.ValidateString
                    (Enums.ValidationType.PhoneNumber, text);
        }
    }
}
