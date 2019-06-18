using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIP.ChefsCorner.PL;
using System.ComponentModel;

namespace TIP.ChefsCorner.BL
{
    public class Ingredient
    {
        // Declare variables
        public int Id { get; set; }
        public string Description { get; set; }

        // Instantiate the class
        public Ingredient()
        {

        }

        public Ingredient(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public void LoadById(int id)
        {
            try
            {
                //Using connection string statement and creating new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    tblIngredient ingredient = dc.tblIngredients.Where(i => i.in_Id == id).FirstOrDefault();
                    if (ingredient != null)
                    {
                        this.Id = ingredient.in_Id;
                        this.Description = ingredient.in_Description;
                    }
                    else
                    {
                        throw new Exception("Ingredient not found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Insert()
        {
            // Create result variable for this method (int)
            int result = 0;

            try
            {
                //Using connection string statement and creating new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // Create new instance of tblIngredient
                    tblIngredient ingredient = new tblIngredient();

                    ingredient.in_Id = 1;
                    if (dc.tblIngredients.Any())
                        ingredient.in_Id = dc.tblIngredients.Max(i => i.in_Id) + 1;
                    this.Id = ingredient.in_Id;
                    ingredient.in_Description = this.Description;
                    dc.tblIngredients.Add(ingredient);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Update()
        {
            // Create result variable for this method(int)
            int result = 0;
            try
            {
                //Using connection string statement and creating new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // If ingredient is selected, update the description. Otherwise, throw an exception.
                    tblIngredient ingredient = dc.tblIngredients.Where(i => i.in_Id == this.Id).FirstOrDefault();
                    if (ingredient != null)
                    {
                        ingredient.in_Description = this.Description;
                        result = dc.SaveChanges();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Delete(int id)
        {
            // Create result variable for this method(int)
            int result = 0;

            try
            {
                //Using connection string statement and creating new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // If ingredient is selected, delete the ingredient along with the Id associated with it. Otherwise, throw an exception.
                    tblIngredient ingredient = dc.tblIngredients.Where(i => i.in_Id == id).FirstOrDefault();
                    if (ingredient != null)
                    {
                        dc.tblIngredients.Remove(ingredient);
                        result = dc.SaveChanges();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class IngredientList : List<Ingredient>
    {
        public void Load()
        {
            try
            {
                // Create new instance of ChefsCornerEntities/ Connection string
                ChefsCornerEntities dc = new ChefsCornerEntities();

                foreach (tblIngredient ingredient in dc.tblIngredients)
                {
                    Ingredient i = new Ingredient(ingredient.in_Id, ingredient.in_Description);
                    Add(i);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    public class MeasuredIngredient : Ingredient
    {
        public Measure Measurement { get; set; }
        public float Quantity { get; set; }
    }
    public class MeasuredIngredientList : List<MeasuredIngredient>
    {
        public void Load(int recipeid)
        {
            ChefsCornerEntities dc = new ChefsCornerEntities();
            var ingredients = from rin in dc.tblRecipeIngredients
                              join i in dc.tblIngredients on rin.in_Id equals i.in_Id
                              join m in dc.tblMeasures on rin.ms_Id equals m.ms_Id
                              where rin.rc_Id == recipeid
                              select new
                              {
                                  i.in_Id,
                                  i.in_Description,
                                  m.ms_Id,
                                  m.ms_Description,
                                  rin.ri_Quantity
                              };
            foreach(var mi in ingredients.ToList())
            {
                MeasuredIngredient MI = new MeasuredIngredient();
                MI.Id = mi.in_Id;
                MI.Description = mi.in_Description;
                MI.Measurement.Id = mi.ms_Id;
                MI.Measurement.Description = mi.ms_Description;
                MI.Quantity = mi.ri_Quantity;
                this.Add(MI);
            }
        }
    }

    public static class RecipeIngredient
    {
        public static void Delete(int recipeid, int ingredientid, int measureid)
        {
            try
            {
                // Create new instance of ChefsCornerEntities/ Connection string
                ChefsCornerEntities dc = new ChefsCornerEntities();

                tblRecipeIngredient rin = dc.tblRecipeIngredients.FirstOrDefault(rci => rci.rc_Id == recipeid && rci.in_Id == ingredientid && rci.ms_Id == measureid);
                if (rin != null)
                {
                    dc.tblRecipeIngredients.Remove(rin);
                    dc.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void Add(int recipeid, int ingredientid, int measureid)
        {
            try
            {
                // Create new instance of ChefsCornerEntities
                ChefsCornerEntities dc = new ChefsCornerEntities();

                // Create new instance of tblRecipeIngredient
                tblRecipeIngredient rin = new tblRecipeIngredient();

                // Add new Ids for recipes, ingredients, and measures to tblRecipeIngredients. Otherwise, throw an exception.
                rin.ri_Id = dc.tblRecipeIngredients.Any() ? dc.tblRecipeIngredients.Max(r => r.ri_Id) + 1 : 1;
                rin.in_Id = ingredientid;
                rin.rc_Id = recipeid;
                rin.ms_Id = measureid;
                dc.tblRecipeIngredients.Add(rin);
                dc.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public static class IngredientChecker
    {
        public static void CheckIngredients (List<Ingredient> ingredients)
        {
            try
            {
                ChefsCornerEntities dc = new ChefsCornerEntities();
                IngredientList Ingredients = new IngredientList();
                Ingredients.Load();               
                foreach (Ingredient i in ingredients)
                {
                    bool found = false;
                    foreach (Ingredient n in Ingredients)
                    {
                        if (i.Description == n.Description)
                        {
                            found = true;
                            i.Id = n.Id;
                        }
                    }
                    if (!found)
                        i.Insert();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
