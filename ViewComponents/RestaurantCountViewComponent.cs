using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;

namespace OdeToFood.ViewComponents
{
    public class RestaurantCountViewComponent 
                : ViewComponent
    {
        private readonly IRestaurantData RestaurantData;
        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            RestaurantData = restaurantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = RestaurantData.GetCountOfRestaurants();
            return View(count);
        }
    }
}