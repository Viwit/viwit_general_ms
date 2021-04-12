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
    public class RouteStopsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RouteStopsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetRouteStops()
        {
            return _context.RouteStops_s.Where(b => b.IdRoute_Route >= 0).Select((c) => new
            {
                IdRoute_Route = c.IdRoute_Route,
                IdBusStop_BusStop = c.IdBusStop_BusStop
            }).ToList();
        }

        [HttpGet("Route/{IdRoute_Route}/BusStop/{IdBusStop_BusStop}")]
        public async Task<ActionResult<RouteStops>> GetRouteStopsAccoidingToIdRouteAndIdStop(int IdRoute_Route, int IdBusStop_BusStop)
        {
            var routeStops = await _context.RouteStops_s.FindAsync(IdRoute_Route, IdBusStop_BusStop);

            if (routeStops == null)
            {
                return NotFound();
            }

            return routeStops_ToDTO(routeStops);
        }

        [HttpPut("{IdRoute_Route}")]
        public async Task<ActionResult<RouteStops>> UpdateRouteStops(int IdRoute_Route, RouteStops routeStops)
        {
            if (IdRoute_Route != routeStops.IdRoute_Route)
            {
                return BadRequest();
            }

            var routeStopsAux = await _context.RouteStops_s.FindAsync(IdRoute_Route);
            if (routeStopsAux == null)
            {
                return NotFound();
            }

            routeStopsAux.IdRoute_Route = routeStops.IdRoute_Route;
            routeStopsAux.IdBusStop_BusStop = routeStops.IdBusStop_BusStop;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!routesStopsExists(IdRoute_Route))
            {
                return NotFound();
            }

            return routeStops_ToDTO(routeStopsAux);
        }

        [HttpPost]
        public async Task<ActionResult<RouteStops>> CreateRouteStops(RouteStops routeStops)
        {
            var routeStopsAux = new RouteStops
            {
                IdRoute_Route = routeStops.IdRoute_Route,
                IdBusStop_BusStop = routeStops.IdBusStop_BusStop
            };

            _context.RouteStops_s.Add(routeStopsAux);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRouteStops),
                new { IdRoute_Route = routeStops.IdRoute_Route },
                routeStops_ToDTO(routeStops));
        }

        [HttpDelete("Route/{IdRoute_Route}/BusStop/{IdBusStop_BusStop}")]
        public async Task<ActionResult<RouteStops>> DeleteRouteStopsAccordingToIdRouteAndIdStop(int IdRoute_Route, int IdBusStop_BusStop)
        {
            var routeStops = await _context.RouteStops_s.FindAsync(IdBusStop_BusStop, IdRoute_Route);

            if (routeStops == null)
            {
                return NotFound();
            }

            _context.RouteStops_s.Remove(routeStops);
            await _context.SaveChangesAsync();

            return routeStops_ToDTO(routeStops);
        }

        private bool routesStopsExists(int IdRoute_Route) =>
             _context.RouteStops_s.Any(e => e.IdRoute_Route == IdRoute_Route);

        private static RouteStops routeStops_ToDTO(RouteStops routeStops) =>
            new RouteStops
            {
                IdRoute_Route = routeStops.IdRoute_Route,
                IdBusStop_BusStop = routeStops.IdBusStop_BusStop
            };
    }
}
