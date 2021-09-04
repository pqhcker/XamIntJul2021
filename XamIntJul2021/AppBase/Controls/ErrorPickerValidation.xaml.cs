using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Effects;
using XamIntJul2021.AppBase.Enums;

namespace XamIntJul2021.AppBase.Controls
{
    public partial class ErrorPickerValidation : StackLayout
    {
        public ErrorPickerValidation()
        {
            InitializeComponent();
            IsObligatory = false;
            picker.BindingContext = this;
            label.BindingContext = this;
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create("IsValid", typeof(ValidStatus), typeof(ErrorPickerValidation), ValidStatus.None, defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsValidChanged);
        public ValidStatus IsValid
        {
            get => (ValidStatus)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }
        public static readonly BindableProperty LineColorProperty = BindableProperty.CreateAttached("LineColor", typeof(Color), typeof(ErrorPickerValidation), Color.Default);
        public static Color GetLineColor(BindableObject view)
        {
            return (Color)view.GetValue(LineColorProperty);
        }
        public static void SetLineColor(BindableObject view, Color value)
        {
            view.SetValue(LineColorProperty, value);
        }
        public static readonly BindableProperty IsObligatoryProperty = BindableProperty.Create("IsObligatory", typeof(bool), typeof(ErrorPickerValidation), true, propertyChanged: IsObligatotyPropertyChanged);
        public bool IsObligatory
        {
            get => (bool)GetValue(IsObligatoryProperty);
            set => SetValue(IsObligatoryProperty, value);
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(ErrorPickerValidation), string.Empty, propertyChanged: TitlePropertyChanged);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList), typeof(ErrorPickerValidation), null, defaultBindingMode: BindingMode.TwoWay);
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(ErrorPickerValidation), null, defaultBindingMode: BindingMode.TwoWay);
        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
                IsValid = ValidStatus.None;
            }
        }
        public static readonly BindableProperty ErrorDescriptionProperty = BindableProperty.Create(nameof(ErrorDescription), typeof(string), typeof(ErrorPickerValidation), string.Empty, defaultBindingMode: BindingMode.TwoWay);
        public string ErrorDescription
        {
            get => (string)GetValue(ErrorDescriptionProperty);
            set
            {
                SetValue(ErrorDescriptionProperty, value?.ToUpperInvariant());
            }
        }
        static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (bindable as StackLayout).Children[1] as Picker;
            if ((ValidStatus)newValue != ValidStatus.Invalid)
            {
                SetLineColor(bindable, Color.Black);
                var entryLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                if (entryLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(entryLineColorEffectToRemove);
                }
                (bindable as ErrorPickerValidation).label.IsVisible = false;
            }
            else
            {
                SetLineColor(bindable, Color.Red);
                view.Effects.Add(new EntryLineColorEffect());
                (bindable as ErrorPickerValidation).label.Text = (bindable as ErrorPickerValidation).ErrorDescription;
            }
        }
        static void IsObligatotyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var errorEntry = bindable as ErrorPickerValidation;
            errorEntry.lbIndicator.IsVisible = (bool)newValue;
        }
        static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var errorPicker = bindable as ErrorPickerValidation;
            errorPicker.picker.Title = (string)newValue;
            errorPicker.lbTitle.Text = (string)newValue;
        }
        
    }
}
