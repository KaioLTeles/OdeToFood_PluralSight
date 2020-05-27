using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;
using OdeToFood.Core;
using Microsoft.Extensions.Logging;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData RestaurantData;
        public IEnumerable<Restaurant> Restaurants { get; set; }
        
        [BindProperty(SupportsGet=true)]
        public string SearchTerm { get; set; }        
        [TempData]
        public string Message { get; set; }
        private readonly ILogger<ListModel> Logger;
        public ListModel(IRestaurantData restaurantData,
                        ILogger<ListModel> logger)
        {
            RestaurantData = restaurantData;
            Logger = logger;
        }

        public void OnGet()
        {                       
            Logger.LogError("Executing List Model");
            Restaurants = RestaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
