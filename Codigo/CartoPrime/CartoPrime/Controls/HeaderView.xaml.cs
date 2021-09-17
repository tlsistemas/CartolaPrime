using CartoPrime.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CartoPrime.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderView : ContentView
    {
        public static Image headerimage;
        public static Thickness titleMargin;
        public static Thickness arrowMargin;
        public static bool changingMargin;
        #region [BindableProperties]
        public static readonly BindableProperty PageTitleProperty = BindableProperty.Create("PageTitle", typeof(string), typeof(string), default(string));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly BindableProperty BackButtonActionProperty = BindableProperty.Create("BackButtonAction",
             typeof(NavigationBackButtonAction), typeof(NavigationBackButtonAction), default(NavigationBackButtonAction));

        public NavigationBackButtonAction BackButtonAction
        {
            get { return (NavigationBackButtonAction)GetValue(BackButtonActionProperty); }
            set { SetValue(BackButtonActionProperty, value); }
        }


        public static readonly BindableProperty ImageAspectroperty = BindableProperty.Create("ImageAspect",
            typeof(Aspect), typeof(Aspect), default(Aspect), propertyChanged: OnAspecPropertyChanged);

        public Aspect ImageAspect
        {
            get { return (Aspect)GetValue(ImageAspectroperty); }
            set { SetValue(ImageAspectroperty, value); }
        }


        public static readonly BindableProperty ChangeMarginFlagProperty = BindableProperty.Create("ChangeMargimFlag",
            typeof(bool), typeof(bool), default(bool), propertyChanged: OnChanginMarginPropertyChanged);

        public bool ChangeMarginFlag
        {
            get { return (bool)GetValue(ChangeMarginFlagProperty); }
            set { SetValue(ChangeMarginFlagProperty, value); }
        }


        public static readonly BindableProperty TitleMarginProperty = BindableProperty.Create("TitleMargim",
            typeof(Thickness), typeof(Thickness), default(Thickness), propertyChanged: OnTitleMargimPropertyChanged);

        public Thickness TitleMargin
        {
            get { return (Thickness)GetValue(TitleMarginProperty); }
            set { SetValue(TitleMarginProperty, value); }
        }


        public static readonly BindableProperty ArrowMarginProperty = BindableProperty.Create("ArrowMargin",
           typeof(Thickness), typeof(Thickness), default(Thickness), propertyChanged: OnArrowMargimPropertyChanged);

        public Thickness ArrowMargin
        {
            get { return (Thickness)GetValue(ArrowMarginProperty); }
            set { SetValue(ArrowMarginProperty, value); }
        }


        static void OnChanginMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            changingMargin = (bool)newValue;
        }
        static void OnArrowMargimPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            arrowMargin = (Thickness)newValue;
        }

        static void OnTitleMargimPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            titleMargin = (Thickness)newValue;
        }

        static void OnAspecPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            headerimage.Aspect = (Aspect)newValue;
        }


        #endregion
        #region [Constants for ui]
        private int paddingTopToiOS = -67;
        private int paddingTopToAndroid = -25;
        private const double maximunHeight = 150;
        #endregion
        #region [Events]
        public delegate void OnBackButtonPressed(NavigationBackButtonAction showBackButtonActionRequested);
        public event OnBackButtonPressed BackButtonPressedEvent;
        #endregion
        #region [properties]
        public int PaddingTop { get; set; }

        #endregion

        public HeaderView()
        {
            InitializeComponent();

            headerimage = new Image();

            if (Device.RuntimePlatform == Device.iOS)
                ConfigureViewToiOS();

            if (Device.RuntimePlatform == Device.Android)
                ConfigureViewToAndroid();


            if (DesignMode.IsDesignModeEnabled)
            {
                this.BackgroundColor = Color.Red;
            }
            headerTitleText.Text = PageTitle;


        }

        protected override void OnParentSet()
        {

            var screenSize = Application.Current.MainPage.Width;

            if (screenSize <= 320)
            {
                paddingTopToAndroid = paddingTopToAndroid - 6;
            }


            if (Device.RuntimePlatform == Device.iOS)
                ConfigureViewToiOS();

            if (Device.RuntimePlatform == Device.Android)
                ConfigureViewToAndroid();

            headerTitleText.Text = PageTitle;

            backButton.IsVisible = (BackButtonAction != NavigationBackButtonAction.None);


            if (changingMargin)
            {
                headerTitleText.Margin = titleMargin;
                backButton.Margin = arrowMargin;
            }


        }

        /// <summary>
        /// Configure all header styles for platform iOS
        /// </summary>
        void ConfigureViewToiOS()
        {

            RefreshBackgroundImage();


            if (PaddingTop != 0)
            {
                this.Margin = new Thickness(0, PaddingTop, 0, 0);
            }
            else
            {
                this.Margin = new Thickness(0, paddingTopToiOS, 0, 0);
            }


            this.HeightRequest = maximunHeight;
            //this.headerTitleText.Margin = new Thickness(0, 20, 0, 0);
            //this.backButton.Margin = new Thickness(0, 20, 0, 0);
        }

        /// <summary>
        /// Configure all header styles for platform Android
        /// </summary>
        void ConfigureViewToAndroid()
        {
            RefreshBackgroundImage();
            this.Margin = new Thickness(0, paddingTopToAndroid, 0, 0);
            //todo: saccomani -  configure view for all android devices here
            this.backButton.Margin = new Thickness(0, 10, 0, 0);
        }

        /// <summary>
        /// Change all images that need a special size
        /// </summary>
        void RefreshBackgroundImage()
        {
            var screenSize = Application.Current.MainPage.Width;

            //414 -- iphone 6/s
            if (true)//screen bigger than phone, change the image
            {
                //backgroundImage.Source = "...";
            }
        }

        void BackButton_Clicked(object sender, System.EventArgs e)
        {
            BackButtonPressedEvent?.Invoke(BackButtonAction);
        }

        public void SetPageTitle(string title)
        {
            headerTitleText.Text = title;
        }

        public void SetFontSize(double headerFontSize)
        {
            this.headerTitleText.FontSize = headerFontSize;
        }
    }
}
