using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using XamIntJul2021.AppBase.Enums;

namespace XamIntJul2021.AppBase.Controls
{
    public class AutoCompleteView : ContentView
    {

        /// <summary>
        /// The show search property.
        /// </summary>
        public static readonly BindableProperty IsObligatoryProperty = BindableProperty.Create("IsObligatory", typeof(bool),
                                                                                                           typeof(AutoCompleteView), false, BindingMode.TwoWay, propertyChanged: IsObligatoryChanged);


        /// <summary>
        /// The execute on suggestion click property.
        /// </summary>
        public static readonly BindableProperty ExecuteOnSuggestionClickProperty = BindableProperty.Create("ExecuteOnSuggestionClick", typeof(bool),
                                                                                                           typeof(AutoCompleteView), false, BindingMode.TwoWay);
        /// <summary>
        /// The placeholder property.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string),
                                                                                              typeof(AutoCompleteView), string.Empty, BindingMode.TwoWay, propertyChanged: PlaceHolderChanged);


        /// <summary>
        /// The search background color property.
        /// </summary>
        public static readonly BindableProperty SearchBackgroundColorProperty = BindableProperty.Create("SearchBackgroundColor", typeof(Color),
                                                                                                        typeof(AutoCompleteView), Color.Red, BindingMode.TwoWay, propertyChanged: SearchBackgroundColorChanged);

        /// <summary>
        /// The search border color property.
        /// </summary>
        public static readonly BindableProperty SearchBorderColorProperty = BindableProperty.Create("SearchBorderColor", typeof(Color),
                                                                                                    typeof(AutoCompleteView), Color.White, BindingMode.TwoWay, propertyChanged: SearchBorderColorChanged);

        /// <summary>
        /// The search border radius property.
        /// </summary>
        public static readonly BindableProperty SearchBorderRadiusProperty = BindableProperty.Create("SearchBorderRadius", typeof(int),
                                                                                                           typeof(AutoCompleteView), 0, BindingMode.TwoWay, propertyChanged: SearchBorderRadiusChanged);

        /// <summary>
        /// The search border width property.
        /// </summary>
        public static readonly BindableProperty SearchBorderWidthProperty = BindableProperty.Create("SearchBorderWidth", typeof(int),
                                                                                                           typeof(AutoCompleteView), 1, BindingMode.TwoWay, propertyChanged: SearchBorderWidthChanged);

        /// <summary>
        /// The search command property.
        /// </summary>
        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create("SearchCommandProperty", typeof(ICommand),
                                                                                                           typeof(AutoCompleteView), null, BindingMode.TwoWay);

        /// <summary>
        /// The search horizontal options property
        /// </summary>
        public static readonly BindableProperty SearchHorizontalOptionsProperty = BindableProperty.Create("SearchHorizontalOptions", typeof(LayoutOptions),
                                                                                                          typeof(AutoCompleteView), LayoutOptions.FillAndExpand, BindingMode.TwoWay, propertyChanged: SearchHorizontalOptionsChanged);

        /// <summary>
        /// The search text color property.
        /// </summary>
        public static readonly BindableProperty SearchTextColorProperty = BindableProperty.Create("SearchTextColor", typeof(Color),
                                                                                                  typeof(AutoCompleteView), Color.Red, BindingMode.TwoWay, propertyChanged: SearchTextColorChanged);

        /// <summary>
        /// The search text property.
        /// </summary>
        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create("SearchText", typeof(string),
                                                                                                        typeof(AutoCompleteView), "Search", BindingMode.TwoWay, propertyChanged: SearchTextChanged);

        /// <summary>
        /// The search vertical options property
        /// </summary>
        public static readonly BindableProperty SearchVerticalOptionsProperty = BindableProperty.Create("SearchVerticalOptions", typeof(LayoutOptions),
                                                                                                        typeof(AutoCompleteView), LayoutOptions.Center, BindingMode.TwoWay, propertyChanged: SearchVerticalOptionsChanged);

        /// <summary>
        /// The selected command property.
        /// </summary>
        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create("SelectedCommand", typeof(ICommand),
                                                                                                           typeof(AutoCompleteView), null, BindingMode.TwoWay);

        /// <summary>
        /// The selected item property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object),
                                                                                                           typeof(AutoCompleteView), null, BindingMode.TwoWay, propertyChanged: SelectedItemPropertyChanged);

        /// <summary>
        /// The show search property.
        /// </summary>
        public static readonly BindableProperty ShowSearchProperty = BindableProperty.Create("ShowSearch", typeof(bool),
                                                                                                           typeof(AutoCompleteView), true, BindingMode.TwoWay, propertyChanged: ShowSearchChanged);

        /// <summary>
        /// The suggestion background color property.
        /// </summary>
        public static readonly BindableProperty SuggestionBackgroundColorProperty = BindableProperty.Create("SuggestionBackgroundColorProperty", typeof(Color),
                                                                                                           typeof(AutoCompleteView), Color.Red, BindingMode.TwoWay, propertyChanged: SuggestionBackgroundColorChanged);

        /// <summary>
        /// The suggestion item data template property.
        /// </summary>
        public static readonly BindableProperty SuggestionItemDataTemplateProperty = BindableProperty.Create("SuggestionItemDataTemplateProperty", typeof(DataTemplate),
                                                                                                           typeof(AutoCompleteView), null, BindingMode.TwoWay, propertyChanged: SuggestionItemDataTemplateChanged);

        /// <summary>
        /// The suggestion height request property.
        /// </summary>
        public static readonly BindableProperty SuggestionsHeightRequestProperty = BindableProperty.Create("SuggestionsHeightRequest", typeof(double),
                                                                                                           typeof(AutoCompleteView), 250d, BindingMode.TwoWay, propertyChanged: SuggestionHeightRequestChanged);

        /// <summary>
        /// The suggestions property.
        /// </summary>
        public static readonly BindableProperty SuggestionsProperty = BindableProperty.Create("Suggestions", typeof(IEnumerable),
                                                                                                           typeof(AutoCompleteView), null, BindingMode.TwoWay);

        /// <summary>
        /// The text background color property.
        /// </summary>
        public static readonly BindableProperty TextBackgroundColorProperty = BindableProperty.Create("TextBackgroundColor", typeof(Color),
                                                                                                      typeof(AutoCompleteView), Color.Transparent, BindingMode.TwoWay, propertyChanged: TextBackgroundColorChanged);

        /// <summary>
        /// The text color property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color),
                                                                                            typeof(AutoCompleteView), Color.Black, BindingMode.TwoWay, propertyChanged: TextColorChanged);

        /// <summary>
        /// The text horizontal options property
        /// </summary>
        public static readonly BindableProperty TextHorizontalOptionsProperty = BindableProperty.Create("TextHorizontalOptions", typeof(LayoutOptions),
                                                                                                        typeof(AutoCompleteView), LayoutOptions.FillAndExpand, BindingMode.TwoWay, propertyChanged: TextHorizontalOptionsChanged);

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string),
                                                                                       typeof(AutoCompleteView), string.Empty, BindingMode.TwoWay, propertyChanged: TextValueChanged);

        /// <summary>
        /// The text vertical options property.
        /// </summary>
        public static readonly BindableProperty TextVerticalOptionsProperty = BindableProperty.Create("TextVerticalOptions", typeof(LayoutOptions),
                                                                                                      typeof(AutoCompleteView), LayoutOptions.Start, BindingMode.TwoWay, propertyChanged: TestVerticalOptionsChanged);

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(AutoCompleteView), Keyboard.Default, propertyChanged: KeyboardPropertyChanged);



        static void KeyboardPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var autoCompleteView = bindable as AutoCompleteView;
            autoCompleteView.EntText.Keyboard = (Keyboard)newValue;
        }

        #region IsValid
        public static readonly BindableProperty LineColorProperty =
            BindableProperty.CreateAttached("LineColor", typeof(Color), typeof(AutoCompleteView), Color.Default);

        public static Color GetLineColor(BindableObject view)
        {
            return (Color)view.GetValue(LineColorProperty);
        }

        public static void SetLineColor(BindableObject view, Color value)
        {
            view.SetValue(LineColorProperty, value);
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create("IsValid", typeof(ValidStatus), typeof(AutoCompleteView), ValidStatus.None, propertyChanged: IsValidChanged);

        public ValidStatus IsValid
        {
            get => (ValidStatus)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((ValidStatus)newValue == ValidStatus.Invalid)
            {
                SetLineColor(bindable, Color.Red);

            }
            else
            {
                SetLineColor(bindable, Color.Black);
            }

        }

        #endregion

        private readonly ObservableCollection<object> _availableSuggestions;
        private readonly Button _btnSearch;
        private readonly Entry _entText;
        private readonly ListView _lstSuggestions;
        private readonly StackLayout _stkBase;
        private readonly StackLayout _stkHeader;

        private readonly Label _lbIndicator;
        private readonly Label _lbTitle;
        private readonly Label _lbError;


        public Entry EntText => _entText;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteView"/> class.
        /// </summary>
        public AutoCompleteView()
        {
            _availableSuggestions = new ObservableCollection<object>();
            _stkBase = new StackLayout();
            var innerLayout = new StackLayout();
            _entText = new Entry
            {
                HorizontalOptions = TextHorizontalOptions,
                VerticalOptions = TextVerticalOptions,
                TextColor = TextColor,
                BackgroundColor = TextBackgroundColor,
                Keyboard = Keyboard
            };
            _btnSearch = new Button
            {
                VerticalOptions = SearchVerticalOptions,
                HorizontalOptions = SearchHorizontalOptions,
                Text = SearchText
            };

            _lstSuggestions = new ListView
            {
                HeightRequest = SuggestionsHeightRequest,
                HasUnevenRows = true,
                SelectedItem = SelectedItem
            };


            _stkHeader = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            _lbIndicator = new Label
            {
                IsVisible = IsObligatory,
                Text = "*",
                TextColor = Color.Red
            };

            _lbTitle = new Label
            {
                Text = Placeholder,
                Style = Application.Current.Resources["FieldHeader"] as Style
            };

            _lbError = new Label
            {
                TextColor = Color.Red
            };

            _stkHeader.Children.Add(_lbTitle);
            _stkHeader.Children.Add(_lbIndicator);
            innerLayout.Children.Add(_entText);
            innerLayout.Children.Add(_btnSearch);
            _stkBase.Children.Add(_stkHeader);
            _stkBase.Children.Add(innerLayout);
            _stkBase.Children.Add(_lstSuggestions);
            _stkBase.Children.Add(_lbError);

            Content = _stkBase;




            _entText.TextChanged += _entText_TextChanged;
            _btnSearch.Clicked += (s, e) =>
            {
                if (SearchCommand != null && SearchCommand.CanExecute(Text))
                {
                    SearchCommand.Execute(Text);
                }
            };
            _lstSuggestions.ItemTapped += (s, e) =>
            {
                _entText.Text = e.Item.ToString();

                _availableSuggestions.Clear();
                ShowHideListbox(false);
                OnSelectedItemChanged(e.Item, e.ItemIndex);

                if (ExecuteOnSuggestionClick
                   && SearchCommand != null
                   && SearchCommand.CanExecute(Text))
                {
                    SearchCommand.Execute(e);
                }
            };
            ShowHideListbox(false);
            _lstSuggestions.ItemsSource = _availableSuggestions;
        }

        private void _entText_TextChanged(object sender, TextChangedEventArgs e)
        {

            Text = e.NewTextValue;
            SelectedItem = null;
            OnTextChanged(e);

        }

        /// <summary>
        /// Occurs when [selected item changed].
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        /// <summary>
        /// Occurs when [text changed].
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Gets the available Suggestions.
        /// </summary>
        /// <value>The available Suggestions.</value>
        public IEnumerable AvailableSuggestions
        {
            get { return _availableSuggestions; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [execute on sugestion click].
        /// </summary>
        /// <value><c>true</c> if [execute on sugestion click]; otherwise, <c>false</c>.</value>
        public bool ExecuteOnSuggestionClick
        {
            get { return (bool)GetValue(ExecuteOnSuggestionClickProperty); }
            set { SetValue(ExecuteOnSuggestionClickProperty, value); }
        }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the search background.
        /// </summary>
        /// <value>The color of the search background.</value>
        public Color SearchBackgroundColor
        {
            get { return (Color)GetValue(SearchBackgroundColorProperty); }
            set { SetValue(SearchBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search border color.
        /// </summary>
        /// <value>The search border brush.</value>
        public Color SearchBorderColor
        {
            get { return (Color)GetValue(SearchBorderColorProperty); }
            set { SetValue(SearchBorderColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search border radius.
        /// </summary>
        /// <value>The search border radius.</value>
        public int SearchBorderRadius
        {
            get { return (int)GetValue(SearchBorderRadiusProperty); }
            set { SetValue(SearchBorderRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the search border.
        /// </summary>
        /// <value>The width of the search border.</value>
        public int SearchBorderWidth
        {
            get { return (int)GetValue(SearchBorderWidthProperty); }
            set { SetValue(SearchBorderWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search horizontal options.
        /// </summary>
        /// <value>The search horizontal options.</value>
        public LayoutOptions SearchHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(SearchHorizontalOptionsProperty); }
            set { SetValue(SearchHorizontalOptionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the search text button.
        /// </summary>
        /// <value>The color of the search text.</value>
        public Color SearchTextColor
        {
            get { return (Color)GetValue(SearchTextColorProperty); }
            set { SetValue(SearchTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search vertical options.
        /// </summary>
        /// <value>The search vertical options.</value>
        public LayoutOptions SearchVerticalOptions
        {
            get { return (LayoutOptions)GetValue(SearchVerticalOptionsProperty); }
            set { SetValue(SearchVerticalOptionsProperty, value); }
        }


        /// <summary>
        /// Gets or sets the selected command.
        /// </summary>
        /// <value>The selected command.</value>
        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show search button].
        /// </summary>
        /// <value><c>true</c> if [show search button]; otherwise, <c>false</c>.</value>
        public bool ShowSearchButton
        {
            get { return (bool)GetValue(ShowSearchProperty); }
            set { SetValue(ShowSearchProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the sugestion background.
        /// </summary>
        /// <value>The color of the sugestion background.</value>
        public Color SuggestionBackgroundColor
        {
            get { return (Color)GetValue(SuggestionBackgroundColorProperty); }
            set { SetValue(SuggestionBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the suggestion item data template.
        /// </summary>
        /// <value>The sugestion item data template.</value>
        public DataTemplate SuggestionItemDataTemplate
        {
            get { return (DataTemplate)GetValue(SuggestionItemDataTemplateProperty); }
            set { SetValue(SuggestionItemDataTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Suggestions.
        /// </summary>
        /// <value>The Suggestions.</value>
        public IEnumerable Suggestions
        {
            get { return (IEnumerable)GetValue(SuggestionsProperty); }
            set { SetValue(SuggestionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Keyboard.
        /// </summary>
        /// <value>The Keyboard.</value>
        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the suggestion.
        /// </summary>
        /// <value>The height of the suggestion.</value>
        public double SuggestionsHeightRequest
        {
            get { return (double)GetValue(SuggestionsHeightRequestProperty); }
            set { SetValue(SuggestionsHeightRequestProperty, value); }
        }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Color TextBackgroundColor
        {
            get { return (Color)GetValue(TextBackgroundColorProperty); }
            set { SetValue(TextBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text horizontal options.
        /// </summary>
        /// <value>The text horizontal options.</value>
        public LayoutOptions TextHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(TextHorizontalOptionsProperty); }
            set { SetValue(TextHorizontalOptionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text vertical options.
        /// </summary>
        /// <value>The text vertical options.</value>
        public LayoutOptions TextVerticalOptions
        {
            get { return (LayoutOptions)GetValue(TextVerticalOptionsProperty); }
            set { SetValue(TextVerticalOptionsProperty, value); }
        }


        /// <summary>
        /// Gets or sets the is obligatory.
        /// </summary>
        /// <value>The text is obligatory.</value>
        public bool IsObligatory
        {
            get { return (bool)GetValue(IsObligatoryProperty); }
            set { SetValue(IsObligatoryProperty, value); }
        }

        /// <summary>
        /// Places the is obligatory changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldIsObligatoryValue">The old place holder value.</param>
        /// <param name="newIsObligatoryValue">The new place holder value.</param>
        private static void IsObligatoryChanged(BindableObject obj, object oldIsObligatoryValue, object newIsObligatoryValue)
        {
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._lbIndicator.IsVisible = (bool)newIsObligatoryValue;
            }
        }


        /// <summary>
        /// Places the holder changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldPlaceHolderValue">The old place holder value.</param>
        /// <param name="newPlaceHolderValue">The new place holder value.</param>
        private static void PlaceHolderChanged(BindableObject obj, object oldPlaceHolderValue, object newPlaceHolderValue)
        {
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._entText.Placeholder = (string)newPlaceHolderValue;
                autoCompleteView._lbTitle.Text = (string)newPlaceHolderValue;
            }
        }

        /// <summary>
        /// Searches the background color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchBackgroundColorChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (Color)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._stkBase.BackgroundColor = newValue;
            }
        }







        /// <summary>
        /// Searches the border color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchBorderColorChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (Color)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.BorderColor = newValue;
            }
        }

        /// <summary>
        /// Searches the border radius changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchBorderRadiusChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (int)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.CornerRadius = newValue;
            }
        }

        /// <summary>
        /// Searches the border width changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchBorderWidthChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (double)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.BorderWidth = newValue;
            }
        }

        /// <summary>
        /// Searches the horizontal options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchHorizontalOptionsChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (LayoutOptions)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.HorizontalOptions = newValue;
            }
        }

        /// <summary>
        /// Searches the text changed.
        /// </summary>
        /// <param name="obj">The bindable.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchTextChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (string)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.Text = newValue;
            }
        }

        /// <summary>
        /// Searches the text color color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchTextColorChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (Color)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.TextColor = newValue;
            }
        }

        /// <summary>
        /// Searches the vertical options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SearchVerticalOptionsChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (LayoutOptions)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.VerticalOptions = newValue;
            }
        }

        /// <summary>
        /// Shows the search changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldShowSearchValue">if set to <c>true</c> [old show search value].</param>
        /// <param name="newShowSearchValue">if set to <c>true</c> [new show search value].</param>
        private static void ShowSearchChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newShowSearchValue = (bool)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._btnSearch.IsVisible = newShowSearchValue;
            }
        }

        /// <summary>
        /// Suggestions the background color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SuggestionBackgroundColorChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (Color)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._lstSuggestions.BackgroundColor = newValue;
            }
        }

        /// <summary>
        /// Suggestions the height changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SuggestionHeightRequestChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (double)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._lstSuggestions.HeightRequest = newValue;
            }
        }




        /// <summary>
        /// Suggestions the item data template changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldShowSearchValue">The old show search value.</param>
        /// <param name="newShowSearchValue">The new show search value.</param>
        private static void SuggestionItemDataTemplateChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newShowSearchValue = (DataTemplate)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._lstSuggestions.ItemTemplate = newShowSearchValue;
            }
        }

        /// <summary>
        /// Tests the vertical options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TestVerticalOptionsChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (LayoutOptions)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._entText.VerticalOptions = newValue;
            }
        }

        /// <summary>
        /// Texts the background color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TextBackgroundColorChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (Color)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._entText.BackgroundColor = newValue;
            }
        }

        /// <summary>
        /// Texts the color changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TextColorChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (Color)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._entText.TextColor = newValue;
            }
        }

        /// <summary>
        /// Texts the horizontal options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void TextHorizontalOptionsChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (LayoutOptions)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._entText.VerticalOptions = newValue;
            }
        }

        /// <summary>
        /// Texts the horizontal options changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void SelectedItemPropertyChanged(BindableObject obj, object oldVal, object newVal)
        {
            var newValue = (object)newVal;
            if (obj is AutoCompleteView autoCompleteView)
            {
                autoCompleteView._entText.TextChanged -= autoCompleteView._entText_TextChanged;
                autoCompleteView._entText.Text = autoCompleteView.SelectedItem?.ToString();
                autoCompleteView._entText.TextChanged += autoCompleteView._entText_TextChanged;
            }
        }


        /// <summary>
        /// Texts the changed.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="oldPlaceHolderValue">The old place holder value.</param>
        /// <param name="newPlaceHolderValue">The new place holder value.</param>
        private static void TextValueChanged(BindableObject obj, object oldVal, object newVal)
        {
            var oldPlaceHolderValue = (string)oldVal;
            var newPlaceHolderValue = (string)newVal;

            if (obj is AutoCompleteView control)
            {
                control._btnSearch.IsEnabled = !string.IsNullOrEmpty(newPlaceHolderValue);

                var cleanedNewPlaceHolderValue = Regex.Replace((newPlaceHolderValue ?? string.Empty).ToLowerInvariant(), @"\s+", string.Empty);

                if (!string.IsNullOrEmpty(cleanedNewPlaceHolderValue) && control.Suggestions != null)
                {
                    var filteredSuggestions = control.Suggestions.Cast<object>()
                        .Where(x => Regex.Replace(x.ToString().ToLowerInvariant(), @"\s+", string.Empty).Contains(cleanedNewPlaceHolderValue))
                        .OrderByDescending(x => Regex.Replace(x.ToString()
                        .ToLowerInvariant(), @"\s+", string.Empty)
                        .StartsWith(cleanedNewPlaceHolderValue, StringComparison.Ordinal)).ToList();

                    control._availableSuggestions.Clear();
                    if (filteredSuggestions.Count > 0)
                    {
                        foreach (var suggestion in filteredSuggestions)
                        {
                            control._availableSuggestions.Add(suggestion);
                        }
                        control.ShowHideListbox(true);
                    }
                    else
                    {
                        control.ShowHideListbox(false);
                    }
                }
                else
                {
                    if (control._availableSuggestions.Count > 0)
                    {
                        control._availableSuggestions.Clear();
                        control.ShowHideListbox(false);
                    }
                }
            }
        }

        /// <summary>
        /// Called when [selected item changed].
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        private void OnSelectedItemChanged(object selectedItem, int index)
        {
            SelectedItem = selectedItem;

            if (SelectedCommand != null)
                SelectedCommand.Execute(selectedItem);

            SelectedItemChanged?.Invoke(this, new SelectedItemChangedEventArgs(selectedItem, index));
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void OnTextChanged(TextChangedEventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Shows the hide listbox.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowHideListbox(bool show)
        {
            _lstSuggestions.IsVisible = show;
        }
    }
}