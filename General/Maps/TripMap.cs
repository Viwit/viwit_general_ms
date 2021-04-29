using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class TripMap
    {
        public TripMap(EntityTypeBuilder<Trip> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.IdTrip, x.Route_IdRoute, x.Bus_LicensePlateBus, x.Driver_DriversLicense });
            entityBuilder.ToTable("Trip");

            entityBuilder.Property(x => x.IdTrip).HasColumnName("IdTrip");
            entityBuilder.Property(x => x.Route_IdRoute).HasColumnName("Route_IdRoute");
            entityBuilder.Property(x => x.Bus_LicensePlateBus).HasColumnName("Bus_LicensePlateBus");
            entityBuilder.Property(x => x.Driver_DriversLicense).HasColumnName("Driver_DriversLicense");
            entityBuilder.Property(x => x.BusStop_IdBusStop).HasColumnName("BusStop_IdBusStop");
            entityBuilder.Property(x => x.CurrentTripOccupation).HasColumnName("CurrentTripOccupation");
            entityBuilder.Property(x => x.StartDate).HasColumnName("StartDate");
            entityBuilder.Property(x => x.TripStatus).HasColumnName("TripStatus");
            entityBuilder.Property(x => x.TotalPassengersTrip).HasColumnName("TotalPassengersTrip");
        }
    }
}
