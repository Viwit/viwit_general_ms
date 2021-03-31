using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Models
{
    public class RouteStops
    {
        public RouteStops()
        {
        }

        public int IdRoute_Route { get; set; }

        public int IdBusStop_BusStop { get; set; }
        
    }
}
