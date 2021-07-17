using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace XamIntJul2021.AppBase.Controls
{
    [ContentProperty("LayoutContent")]
    public class BindedPage : ContentPage
    {
        public static readonly BindableProperty LayoutContentProperty
            = BindableProperty.Create(nameof(LayoutContent), typeof(View), typeof(BindedPage), null, propertyChanged: OnContentChanged);

        public View LayoutContent
        {
            get => (View)GetValue(LayoutContentProperty);
            set => SetValue(LayoutContentProperty, value);
        }

        private static void OnContentChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            if (bindableObject is BindedPage bindedPage)
            {
                var contentViewVar = new ContentView
                {
                    ControlTemplate = App.Current.Resources["MasterTemplate"] as ControlTemplate,
                    Content = newValue as View
                };

                bindedPage.Content = contentViewVar;
            }
        }


        public static readonly BindableProperty PageIdProperty
            = BindableProperty.Create(nameof(PageId), typeof(string), typeof(BindedPage), "00");

        public string PageId
        {
            get => (string)GetValue(PageIdProperty);
            set => SetValue(PageIdProperty, value);
        }

        

        public static readonly BindableProperty SubtitleProperty
            = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(BindedPage), "00");

        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is BaseViewModel context)
                context?.OnAppearingCommnad?.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is BaseViewModel context)
                context?.OnDisapearing?.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is BaseViewModel context)
                context?.OnBackButtonPressed();
            return base.OnBackButtonPressed();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Binding titleBinding = new Binding("Title");
            SetBinding(TitleProperty, titleBinding);

            Binding subtitleBinding = new Binding("Subtitle");
            SetBinding(SubtitleProperty, subtitleBinding);

            Binding pageBinding = new Binding("PageId");
            SetBinding(PageIdProperty, pageBinding);

            Binding isBusyBinding = new Binding("IsBusy");
            SetBinding(IsBusyProperty, isBusyBinding);
        }


        public BindedPage()
        {
            On<iOS>().SetUseSafeArea(true);
        }
    }
}
