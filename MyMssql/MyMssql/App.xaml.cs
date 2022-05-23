using MyMssql.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMssql
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new NotesList());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
