using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class Trip
    {
        public Trip()
        {
        }

        public string IdTrip { get; set; }

        public int IdRoute_Route { get; set; }

        public string LicensePlateBus_Bus { get; set; }

        public string DriversLicense_Driver { get; set; }

        public int IdBusStop_BusStop { get; set; }

        public int CurrentTripOccupation { get; set; }

        public string StartDate { get; set; }

        public string TripStatus { get; set; }
    }
}
