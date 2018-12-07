using MaraudeurMap.Service;
using TodoREST;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomPinREST
{
    public class App : Application
    {
        public static CustomPinManager customPinManager { get; private set; }

       // public static ITextToSpeech Speech { get; set; }

        public App()
        {
            customPinManager = new CustomPinManager (new RestService ());
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

