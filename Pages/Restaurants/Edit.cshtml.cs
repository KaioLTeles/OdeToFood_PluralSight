using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;
using OdeToFood.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData RestaurantData;

        public IEnumerable<SelectListItem> Cuisines { get; set; }
        
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        
        private readonly IHtmlHelper HtmlHelper;
        
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            RestaurantData =  restaurantData;
            HtmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();
            if(restaurantId.HasValue)
            {
                Restaurant = RestaurantData.GetById(restaurantId.Value);            
            }
            else
            {
                Restaurant = new Restaurant();

            }            
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFoud");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {               
                Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();           
                return Page();                
            }

            if(Restaurant.Id > 0)
            {
                TempData["Message"] = "Restaurant updated!";
                RestaurantData.Update(Restaurant);                
            }
            else
            {
                TempData["Message"] = "Restaurant saved!";
                RestaurantData.Add(Restaurant); 
            }
            
            RestaurantData.Commit();           
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
