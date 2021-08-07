using System;
using System.Collections.Generic;
using XamIntJul2021.AppBase;
using XamIntJul2021.AppBase.Localization;
using XamIntJul2021.Model;

namespace XamIntJul2021.ViewModels.Locations
{
    public class BranchDetailViewModel : BaseViewModel
    {
        public BranchDetailViewModel()
        {
            Title = AppResources.BranchDetailTitle;
            PageId = AppBase.Constants.PageIds.BRANCH_DETAIL;
        }

        public override void OnNavigationFrom(Dictionary<string, object> navigationParameters)
        {
            var branch = navigationParameters[AppBase.Constants.Parameters.BRANCH] as Branch;
            Name = branch.Name;
            Location = branch.Location;
        }

        private string name;
        private string location;

        public String Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public String Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }
    }
}
