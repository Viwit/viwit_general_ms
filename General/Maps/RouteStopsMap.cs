using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using General.Models;

namespace General.Maps
{
    public class RouteStopsMap
    {
        public RouteStopsMap(EntityTypeBuilder<RouteStops> entityBuilder)
        {
            entityBuilder.HasKey(x => new { x.IdRoute_Route, x.IdBusStop_BusStop });
            entityBuilder.ToTable("RouteStops");

            entityBuilder.Property(x => x.IdRoute_Route).HasColumnName("IdRoute_Route");
            entityBuilder.Property(x => x.IdBusStop_BusStop).HasColumnName("IdBusStop_BusStop");
        }
    }
}