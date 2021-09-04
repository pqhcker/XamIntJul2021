using System;
namespace XamIntJul2021.AppBase.Validations
{
    public class EmailValidator : FieldValidation
    {
        public object ObjectToValidate
        {
            get;
            set;
        }

        public EmailValidator(object objectToValidate, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
        }

        public EmailValidator(object objectToValidate)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = $"Este campo debe cumplir con el formato de email";
        }

        public override void Validate()
        {
            if (ObjectToValidate is string text)
                IsValid = Helpers.ValidationHelper.ValidateString
                    (Enums.ValidationType.Email, text);
        }
    }
}
