using System.Linq;
using System.Reflection;

using Xamarin.Forms;
using XamSvg.Demo.Pages;

namespace XamSvg.Demo
{
    public class App : Application
    {
        public App()
        {
            XamSvg.Shared.Config.License = "eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJ1bmlxdWVfbmFtZSI6ImU1ZjRmODZlOGY4OTRjOTI4MmFkZDMyMWNjZTVkYjgxIiwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvTWF4QnVpbGREYXRlQ2xhaW0iOiIyMDIxLTA1LTEzVDA4OjAxOjE2LjYzNjc2MiswMjowMCIsImh0dHBzOi8vc2NoZW1hcy52YXBvbGlhLmV1LzIwMjAvMDUvY2xhaW1zL1Byb2R1Y3RDb2RlQ2xhaW0iOlsieGFtc3ZnIiwieGFtc3ZnZm9ybXMiXSwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvQXBwSWRDbGFpbSI6WyJmci52YXBvbGlhLnN2Z3Rlc3QiLCJmci52YXBvbGlhLnN2Z2Zvcm10ZXN0IiwieGFtc3ZnLmRyb2lkLnRlc3RzIiwiWGFtU3ZnLkRlbW8uRHJvaWQiXSwiaHR0cHM6Ly9zY2hlbWFzLnZhcG9saWEuZXUvMjAyMC8wNS9jbGFpbXMvT3NDbGFpbSI6WyJpb3MiLCJhbmRyb2lkIiwidXdwIl0sIm5iZiI6MTU4OTM0OTY3NiwiZXhwIjoxOTA0ODgyNDc2LCJpYXQiOjE1ODkzNDk2NzYsImlzcyI6Imh0dHBzOi8vdmFwb2xpYS5ldS9hdXRob3JpdHkiLCJhdWQiOiJodHRwczovL3ZhcG9saWEuZXUvYXV0aG9yaXR5L2xpY2Vuc2VzIn0.r9SLG24WPQM7mgWNXBP-51IHSYdNcuAMNN8vhWP5hYWip8dWzUQRvI7U0D2z5-w8i8WTrbwkFc3s0R8plF7SB02CeXzTYEDmYhu-tUWnicC_0OrEsfmsQK0HyUyd8jEaehNH7IB5EpgwPG9-8k2RbsXg0803uacnjx7WoEYTwdb8vpxVuCHi9opCReHHL1gztElFN1aXwHbiyle_AqsX9seBKFKQxgi5jXWFSi4blGuwLEe44GWnzyJAAZQcK_jYUDC2PGkcVFBDeyIROmPAmq4_4nEeYrQWF80tPmsbqHNcqR9_lwZUi_ZThtrc-iCwfIIY-r8DFFDP_hnqTmXIkg";
            var assembly = typeof (App).GetTypeInfo().Assembly;
            XamSvg.Shared.Config.ResourceAssembly = assembly;

            MainPage = new NavigationPage(new TabContainer());
        }
    }

    public static class MessagingCenterConst
    {
        public const string OpenDeepLink = nameof(OpenDeepLink);
    }
}