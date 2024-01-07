using System;
using System.Net.Http;
using System.Threading;
//using Microsoft.Maui.Essentials;
using TheWatch.Maui.Data;

namespace TheWatch.Maui.Services
{
    public interface ILocationService
    {
        Task<Location> GetLocationAsync();
        event EventHandler<Location> LocationChanged;
    }

    public class LocationService : ILocationService
    {
        private Timer _timer;
        private Location _lastLocation;
        private const double LocationChangeThreshold = 10.0; // in meters
        private const int Interval = 300000; // 5 minutes

        public LocationService()
        {
            _timer = new Timer(TimerCallback, null, Interval, Interval);
        }

        private async void TimerCallback(object state)
        {
            var currentLocation = await GetLocationAsync();

            if (HasLocationChanged(currentLocation))
            {
                LocationChanged?.Invoke(this, currentLocation);
                _lastLocation = currentLocation;
            }
        }

        public async Task<Location> GetLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                    return location;

                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                return location;
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        public event EventHandler<Location> LocationChanged;

        private bool HasLocationChanged(Location currentLocation)
        {
            if (_lastLocation == null) return true;

            return Location.CalculateDistance(_lastLocation, currentLocation, DistanceUnits.Kilometers) > LocationChangeThreshold;
        }
    }
}
