using System;
namespace XamIntJul2021.AppBase.Validations
{
    public class NullOrWhiteSpaceValidation : FieldValidation
    {
        public object ObjectToValidate { get; set; }

        public NullOrWhiteSpaceValidation(object objectToValidate, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
        }

        public NullOrWhiteSpaceValidation(object objectToValidate)
            : this(objectToValidate,"Este campo es obligatorio")
        {

        }

        public override void Validate()
        {
            if (ObjectToValidate is string text)
                IsValid = !string.IsNullOrWhiteSpace(text);
            else
                IsValid = ObjectToValidate is not null;
        }
    }
}
