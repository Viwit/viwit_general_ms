using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using General.Controllers;
using General.Models;

namespace General.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public TripController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetTrip()
        {
            return _context.Trips.Where(b => 1 > 0).Select((c) => new
            {
                IdTrip = c.IdTrip,
                Route_IdRoute = c.Route_IdRoute,
                Bus_LicensePlateBus = c.Bus_LicensePlateBus,
                Driver_DriversLicense = c.Driver_DriversLicense,
                BusStop_IdBusStop = c.BusStop_IdBusStop,
                CurrentTripOccupation = c.CurrentTripOccupation,
                StartDate = c.StartDate,
                TripStatus = c.TripStatus,
                TotalPassengersTrip = c.TotalPassengersTrip
            }).ToList();
        }

        [HttpGet("Route/{Route_IdRoute}/LicensePlateBus/{Bus_LicensePlateBus}/DriversLicense/{Driver_DriversLicense}")]
        public object GetTripAccordingToRouteBusDriver(int Route_IdRoute, string Bus_LicensePlateBus, string Driver_DriversLicense)
        {
            return _context.Trips.Where(b => b.Route_IdRoute.Equals(Route_IdRoute) 
            & b.Bus_LicensePlateBus.Equals(Bus_LicensePlateBus) 
            & b.Driver_DriversLicense.Equals(Driver_DriversLicense)).Select((c) => new
            {
                IdTrip = c.IdTrip,
                Route_IdRoute = c.Route_IdRoute,
                BusStop_IdBusStop = c.BusStop_IdBusStop,
                Driver_DriversLicense = c.Driver_DriversLicense,
                Bus_LicensePlateBus = c.Bus_LicensePlateBus,
                CurrentTripOccupation = c.CurrentTripOccupation,
                StartDate = c.StartDate,
                TripStatus = c.TripStatus,
                TotalPassengersTrip = c.TotalPassengersTrip
            }).ToList();
        }

        [HttpGet("{IdTrip}")]
        public async Task<ActionResult<Trip>> GetTrip(int IdTrip)
        {
            var trip = await _context.Trips.FindAsync(IdTrip);

            if (trip == null)
            {
                return NotFound();
            }

            return trip_ToDTO(trip);
        }

        [HttpPut("{IdTrip}")]
        public async Task<ActionResult<Trip>> Updatebus(int IdTrip, Trip trip)
        {
            if (IdTrip != trip.IdTrip)
            {
                return BadRequest();
            }

            var tripAux = await _context.Trips.FindAsync(IdTrip);
            if (tripAux == null)
            {
                return NotFound();
            }

            
                tripAux.IdTrip = trip.IdTrip;
                tripAux.Route_IdRoute = trip.Route_IdRoute;
                tripAux.BusStop_IdBusStop = trip.BusStop_IdBusStop;
                tripAux.Driver_DriversLicense = trip.Driver_DriversLicense;
                tripAux.Bus_LicensePlateBus = trip.Bus_LicensePlateBus;
                tripAux.CurrentTripOccupation = trip.CurrentTripOccupation;
                tripAux.StartDate = trip.StartDate;
                tripAux.TripStatus = trip.TripStatus;
                tripAux.TotalPassengersTrip = trip.TotalPassengersTrip;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!tripExists(IdTrip))
            {
                return NotFound();
            }

            return trip_ToDTO(tripAux);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> Createbus(Trip trip)
        {
            var tripAux = new Trip
            {
                Route_IdRoute = trip.Route_IdRoute,
                BusStop_IdBusStop = trip.BusStop_IdBusStop,
                Driver_DriversLicense = trip.Driver_DriversLicense,
                Bus_LicensePlateBus = trip.Bus_LicensePlateBus,
                CurrentTripOccupation = trip.CurrentTripOccupation,
                StartDate = trip.StartDate,
                TripStatus = trip.TripStatus,
                TotalPassengersTrip = trip.TotalPassengersTrip
            };

            _context.Trips.Add(tripAux);
            await _context.SaveChangesAsync();

            var message = new Message("Trip successfully added");

            return message;
        }

        [HttpDelete("{IdTrip}")]
        public async Task<ActionResult<Trip>> Deletebus(int IdTrip)
        {
            var trip = await _context.Trips.FindAsync(IdTrip);

            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return trip_ToDTO(trip);
        }

        private bool tripExists(int IdTrip) =>
             _context.Trips.Any(e => e.IdTrip == IdTrip);

        private static Trip trip_ToDTO(Trip trip) =>
            new Trip
            {
                IdTrip = trip.IdTrip,
                Route_IdRoute = trip.Route_IdRoute,
                BusStop_IdBusStop = trip.BusStop_IdBusStop,
                Driver_DriversLicense = trip.Driver_DriversLicense,
                Bus_LicensePlateBus = trip.Bus_LicensePlateBus,
                CurrentTripOccupation = trip.CurrentTripOccupation,
                StartDate = trip.StartDate,
                TripStatus = trip.TripStatus,
                TotalPassengersTrip = trip.TotalPassengersTrip
            };
    }
}
