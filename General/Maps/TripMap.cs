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
            entityBuilder.HasKey(x => new { x.IdTrip, x.IdRoute_Route, x.LicensePlateBus_Bus, x.DriversLicense_Driver });
            entityBuilder.ToTable("Trip");

            entityBuilder.Property(x => x.IdTrip).HasColumnName("IdTrip");
            entityBuilder.Property(x => x.IdRoute_Route).HasColumnName("IdRoute_Route");
            entityBuilder.Property(x => x.LicensePlateBus_Bus).HasColumnName("LicensePlateBus_Bus");
            entityBuilder.Property(x => x.DriversLicense_Driver).HasColumnName("DriversLicense_Driver");
            entityBuilder.Property(x => x.IdBusStop_BusStop).HasColumnName("IdBusStop_BusStop");
            entityBuilder.Property(x => x.CurrentTripOccupation).HasColumnName("CurrentTripOccupation");
            entityBuilder.Property(x => x.StartDate).HasColumnName("StartDate");
            entityBuilder.Property(x => x.TripStatus).HasColumnName("TripStatus");
        }
    }
}
