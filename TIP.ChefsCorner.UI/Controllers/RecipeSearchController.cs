using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TIP.ChefsCorner.BL;


namespace TIP.ChefsCorner.UI.Controllers
{
    public class RecipeSearchController : Controller
    {

            public async Task<ActionResult> Index(string recipe)
            {
            List<RecipeSearch> recipeSearch = new List<RecipeSearch>();  
            
                using (var webclient = new WebClient())
                {
                string myurl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?number=15&ingredients=";
                webclient.Headers[HttpRequestHeader.Accept] = "application/json";
                webclient.Headers["X-RapidAPI-Key"] = "037ed14f35msh15f3f745fd822dbp10e5b8jsn21f31374305f";
                string json = await webclient.DownloadStringTaskAsync(myurl + recipe);
                recipeSearch = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RecipeSearch>>(json);
                //need to see if success>
                if (recipeSearch.Count == 0 && recipe != null)
                {
                    ViewBag.Message = "Sorry No Recipe match those ingredients";
                    // TODO: display a message back that there aren't any substitutions
                    return View(recipeSearch);

                }
                else
                {
                    return View(recipeSearch);
                }


            }
            }
       
        

    }
}




   