using System;
namespace XamIntJul2021.AppBase.Validations
{
    public class DoubleGreaterThanValidator : FieldValidation
    {
        public object ObjectToValidate
        {
            get;
            set;
        }

        public decimal MinimumValue { get; set; }

        public DoubleGreaterThanValidator(object objectToValidate, decimal minimumValue, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
            MinimumValue = minimumValue;
        }

        public DoubleGreaterThanValidator(object objectToValidate, decimal minimumValue)
            : this(objectToValidate, minimumValue, $"Este campo debe ser mayor a {minimumValue}")
        {

        }

        public override void Validate()
        {
            if (ObjectToValidate is decimal number)
                IsValid = number >= MinimumValue;
        }
    }
}
