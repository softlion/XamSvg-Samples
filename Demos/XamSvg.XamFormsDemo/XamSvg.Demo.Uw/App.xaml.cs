using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamSvg.Demo.Uw;
using XamSvg.Shared;
using XamSvg.XamForms;
using XamSvg.XamForms.Uw;
using Application = Windows.UI.Xaml.Application;
using Frame = Windows.UI.Xaml.Controls.Frame;

//Does not work in release mode ?
[assembly:ExportRenderer(typeof(SvgImage), typeof(SvgImageRenderer))]
[assembly: Dependency(typeof(SvgLogger))]

namespace XamSvg.Demo.Uw
{
    class SvgLogger : ILogger
    {
        public bool TraceEnabled { get; set; } = true;

        public void Trace(Func<string> s, string method = null, int lineNumber = 0)
        {
            Debug.WriteLine($"SvgTrace {method}:{lineNumber}: {s()}");
        }
    }


    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                //Initialize the cross platform color helper
                XamSvg.Setup.InitSvgLib();

                //Tells XamSvg in which assembly to search for svg when "res:" is used
                XamSvg.Shared.Config.ResourceAssembly = typeof(App).GetTypeInfo().Assembly;

                Xamarin.Forms.Forms.Init(e);

                ForceRegisterRenderer();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        private static void ForceRegisterRenderer()
        {
            try
            {
                var obj = typeof(View).GetTypeInfo().Assembly.GetType("Xamarin.Forms.Registrar").GetRuntimeProperties().FirstOrDefault(p => p.Name == "Registered").GetValue(null);
                var dic = obj.GetType().GetRuntimeFields().FirstOrDefault(p => p.Name == "_handlers").GetValue(obj) as Dictionary<Type, Type>;
                if (!dic.ContainsKey(typeof(SvgImage)))
                    dic.Add(typeof(SvgImage), typeof(SvgImageRenderer));
            }
            catch (Exception)
            {
                throw new Exception("SvgImageRenderer is not registered (bug in Xamarin.Forms). Fix it by adding this line to your startup project directly below the 'usings' definitions: [assembly: ExportRenderer(typeof(SvgImage), typeof(SvgImageRenderer))]");
            }
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
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
