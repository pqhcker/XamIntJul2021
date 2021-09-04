using System;
namespace XamIntJul2021.AppBase.Validations
{
    public class MaxLengthValidator : FieldValidation
    {
        public object ObjectToValidate
        {
            get;
            set;
        }

        public int MaxLenght { get; set; }

        public MaxLengthValidator(object objectToValidate,int maxLength, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
            MaxLenght = maxLength;
        }

        public MaxLengthValidator(object objectToValidate, int maxLength)
            : this(objectToValidate, maxLength, $"Este campo debe tener menos de {maxLength} carácteres")
        {

        }

        public override void Validate()
        {
            if (ObjectToValidate is string text)
                IsValid = text.Length <= MaxLenght;
        }
    }
}
