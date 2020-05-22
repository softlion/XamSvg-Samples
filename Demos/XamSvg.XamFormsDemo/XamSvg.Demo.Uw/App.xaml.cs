using System;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Application = Windows.UI.Xaml.Application;
using Frame = Windows.UI.Xaml.Controls.Frame;

namespace XamSvg.Demo.Uw
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
                this.DebugSettings.EnableFrameRateCounter = true;
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                #region XamSvg init
                //Tells XamSvg in which assembly to search for svg when "res:" is used
                //XamSvg.Shared.Config.ResourceAssemblies = new List<Assembly> { typeof(App).GetTypeInfo().Assembly };
                XamSvg.Shared.Config.ResourceAssembly = typeof(App).GetTypeInfo().Assembly;
                #endregion

                Xamarin.Forms.Forms.Init(e);

                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
