using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCenterImplementation.Models;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;

namespace AppCenterImplementation.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public int n;
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send(this, "AddItem", Item);
                await Navigation.PopModalAsync();
                Analytics.TrackEvent("Save_Clicked");
                throw FormatException();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private Exception FormatException()
        {
            throw new NotImplementedException();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync();
                Analytics.TrackEvent("Cancel_Clicked");

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}