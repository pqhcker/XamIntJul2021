using System;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Enums;
using XamIntJul2021.AppBase.Helpers;

namespace XamIntJul2021.AppBase.Behaviors
{
    public class EntryValidationBehavior : Behavior<Entry>
    {

        static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateAttachedReadOnly(nameof(IsValid), typeof(bool), typeof(EntryValidationBehavior), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidPropertyKey, value);
        }

        public static readonly BindableProperty ValidationTypeProperty =
            BindableProperty.Create(nameof(ValidationType), typeof(ValidationType), typeof(EntryValidationBehavior), ValidationType.None);

        public ValidationType ValidationType
        {
            get { return (ValidationType)base.GetValue(ValidationTypeProperty); }
            set { base.SetValue(ValidationTypeProperty, value); }
        }

        public EntryValidationBehavior()
        {
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
        }

        private void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            BindingContext = entry.BindingContext;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            IsValid = ValidationHelper.ValidateString(ValidationType, entry.Text);
            entry.TextColor = IsValid ? Color.Black : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            BindingContext = null;
        }
    }
}
