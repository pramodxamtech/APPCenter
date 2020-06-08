using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCenterImplementation.Models;
using AppCenterImplementation.Views;
using AppCenterImplementation.ViewModels;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace AppCenterImplementation.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            try
            {
                var item = args.SelectedItem as Item;
                if (item == null)
                    return;

                await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

                // Manually deselect item.
                ItemsListView.SelectedItem = null;
                Analytics.TrackEvent("ItemsPage_OnItemSelected");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));

                Analytics.TrackEvent("ItemsPage_AddItem_Clicked");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (viewModel.Items.Count == 0)
                    viewModel.LoadItemsCommand.Execute(null);
                Analytics.TrackEvent("ItemsPage_OnAppearing");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}