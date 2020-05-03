using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WhackABox
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<Game, GameStats>(this,"stats_updated", StatsUpdated);
        }
        private void StatsUpdated(Game sender, GameStats stats)
        {
            boxCountLabel.Text = stats.NumberOfBoxes.ToString();
            planeCountLabel.Text = stats.NumberOfPlanes.ToString();
        }
    }
}
