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
    public class IngredientSubstitutionController : Controller
    {
       
        public async Task<ActionResult> Index(string ingredient)
        {
            if (ingredient != "" && ingredient != null)
            {

            IngredientSubstitutions substititueIngredients = new IngredientSubstitutions();

            using (var webclient = new WebClient())
            {
                string myurl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/food/ingredients/substitutes?ingredientName=";
                webclient.Headers[HttpRequestHeader.Accept] = "application/json";
                webclient.Headers["X-RapidAPI-Key"] = "037ed14f35msh15f3f745fd822dbp10e5b8jsn21f31374305f";
                string json = await webclient.DownloadStringTaskAsync(myurl + ingredient);
                substititueIngredients = Newtonsoft.Json.JsonConvert.DeserializeObject<IngredientSubstitutions>(json);
                //need to see if success
                if (substititueIngredients.status == "failure")
                {
                   
                        // TODO: display a message back that there aren't any substitutions
                         ViewBag.Message = "Sorry there wasn't a substitution found!";
                        return View();
                }
                else
                {
                    return View(substititueIngredients);
                }


            }
        }
            return View();
        }
    }
}




   