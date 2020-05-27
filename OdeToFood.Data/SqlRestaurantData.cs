using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext Db;
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            Db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            Db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return Db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var Restaurant = GetById(id);
            if(Restaurant != null)
            {
                Db.Restaurant.Remove(Restaurant);
            }
            return Restaurant;
        }

        public Restaurant GetById(int id)
        {
            return Db.Restaurant.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in Db.Restaurant
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;

             return query;                       
        }               

        public Restaurant Update(Restaurant updateRestaurant)
        {
            var entity = Db.Restaurant.Attach(updateRestaurant);
            entity.State = EntityState.Modified;
            return updateRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return Db.Restaurant.Count();
        }
    }
}