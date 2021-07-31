using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Controls;
using XamIntJul2021.AppBase.Helpers;
using XamIntJul2021.ViewModels;

namespace XamIntJul2021.Views
{
    public partial class TermsAndConditionsPage : BindedPage
    {
        public TermsAndConditionsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(500);

            if(BindingContext is TermsAndConditionsViewModel viewModel)
            {
                var termsContent = await LocalFilesHelper.ReadFileInPackageAsync(viewModel.TermsPath);
                var htmlSource = new HtmlWebViewSource
                {
                    Html = termsContent
                };

                browser.Source = htmlSource;
            }
        }
    }
}
