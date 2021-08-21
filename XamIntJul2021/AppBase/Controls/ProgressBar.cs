using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Localization;

namespace XamIntJul2021.AppBase.Controls
{
    public class ProgressBar : StackLayout
    {
        public static readonly BindableProperty CurrentStepProperty
            = BindableProperty.Create(nameof(CurrentStep), typeof(int),
                typeof(ProgressBar), 0);


        public int CurrentStep
        {
            get => (int)GetValue(CurrentStepProperty);
            set => SetValue(CurrentStepProperty, value);
        }

        public static readonly BindableProperty TotalStepsProperty
            = BindableProperty.Create(nameof(TotalSteps), typeof(int),
                typeof(ProgressBar), 1);


        public int TotalSteps
        {
            get => (int)GetValue(TotalStepsProperty);
            set => SetValue(TotalStepsProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName.Equals(nameof(CurrentStep)) || propertyName.Equals(nameof(TotalSteps)))
            {
                InitializeComponent();
            }
            base.OnPropertyChanged(propertyName);
        }

        private void InitializeComponent()
        {
            //StackLayout layout = new();

            Children.Clear();

            Label messageLabel = new()
            {
                Text = AppResources.ProgressBarHeader,
                Style = Application.Current.Resources["ProgressBarHeader"] as Style
            };

            Grid stepsGrid = new()
            {
                Margin = new Thickness(0, 0, 0, 15)
            };

            RowDefinition row = new()
            {
                Height = 15
            };

            stepsGrid.RowDefinitions.Add(row);

            double totalWidth = (double)1 / TotalSteps;

            for (int i = 0; i < TotalSteps; i++)
            {
                ColumnDefinition columnDefinition = new()
                {
                    Width = new GridLength(totalWidth, GridUnitType.Star)
                };

                stepsGrid.ColumnDefinitions.Add(columnDefinition);

                Label lb = new()
                {
                    Style =
                    Application.Current.Resources[i <= CurrentStep - 1 ? "ProgressBarDark" : "ProgressBarLight"] as Style
                };

                stepsGrid.Children.Add(lb);

                Grid.SetColumn(lb, i);
                //Esto por defecto va en la columna 0
                Grid.SetRow(lb, 0);

                Children.Add(messageLabel);
                Children.Add(stepsGrid);

                //Esto era para tratar de asignarlo en el ContentView. Pero se puso a
                //heredar de StackLayour y se soluciono.
                //Content = layout;
            }

        }

        public ProgressBar()
        {
        }
    }
}
