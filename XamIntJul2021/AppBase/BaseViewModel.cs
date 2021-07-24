using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamIntJul2021.AppBase.Interfaces;

namespace XamIntJul2021.AppBase
{
    public class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
            NavigationService = (Xamarin.Forms.Application.Current as App).FormsNavigationService;
        }

        public BaseViewModel(INavigationService navigationService)
            => NavigationService = navigationService;

        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string subtitle;

        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        private string icon;

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        private string pageID;

        public string PageId
        {
            get => pageID;
            set => SetProperty(ref pageID, value);
        }

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public INavigationService NavigationService { get; private set; }

        public ICommand OnAppearingCommnad { get; set; }

        public ICommand OnDisapearing { get; set; }

        public virtual void OnBackButtonPressed()
        {

        }

        public virtual void Save()
        {

        }

        public virtual void OnNavigationFrom(Dictionary<string,object> navigationParameters)
        {

        }

        private Dictionary<String, object> navigationParametersToSend;

        protected Dictionary<String, object> NavigationParametersToSend
        {
            get
            {
                if (navigationParametersToSend is null)
                    navigationParametersToSend = new();

                return navigationParametersToSend;
            }
        }
    }
}
