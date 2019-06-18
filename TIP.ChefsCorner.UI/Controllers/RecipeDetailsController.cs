using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIP.ChefsCorner.BL;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace TIP.ChefsCorner.UI.Controllers
{
    public class RecipeDetailsController : Controller
    {
        public async Task<ActionResult> Details(int id)
        {

            using (var webclient = new WebClient())
            {
                string myurl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/informationBulk?ids=";
                webclient.Headers[HttpRequestHeader.Accept] = "application/json";
                webclient.Headers["X-RapidAPI-Key"] = "037ed14f35msh15f3f745fd822dbp10e5b8jsn21f31374305f";
               
                string json = await webclient.DownloadStringTaskAsync(myurl + id);
            
               var recipeDetails = RecipeDetails.FromJson(json);

                
               

                    return View(recipeDetails);
           


            }
        }   }
}