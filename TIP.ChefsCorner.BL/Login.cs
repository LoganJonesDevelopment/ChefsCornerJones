using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIP.ChefsCorner.PL;
using System.ComponentModel;

namespace TIP.ChefsCorner.BL
{
    public class Login
    {
        // Declare variables
        public int Id { get; set; }
        [DisplayName("User Name")]
        public string ScreenName { get; set; }
        public string Email { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }

        // Instantiate the class
        public Login()
        {

        }

        public Login(string screenname, string email, string password)
        {
            ScreenName = screenname;
            Email = email;
            Password = password;
        }

        public Login(int id, string screenname, string email, string password)
        {
            Id = id;
            ScreenName = screenname;
            Email = email;
            Password = password;
        }

        private void Map(tblUser user)
        {
            user.us_Id = this.Id;
            user.us_ScreenName = this.ScreenName;
            user.us_Email = this.Email;
            user.us_Pass = this.GetHash();
        }

        private string GetHash()
        {
            // Hash the password
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(this.Password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }

        public bool SignIn()
        {
            try
            {
                // If Username and/ or Password text boxes are not empty, then login. Otherwise, return false.
                if (ScreenName != null && ScreenName != string.Empty)
                {
                    if (Password != null && Password != string.Empty)
                    {
                        // Create new instance of ChefsCornerEntities/ Connection string
                        ChefsCornerEntities dc = new ChefsCornerEntities();

                        tblUser user = dc.tblUsers.FirstOrDefault(u => u.us_ScreenName == this.ScreenName);
                        if (user != null)
                        {
                            if (user.us_Pass == this.GetHash())
                            {
                                // Successful login
                                Email = user.us_Email;
                                Id = user.us_Id;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Insert()
        {
            try
            {
                // Create new instance of ChefsCornerEntities/ Connection string
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // Create new instance of tblUser called user
                    tblUser user = new tblUser();
                    // Insert new user into tblUsers by adding new Id number, otherwise throw exception
                    Id = dc.tblUsers.Any() ? dc.tblUsers.Max(u => u.us_Id) + 1 : 1;
                    Map(user);
                    dc.tblUsers.Add(user);
                    dc.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update()
        {
            try
            {
                // Using statement for connection string, and create a new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    tblUser user = dc.tblUsers.Where(u => u.us_Id == this.Id).FirstOrDefault();

                    // If a user is selected, update the user info. Otherwise, throw an exception.
                    if (user != null)
                    {
                        Map(user);
                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(int id)
        {
            try
            {
                // Using statement for connection string, and create a new instance of it
                using (ChefsCornerEntities dc = new ChefsCornerEntities())
                {
                    // If user with a specific id is selected, delete the user and the Id associated with it. Otherwise, throw an exception.
                    tblUser user = dc.tblUsers.Where(u => u.us_Id == id).FirstOrDefault();                   
                    if (user != null)
                    {
                        dc.tblUsers.Remove(user);
                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
