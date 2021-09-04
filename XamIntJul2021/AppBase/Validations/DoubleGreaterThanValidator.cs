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

        public double MinimumValue { get; set; }

        public DoubleGreaterThanValidator(object objectToValidate, double minimumValue, string errorMessage)
        {
            ObjectToValidate = objectToValidate;
            ErrorMessage = errorMessage;
            MinimumValue = minimumValue;
        }

        public DoubleGreaterThanValidator(object objectToValidate, double minimumValue)
            : this(objectToValidate, minimumValue, $"Este campo debe ser mayor a {minimumValue}")
        {

        }

        public override void Validate()
        {
            if (ObjectToValidate is double number)
                IsValid = number >= MinimumValue;
        }
    }
}
