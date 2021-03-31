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
    public class RouteController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public RouteController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object GetRoute()
        {
            return _context.Routes.Where(b => b.IdRoute >= 0).Select((c) => new
            {
                IdRoute = c.IdRoute,
                NameRoute = c.NameRoute,
                InitialBusStop = c.InitialBusStop,
                FinalBusStop = c.FinalBusStop,
                ApproximateDuration = c.ApproximateDuration
            }).ToList();
        }

        [HttpGet("{IdRoute}")]
        public async Task<ActionResult<Route>> GetRoute(int IdRoute)
        {
            var route = await _context.Routes.FindAsync(IdRoute);

            if (route == null)
            {
                return NotFound();
            }

            return route_ToDTO(route);
        }

        [HttpPut("{IdRoute}")]
        public async Task<IActionResult> UpdateRoute(int IdRoute, Route route)
        {
            if (IdRoute != route.IdRoute)
            {
                return BadRequest();
            }

            var routeAux = await _context.Routes.FindAsync(IdRoute);
            if (routeAux == null)
            {
                return NotFound();
            }

            routeAux.IdRoute = route.IdRoute;
            routeAux.NameRoute = route.NameRoute;
            routeAux.InitialBusStop = route.InitialBusStop;
            routeAux.FinalBusStop = route.FinalBusStop;
            routeAux.ApproximateDuration = route.ApproximateDuration;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception) when (!routeExists(IdRoute))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Route>> CreateRoute(Route route)
        {
            var routeAux = new Route
            {
                IdRoute = route.IdRoute,
                NameRoute = route.NameRoute,
                InitialBusStop = route.InitialBusStop,
                FinalBusStop = route.FinalBusStop,
                ApproximateDuration = route.ApproximateDuration
            };

            _context.Routes.Add(routeAux);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRoute),
                new { IdRoute = route.IdRoute },
                route_ToDTO(route));
        }

        [HttpDelete("{IdRoute}")]
        public async Task<IActionResult> DeleteRoute(int IdRoute)
        {
            var route = await _context.Routes.FindAsync(IdRoute);

            if (route == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool routeExists(int IdRoute) =>
             _context.Routes.Any(e => e.IdRoute == IdRoute);

        private static Route route_ToDTO(Route route) =>
            new Route
            {
                IdRoute = route.IdRoute,
                NameRoute = route.NameRoute,
                InitialBusStop = route.InitialBusStop,
                FinalBusStop = route.FinalBusStop,
                ApproximateDuration = route.ApproximateDuration
            };
    }
}