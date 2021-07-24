using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Interfaces;

namespace XamIntJul2021.AppBase.Navigation
{
    public class FormsNavigationService : INavigationService
    {
        private INavigation navigation;

        private Dictionary<string, NavigationPair> navigationPairs = new();

        private readonly Application application = null;

        public FormsNavigationService(Application application)
        {
            this.application = application;
        }

        private void InitNavigation()
        {
            if (navigation is null)
            {
                navigation = application.MainPage.Navigation;
            }
        }

        public async Task GoBackAsync()
        {
            InitNavigation();
            await navigation.PopAsync();
        }

        public async Task GoBackModalAsync()
        {
            InitNavigation();
            await navigation.PopModalAsync();
        }

        public async Task NavigateToAsync(string pageName)
            => await NavigateToAsync(pageName, null);

        public async Task NavigateToAsync(string pageName, Dictionary<string, object> navigationParameters)
        => await NavigateToAsync(pageName, navigationParameters, true);

        public async Task NavigateToAsync(string pageName, Dictionary<string, object> navigationParameters, bool addToNavigationPage = false)
        => await NavigateToAsync(pageName, navigationParameters, false, addToNavigationPage);

        public async Task NavigateToAsync(string pageName, Dictionary<string, object> navigationParameters,
            bool isModal = false, bool addToNavigationPage = false)
        {
            InitNavigation();
            if (navigationPairs.ContainsKey(pageName))
            {
                var viewModelType = navigationPairs[pageName].ViewModelType;
                var pageType = navigationPairs[pageName].PageType;

                var pageConstr = GetConstructor(pageType);

                if (pageConstr is not null)
                {
                    var page = pageConstr.Invoke(null) as Page;

                    if (page.BindingContext is not null
                        && page.BindingContext.GetType() == viewModelType
                        && page.BindingContext is BaseViewModel viewModel)

                        viewModel.OnNavigationFrom(navigationParameters);
                    else
                    {
                        var viewModelConst = GetConstructor(viewModelType);
                        var newViewModel = viewModelConst.Invoke(null) as BaseViewModel;
                        page.BindingContext = newViewModel;
                        newViewModel.OnNavigationFrom(navigationParameters);
                    }

                    if (isModal)
                        if (!addToNavigationPage)
                            await navigation.PushModalAsync(page);
                        else
                            await navigation.PushModalAsync(new NavigationPage(page));
                    else
                        if (!addToNavigationPage)
                        await navigation.PushAsync(page);
                    else
                        await navigation.PushAsync(new NavigationPage(page));
                }
                else
                {
                    throw new MissingMemberException($"{pageName} has not valid constructor for navigation. Parameter less constructor is requerid");
                }

            }
            else
            {
                throw new KeyNotFoundException($"{pageName} is not present in the navigation dictionary");
            }
        }

        public async Task NavigateToFlyoutDetailAsync(string pageName, Dictionary<string, object> navigationParameters, bool addToNavigationPage = false)
        {
            if (navigationPairs.ContainsKey(pageName))
            {
                var viewModelType = navigationPairs[pageName].ViewModelType;
                var pageType = navigationPairs[pageName].PageType;

                var pageConstr = GetConstructor(pageType);

                if (pageConstr is not null)
                {
                    var page = pageConstr.Invoke(null) as Page;

                    if (page.BindingContext is not null
                        && page.BindingContext.GetType() == viewModelType
                        && page.BindingContext is BaseViewModel viewModel)

                        viewModel.OnNavigationFrom(navigationParameters);
                    else
                    {
                        var viewModelConst = GetConstructor(viewModelType);
                        var newViewModel = viewModelConst.Invoke(null) as BaseViewModel;
                        page.BindingContext = newViewModel;
                        newViewModel.OnNavigationFrom(navigationParameters);
                    }


                    if (application.MainPage is FlyoutPage flyoutPage)
                    {
                        if (!addToNavigationPage)
                            flyoutPage.Detail = page;
                        else
                            flyoutPage.Detail = new NavigationPage(page);

                        flyoutPage.IsPresented = false;
                    }
                }
                else
                {
                    throw new MissingMemberException($"{pageName} has not valid constructor for navigation. Parameter less constructor is requerid");
                }

            }
            else
            {
                throw new KeyNotFoundException($"{pageName} is not present in the navigation dictionary");
            }
        }

        public async Task NavigateToModalAsync(string pageName)
        => await NavigateToAsync(pageName, null, true, true);

        public Task NavigateToRootAsync() => navigation.PopToRootAsync();

        public async Task ReplaceRootAsync(string pageName, bool addToNavigationPage = false)
        => await ReplaceRootAsync(pageName, null, addToNavigationPage);

        public async Task ReplaceRootAsync(string pageName, Dictionary<string, object> navigationParameters, bool addToNavigationPage = false)
        {
            if (navigationPairs.ContainsKey(pageName))
            {
                var viewModelType = navigationPairs[pageName].ViewModelType;
                var pageType = navigationPairs[pageName].PageType;

                var pageConstr = GetConstructor(pageType);

                if (pageConstr is not null)
                {
                    var page = pageConstr.Invoke(null) as Page;

                    if (page.BindingContext is not null
                        && page.BindingContext.GetType() == viewModelType
                        && page.BindingContext is BaseViewModel viewModel)

                        viewModel.OnNavigationFrom(navigationParameters);
                    else
                    {
                        var viewModelConst = GetConstructor(viewModelType);
                        var newViewModel = viewModelConst.Invoke(null) as BaseViewModel;
                        page.BindingContext = newViewModel;
                        newViewModel.OnNavigationFrom(navigationParameters);
                    }


                    if (!addToNavigationPage)
                        application.MainPage = page;
                    else
                        application.MainPage = new NavigationPage(page);
                }
                else
                {
                    throw new MissingMemberException($"{pageName} has not valid constructor for navigation. Parameter less constructor is requerid");
                }

            }
            else
            {
                throw new KeyNotFoundException($"{pageName} is not present in the navigation dictionary");
            }
        }

        public void RegisterNavigationDictionary(Dictionary<string, NavigationPair> navigationPairs) => this.navigationPairs = navigationPairs;


        private ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo constructorInfo = type.GetTypeInfo().DeclaredConstructors.
                FirstOrDefault(c => !c.GetParameters().Any());

            return constructorInfo;
        }

    }
}
