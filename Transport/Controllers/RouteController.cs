using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Transport.Models;
using Transport.Models.Domain;

namespace Transport.Controllers
{
    public class RouteController : Controller
    {
        private readonly TransportDbContext transportDbContext;
        public RouteController(TransportDbContext transportDbContext) {
            this.transportDbContext = transportDbContext;
        }

		[HttpGet]
		public async Task<IActionResult> RouteView()
		{
			var routes = await transportDbContext.Routes.ToListAsync();
            return View(routes);
		}

		[HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRouteViewModel addRouteRequest)
        {
            var route = new Models.Domain.Route()
            {
                Id = Guid.NewGuid(),
                Name = addRouteRequest.Name,
                Url = addRouteRequest.Url
            };

            await transportDbContext.Routes.AddAsync(route);
            await transportDbContext.SaveChangesAsync();
            return RedirectToAction("RouteView");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var route = await transportDbContext.Routes.FirstOrDefaultAsync(x => x.Id == id);
            
            if (route != null)
            {
                var viewModel = new UpdateRouteViewModel()
                {
                    Id = route.Id,
                    Name = route.Name,
                    Url = route.Url
                };
                return View(viewModel);

            }

            return RedirectToAction("RouteView");
        }

    }
}
