using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.AppBase.Settings;
using XamIntJul2021.Model;
using XamIntJul2021.Services.Interfaces;
using XamIntJul2021.Services.RestServices;

namespace XamIntJul2021.ViewModels.Locations
{
    public class LocationsViewModel : BaseViewModel
    {
        IBranchesService branchesService;

        public Command LoadNextItemsCommand { get; set; }

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public LocationsViewModel()
        {
            Title = AppResources.PageTitleLocations;
            PageId = AppBase.Constants.PageIds.LOCATIONS;
            branchesService = new BranchRestService
                (UserSettings.AccessToken, AppBase.Constants.RestServiceConstants.TOKEN_TYPE);
            OnAppearingCommnad = new Command(async () => await LoadBranches());

            LoadNextItemsCommand = new(async () => await LoadNextItems());
        }

        private ObservableCollection<Branch> branches;

        public ObservableCollection<Branch> Branches
        {
            get => branches;
            set => SetProperty(ref branches, value);

        }

        private Branch selectedBranch;

        public Branch SelectedBranch
        {
            get => selectedBranch;
            set
            {
                SetProperty(ref selectedBranch, value);
                if (selectedBranch is not null)
                {
                    NavigationParametersToSend[AppBase.Constants.Parameters.BRANCH] = selectedBranch;
                    SelectedBranch = null;
                    NavigationService.NavigateToAsync(AppBase.Constants.PageIds.BRANCH_DETAIL, NavigationParametersToSend);
                }
            }


        }

        private async Task LoadBranches()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                IsRefreshing = true;
                currentPage = 1;
                var branchesResponse = await branchesService.GetBranches(currentPage, AppBase.Constants.RestServiceConstants.BRANCH_PAGE_SIZE);
                if (branchesResponse.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                    Branches = new(branchesResponse.Branches);
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        private int currentPage = 1;

        private async Task LoadNextItems()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                currentPage++;
                var branchesResponse = await branchesService.GetBranches(currentPage, AppBase.Constants.RestServiceConstants.BRANCH_PAGE_SIZE);
                if (branchesResponse.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                    foreach (var branch in branchesResponse.Branches)
                    {
                        Branches.Add(branch);
                    }

                IsBusy = false;
            }

        }
    }
}
