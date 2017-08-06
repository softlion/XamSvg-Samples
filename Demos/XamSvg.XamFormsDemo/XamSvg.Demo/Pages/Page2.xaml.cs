using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamSvg.Demo
{
    public partial class Page2 : ContentPage
    {
        private CancellationTokenSource cancelSource = new CancellationTokenSource();

        public Page2()
        {
            InitializeComponent();
        }

        private void Start_OnClicked(object sender, EventArgs e)
        {
            cancelSource.Cancel();
            cancelSource = new CancellationTokenSource();
            var unused = StartAnimation(cancelSource.Token);
        }

        private void Stop_OnClicked(object sender, EventArgs e)
        {
            cancelSource.Cancel();
        }

        private async Task StartAnimation(CancellationToken cancel)
        {
            var totalSeconds = 10.0;
            var movePerSeconds = 100;
            var increment = 100/ totalSeconds / movePerSeconds;

            while (!cancel.IsCancellationRequested)
            {
                var p = TheProgress.Progress + increment;
                if (p > 100)
                    p = 0;
                TheProgress.Progress = p;

                var seconds = ((int)(totalSeconds * p/100.0)).ToString();
                if(Seconds.Text != seconds)
                    Seconds.Text = seconds;

                try
                {
                    await Task.Delay(1000 / movePerSeconds, cancel);
                }
                catch (TaskCanceledException)
                {
                }
            }
        }
    }
}
