using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Effects;
using XamIntJul2021.AppBase.Enums;

namespace XamIntJul2021.AppBase.Controls
{
    public partial class ErrorEntryValidation : StackLayout
    {
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(ValidStatus),
                typeof(ErrorEntryValidation), null, propertyChanged: IsValidChanged);

        public ValidStatus IsValid
        {
            get => (ValidStatus)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        private static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ErrorEntryValidation errorEntryValidator)
            {
                if ((ValidStatus)newValue != ValidStatus.Invalid)
                {
                    var effect = errorEntryValidator.entry.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                    if (effect is not null)
                        errorEntryValidator.entry.Effects.Remove(effect);
                    errorEntryValidator.lbError.IsVisible = false;
                }
                else
                {
                    errorEntryValidator.entry.Effects.Add(new EntryLineColorEffect());
                    errorEntryValidator.lbError.IsVisible = true;
                }
            }
        }

        public ErrorEntryValidation()
        {
            InitializeComponent();
            entry.BindingContext = this;
            lbError.BindingContext = this;
        }

        public static readonly BindableProperty IsObligatoryProperty = BindableProperty.Create("IsObligatory", typeof(bool), typeof(ErrorEntryValidation), true, propertyChanged: IsObligatotyPropertyChanged);
        public bool IsObligatory
        {
            get => (bool)GetValue(IsObligatoryProperty);
            set => SetValue(IsObligatoryProperty, value);
        }
        static void IsObligatotyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var errorEntry = bindable as ErrorEntryValidation;
            errorEntry.lbIndicator.IsVisible = (bool)newValue;
        }
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(ErrorEntryValidation), string.Empty, propertyChanged: PlaceHolderPropertyChanged);
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        static void PlaceHolderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var errorEntry = bindable as ErrorEntryValidation;
            errorEntry.entry.Placeholder = (string)newValue;
            errorEntry.lbTitle.Text = (string)newValue;
        }
        public static readonly BindableProperty ErrorDescriptionProperty = BindableProperty.Create(nameof(ErrorDescription), typeof(string), typeof(ErrorEntryValidation), string.Empty, defaultBindingMode: BindingMode.TwoWay);
        public string ErrorDescription
        {
            get => (string)GetValue(ErrorDescriptionProperty);
            set
            {
                SetValue(ErrorDescriptionProperty, value);
            }
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(ErrorEntryValidation), string.Empty, defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
                //IsValid = ValidStatus.None;            }
            }
        }
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(ErrorEntryValidation), Keyboard.Default, propertyChanged: KeyboardPropertyChanged);
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }
        static void KeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var errorEntry = bindable as ErrorEntryValidation;
            errorEntry.entry.Keyboard = (Keyboard)newValue;
        }
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(ErrorEntryValidation), 0, propertyChanged: MaxLengthPropertyChanged);
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }
        static void MaxLengthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var errorEntry = bindable as ErrorEntryValidation;
            errorEntry.entry.MaxLength = (int)newValue;
        }
    }
}
