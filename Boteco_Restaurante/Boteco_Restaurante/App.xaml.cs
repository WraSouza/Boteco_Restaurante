using System;
using Xamarin.Forms;
using Boteco_Restaurante.View;
using Xamarin.Forms.Xaml;

namespace Boteco_Restaurante
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new View.MainView();
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
