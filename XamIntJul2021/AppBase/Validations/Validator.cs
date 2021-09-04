using System;
using System.Collections.Generic;
using XamIntJul2021.AppBase.Enums;

namespace XamIntJul2021.AppBase.Validations
{
    public class Validator: ObservableObject
    {
        public Validator()
        {
        }

        public Validator(object objectValidate, bool validateNullOrEmpty)
        {
            if (validateNullOrEmpty)
                Validations.Add(new NullOrWhiteSpaceValidation(objectValidate));
        }

        private List<FieldValidation> validations = new ();

        public List<FieldValidation> Validations
        {
            get => validations;
            set => SetProperty(ref validations, value);
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        private ValidStatus isValid;

        public ValidStatus IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        public void Validate()
        {
            foreach (var validation in Validations)
            {
                validation.Validate();
                IsValid = validation.IsValid ? ValidStatus.Valid : ValidStatus.Invalid;

                ErrorMessage = validation.ErrorMessage;
                if (IsValid == ValidStatus.Invalid)
                    break;
            }
        }

    }
}
