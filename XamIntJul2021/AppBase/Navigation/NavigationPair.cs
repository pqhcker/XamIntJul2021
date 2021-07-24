using System;
namespace XamIntJul2021.AppBase.Navigation
{
    public class NavigationPair
    {
        public NavigationPair(Type pageType, Type viewModelType)
        {
            PageType = pageType;
            ViewModelType = viewModelType;
        }

        public Type PageType { get; private set; }
        public Type ViewModelType { get; private set; }

    }
}
