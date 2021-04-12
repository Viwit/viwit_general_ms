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
            return _context.Trips.Where(b => b.IdTrip.Contains("")).Select((c) => new
            {
                IdTrip = c.IdTrip,
                IdRoute_Route = c.IdRoute_Route,
                LicensePlateBus_Bus = c.LicensePlateBus_Bus,
                DriversLicense_Driver = c.DriversLicense_Driver,
                IdBusStop_BusStop = c.IdBusStop_BusStop,
                CurrentTripOccupation = c.CurrentTripOccupation,
                StartDate = c.StartDate,
                TripStatus = c.TripStatus
            }).ToList();
        }

        [HttpGet("Route/{IdRoute_Route}/LicensePlateBus/{LicensePlateBus_Bus}/DriversLicense/{DriversLicense_Driver}")]
        public object GetTripAccordingToRouteBusDriver(int IdRoute_Route, string LicensePlateBus_Bus, string DriversLicense_Driver)
        {
            return _context.Trips.Where(b => b.IdRoute_Route.Equals(IdRoute_Route) 
            & b.LicensePlateBus_Bus.Equals(LicensePlateBus_Bus) 
            & b.DriversLicense_Driver.Equals(DriversLicense_Driver)).Select((c) => new
            {
                IdTrip = c.IdTrip,
                IdRoute_Route = c.IdRoute_Route,
                LicensePlateBus_Bus = c.LicensePlateBus_Bus,
                DriversLicense_Driver = c.DriversLicense_Driver,
                IdBusStop_BusStop = c.IdBusStop_BusStop,
                CurrentTripOccupation = c.CurrentTripOccupation,
                StartDate = c.StartDate,
                TripStatus = c.TripStatus
            }).ToList();
        }

        [HttpGet("{IdTrip}")]
        public async Task<ActionResult<Trip>> GetTrip(string IdTrip)
        {
            var trip = await _context.Trips.FindAsync(IdTrip);

            if (trip == null)
            {
                return NotFound();
            }

            return trip_ToDTO(trip);
        }

        [HttpPut("{IdTrip}")]
        public async Task<ActionResult<Trip>> Updatebus(string IdTrip, Trip trip)
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
                tripAux.IdRoute_Route = trip.IdRoute_Route;
                tripAux.LicensePlateBus_Bus = trip.LicensePlateBus_Bus;
                tripAux.DriversLicense_Driver = trip.DriversLicense_Driver;
                tripAux.IdBusStop_BusStop = trip.IdBusStop_BusStop;
                tripAux.CurrentTripOccupation = trip.CurrentTripOccupation;
                tripAux.StartDate = trip.StartDate;
                tripAux.TripStatus = trip.TripStatus;

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
        public async Task<ActionResult<Trip>> Createbus(Trip trip)
        {
            var tripAux = new Trip
            {
                IdTrip = trip.IdTrip,
                IdRoute_Route = trip.IdRoute_Route,
                LicensePlateBus_Bus = trip.LicensePlateBus_Bus,
                DriversLicense_Driver = trip.DriversLicense_Driver,
                IdBusStop_BusStop = trip.IdBusStop_BusStop,
                CurrentTripOccupation = trip.CurrentTripOccupation,
                StartDate = trip.StartDate,
                TripStatus = trip.TripStatus
            };

            _context.Trips.Add(tripAux);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTrip),
                new { IdTrip = trip.IdTrip },
                trip_ToDTO(trip));
        }

        [HttpDelete("{IdTrip}")]
        public async Task<ActionResult<Trip>> Deletebus(string IdTrip)
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

        private bool tripExists(string IdTrip) =>
             _context.Trips.Any(e => e.IdTrip == IdTrip);

        private static Trip trip_ToDTO(Trip trip) =>
            new Trip
            {
                IdTrip = trip.IdTrip,
                IdRoute_Route = trip.IdRoute_Route,
                LicensePlateBus_Bus = trip.LicensePlateBus_Bus,
                DriversLicense_Driver = trip.DriversLicense_Driver,
                IdBusStop_BusStop = trip.IdBusStop_BusStop,
                CurrentTripOccupation = trip.CurrentTripOccupation,
                StartDate = trip.StartDate,
                TripStatus = trip.TripStatus
            };
    }
}
