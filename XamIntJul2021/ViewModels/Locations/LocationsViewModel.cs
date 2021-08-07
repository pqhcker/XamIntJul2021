using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public LocationsViewModel()
        {
            Title = AppResources.PageTitleLocations;
            PageId = AppBase.Constants.PageIds.LOCATIONS;
            branchesService = new BranchRestService
                (UserSettings.AccessToken, AppBase.Constants.RestServiceConstants.TOKEN_TYPE);
            OnAppearingCommnad = new Command(async () => await LoadBranches());
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
                if(selectedBranch is not null)
                {
                    NavigationParametersToSend[AppBase.Constants.Parameters.BRANCH] = selectedBranch;
                    SelectedBranch = null;
                    NavigationService.NavigateToAsync(AppBase.Constants.PageIds.BRANCH_DETAIL, NavigationParametersToSend);
                }
            }
                

        }

        private async Task LoadBranches()
        {
            var branchesResponse = await branchesService.GetBranches();
            if (branchesResponse.ServiceResponse == AppBase.Enums.ServiceResponse.Ok)
                Branches = new(branchesResponse.Branches);
        }
    }
}
