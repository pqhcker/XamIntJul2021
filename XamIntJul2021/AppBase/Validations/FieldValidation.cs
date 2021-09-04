using System;
namespace XamIntJul2021.AppBase.Validations
{
    public abstract class FieldValidation : ObservableObject
    {
        private bool isValid;

        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        public virtual void Validate()
        {

        }
    }
}
