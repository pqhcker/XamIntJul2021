using System;
namespace XamIntJul2021.AppBase.Validations
{
    public class ExactLengthValidator : FieldValidation
    {
        public object ObjectToValidate
        {
            get;
            set;
        }

        public int Length { get; set; }

        public ExactLengthValidator(object objectToValidate,int length, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
            Length = length;
        }

        public ExactLengthValidator(object objectToValidate, int length)
            : this(objectToValidate, length, $"Este campo debe tener {length} carácteres")
        {

        }

        public override void Validate()
        {
            if (ObjectToValidate is string text)
                IsValid = text.Length == Length;
        }
    }
}
