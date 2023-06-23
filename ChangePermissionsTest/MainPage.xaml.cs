using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Forms;

namespace ChangePermissionsTest
{
	public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Permissions();
			Timer();
		}

        private async void Permissions()
        {
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
				if (status != PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
					{
						await DisplayAlert("Геолокация", "Права на использование геолокации изменились", "OK");
					}

					status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private void Timer()
		{
			Device.StartTimer(TimeSpan.FromMinutes(0.2), () =>
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
					if (status != PermissionStatus.Granted)
					{
						await DisplayAlert("","Прав нету", "OK");
					}
					else
					{
						await DisplayAlert("", "Права есть", "OK");
					}
				});
				return true;
			});
		}
    }
}
