using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIP.ChefsCorner.PL;
using System.ComponentModel;

namespace TIP.ChefsCorner.BL
{
    public class Measure
    {
        // Declare variables
        public int Id { get; set; }
        public string Description { get; set; }

        // Instantiate the class
        public Measure()
        {

        }

        public Measure(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public void LoadById(int id)
        {
            try
            {
                // Create new instance of ChefsCornerEntities/ Connection string
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // Find specific measure with the Id number, otherwise throw an exception
                    tblMeasure Measure = dc.tblMeasures.Where(i => i.ms_Id == id).FirstOrDefault();
                    if (Measure != null)
                    {
                        this.Id = Measure.ms_Id;
                        this.Description = Measure.ms_Description;
                    }
                    else
                    {
                        throw new Exception("Measure not found.");
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
                // Using statement for connection string, and create a new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // Create new instance of tblMeasure called Measure
                    tblMeasure measure = new tblMeasure();

                    // Insert new user into tblMeasures by adding new Id number, otherwise throw exception
                    measure.ms_Id = 1;
                    if (dc.tblMeasures.Any())
                        measure.ms_Id = dc.tblMeasures.Max(i => i.ms_Id) + 1;
                    this.Id = measure.ms_Id;
                    measure.ms_Description = this.Description;
                    dc.tblMeasures.Add(measure);
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
            // Create result variable for this method (int)
            int result = 0;

            try
            {
                // Using statement for connection string, and create a new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // If a specific user is selected, update the user information. Otherwise, throw an exception.
                    tblMeasure Measure = dc.tblMeasures.Where(i => i.ms_Id == this.Id).FirstOrDefault();
                    if (Measure != null)
                    {
                        Measure.ms_Description = this.Description;
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
            // Create result variable for this method (int)
            int result = 0;

            try
            {
                // Using statement for connection string, and create a new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                
                // If Measure is selected, delete measure from tblMeasures and its Id number. Otherwise, throw exception
                {
                    tblMeasure Measure = dc.tblMeasures.Where(i => i.ms_Id == id).FirstOrDefault();
                    if (Measure != null)
                    {
                        dc.tblMeasures.Remove(Measure);
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

    public class MeasureList : List<Measure>
    {
        public void Load()
        {
            try
            {
                // Create new instance for ChefsCornerEntities/ Connection string
                ChefsCornerEntities dc = new ChefsCornerEntities();

                foreach (tblMeasure Measure in dc.tblMeasures)
                {
                    Measure i = new Measure(Measure.ms_Id, Measure.ms_Description);
                    Add(i);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
