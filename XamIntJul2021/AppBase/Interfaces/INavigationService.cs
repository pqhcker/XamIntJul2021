using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamIntJul2021.AppBase.Interfaces
{
    public interface INavigationService
    {
        Task GoBackAsync();
        Task GoBackModalAsync();
        Task NavigateToAsync(string pageName);
        Task NavigateToModalAsync(string pageName);

        Task NavigateToAsync(string pageName, Dictionary<string, object> navigationParameters);
        Task NavigateToAsync(string pageName, Dictionary<string, object> navigationParameters,
            bool addToNavigationPage = false);

        Task NavigateToAsync(string pageName, Dictionary<string, object> navigationParameters,
            bool isModal = false, bool addToNavigationPage = false);

        Task NavigateToRootAsync();

        Task ReplaceRootAsync(string pageName, bool addToNavigationPage = false);
        Task ReplaceRootAsync(string pageName, Dictionary<string, object> navigationParameters,
            bool addToNavigationPage = false);

        Task NavigateToFlyoutDetailAsync(string pageName, Dictionary<string, object> navigationParameters,
            bool addToNavigationPage = false);
    }
}
