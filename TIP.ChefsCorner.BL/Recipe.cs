using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIP.ChefsCorner.PL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace TIP.ChefsCorner.BL
{
    public class Recipe
    {
        // Declare variables and alter displayed names on the views
        public int Id { get; set; }
        [DisplayName("User Id")]
        public int UserId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        public HttpPostedFileBase Image { get; set; }
        [DisplayName("Image")]
        public string ConvertedImage { get; set; }
        [DisplayName("Image")]
        public string Thumbnail { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Directions { get; set; }
        [Required]
        public string Ingredients { get; set; }
        //public MeasuredIngredientList Ingredients { get; set; }

        // Instantiate the class and create an instance of IngredientList
        //public Recipe()
        //{
        //    Ingredients = new MeasuredIngredientList();
        //}

        public void LoadById(int id)
        {
            try
            {
                // Using statement 
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    var recipe = (from rc in dc.tblRecipes
                                  join u in dc.tblUsers on rc.us_Id equals u.us_Id
                                  where rc.rc_Id == id
                                  select new
                                  {
                                      rc.rc_Id,
                                      rc.us_Id,
                                      u.us_ScreenName,
                                      rc.rc_Description,
                                      rc.rc_Directions,
                                      rc.rc_Name,
                                      rc.rc_Image,
                                      rc.rc_Ingredients
                                  }).FirstOrDefault();

                    // If specific recipe is loaded by Id number, load the list of ingredients. Otherwise, throw an exception.
                    if (recipe != null)
                    {
                        this.Id = recipe.rc_Id;
                        this.UserId = recipe.us_Id;
                        this.UserName = recipe.us_ScreenName;
                        this.Description = recipe.rc_Description;
                        this.Directions = recipe.rc_Directions;
                        this.Name = recipe.rc_Name;
                        this.Image = (HttpPostedFileBase) new HttpByteFileBase(recipe.rc_Image);
                        ConvertedImage = Convert.ToBase64String(recipe.rc_Image);
                        this.Thumbnail = GetThumbnail(ConvertedImage);
                        this.Ingredients = recipe.rc_Ingredients;
                    }
                    else
                    {
                        throw new Exception("Recipe not found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public byte[] ConvertImage(HttpPostedFileBase i) //Convert our posted file into a byte array for SQL upload and resize it in the process
        {
            using (MemoryStream m = new MemoryStream())
            {
                try
                {             
                    MemoryStream t = new MemoryStream();
                    i.InputStream.CopyTo(t);
                    byte[] b = t.ToArray();
                    Image image;
                    MemoryStream ms = new MemoryStream(b);
                    image = System.Drawing.Image.FromStream(ms);
                    Bitmap bt = new Bitmap(200, 200);
                    Graphics g = Graphics.FromImage((Image)bt);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, 200, 200);
                    g.Dispose();
                    using (MemoryStream mst = new MemoryStream())
                    {
                        image.Save(mst, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageBytes = mst.ToArray();
                        return imageBytes;
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public string GetThumbnail(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                bytes = ms.ToArray();
            }
            return Convert.ToBase64String(bytes);
        }

        public int Insert()
        {
            // Declare result variable for this method (int)
            int result = 0;

            try
            {
                // Using statement for connection string, and create a new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // Create new instance of tblRecipe called recipe
                    tblRecipe recipe = new tblRecipe();

                    recipe.rc_Id = 1;
                    if (dc.tblRecipes.Any())
                        recipe.rc_Id = dc.tblRecipes.Max(r => r.rc_Id) + 1;
                    this.Id = recipe.rc_Id;
                    recipe.rc_Name = this.Name;
                    recipe.rc_Description = this.Description;
                    recipe.rc_Directions = this.Directions;
                    recipe.rc_Image = ConvertImage(this.Image);
                    recipe.us_Id = this.UserId;
                    recipe.rc_Ingredients = this.Ingredients;
                    dc.tblRecipes.Add(recipe);
                    result = dc.SaveChanges();
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Update()
        {
            int result = 0;
            try
            {
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    tblRecipe recipe = dc.tblRecipes.Where(r => r.rc_Id == this.Id).FirstOrDefault();
                    if (recipe != null)
                    {
                        recipe.rc_Name = this.Name;
                        recipe.rc_Description = this.Description;
                        recipe.rc_Directions = this.Directions;
                        recipe.rc_Image = ConvertImage(this.Image);
                        recipe.rc_Ingredients = this.Ingredients;
                        result = dc.SaveChanges();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return 0;
                throw e;
            }
        }

        public int Delete (int id)
        {
            int result = 0;
            try
            {
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    tblRecipe recipe = dc.tblRecipes.Where(r => r.rc_Id == id).FirstOrDefault();
                    if (recipe != null)
                    {
                        dc.tblRecipes.Remove(recipe);
                        result = dc.SaveChanges();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return 0;
                throw e;
            }
        }
    }

    public class RecipeList : List<Recipe>
    {
        public void Load()
        {
            Load(null);
        }

        public void Load(int? userId)
        {
            using (ChefsCornerEntities dc = new ChefsCornerEntities())
            {
                var recipes = (from rc in dc.tblRecipes
                               join u in dc.tblUsers on rc.us_Id equals u.us_Id
                               where rc.us_Id == userId || userId == null
                               select new
                               {
                                   rc.rc_Id,
                                   rc.rc_Name,
                                   rc.rc_Description,
                                   rc.rc_Directions,
                                   rc.rc_Image,
                                   u.us_Id,
                                   u.us_ScreenName,
                                   rc.rc_Ingredients
                               }).ToList();
                foreach (var rc in recipes)
                {
                    Recipe recipe = new Recipe();
                    recipe.Id = rc.rc_Id;
                    recipe.Name = rc.rc_Name;
                    recipe.Description = rc.rc_Description;
                    recipe.Directions = rc.rc_Directions;
                    recipe.Image = (HttpPostedFileBase)new HttpByteFileBase(rc.rc_Image);
                    recipe.ConvertedImage = Convert.ToBase64String(rc.rc_Image);
                    recipe.UserId = rc.us_Id;
                    recipe.UserName = rc.us_ScreenName;
                    recipe.Ingredients = rc.rc_Ingredients;
                    recipe.Thumbnail = recipe.GetThumbnail(recipe.ConvertedImage);
                   // recipe.LoadIngredients();
                    this.Add(recipe);
                }
            }
        }
    }
    public class HttpByteFileBase : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public HttpByteFileBase(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }
}
