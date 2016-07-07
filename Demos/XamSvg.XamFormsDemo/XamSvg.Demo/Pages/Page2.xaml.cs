using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var dontWait = StartAnimation(cancelSource.Token);
        }

        private void Stop_OnClicked(object sender, EventArgs e)
        {
            cancelSource.Cancel();
        }

        private async Task StartAnimation(CancellationToken cancel)
        {
            var movePerSeconds = 10;
            var increment = 100/60.0/movePerSeconds;

            while (!cancel.IsCancellationRequested)
            {
                var p = TheProgress.Progress + increment;
                if (p > 100)
                    p = 0;
                TheProgress.Progress = p;

                var seconds = ((int)(60*p/100.0)).ToString();
                if(Seconds.Text != seconds)
                    Seconds.Text = seconds;

                await Task.Delay(1000/movePerSeconds); //Don't use the version with the cancel overload, as it throws an exception and we don't want 4 try/catch lines
            }
        }
    }
}
