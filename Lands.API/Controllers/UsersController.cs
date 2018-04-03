namespace Lands.API.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Domain;
    using Helpers;
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json.Linq;

    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        [HttpPost]
        [Route("PasswordRecovery")]
        public async Task<IHttpActionResult> PasswordRecovery(JObject form)
        {
            try
            {
                var email = string.Empty;
                dynamic jsonObject = form;

                try
                {
                    email = jsonObject.Email.Value;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }

                var user = await db.Users
                    .Where(u => u.Email.ToLower() == email.ToLower())
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest("Error 001");
                }

                var userContext = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var userASP = userManager.FindByEmail(email);
                if (userASP == null)
                {
                    return BadRequest("Error 001");
                }

                var random = new Random();
                var newPassword = string.Format("{0}", random.Next(100000, 999999));
                var response1 = userManager.RemovePassword(userASP.Id);
                var response2 = await userManager.AddPasswordAsync(userASP.Id, newPassword);
                if (response2.Succeeded)
                {
                    var subject = "Russia - Password Recovery";
                    var body = string.Format(@"
                        <h1>Russia App - Password Recovery</h1>
                        <p>Your new password is: <strong>{0}</strong></p>
                        <p>Please, don't forget change it for one easy remember for you.",
                        newPassword);

                    await MailHelper.SendMail(email, subject, body);
                    return Ok(true);
                }

                return BadRequest("Error 002");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(JObject form)
        {
            var email = string.Empty;
            var currentPassword = string.Empty;
            var newPassword = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
                currentPassword = jsonObject.CurrentPassword.Value;
                newPassword = jsonObject.NewPassword.Value;
            }
            catch
            {
                return BadRequest("Incorrect call");
            }

            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);

            if (userASP == null)
            {
                return BadRequest("Incorrect call");
            }

            var response = await userManager.ChangePasswordAsync(userASP.Id, currentPassword, newPassword);
            if (!response.Succeeded)
            {
                return BadRequest(response.Errors.FirstOrDefault());
            }

            return Ok("ok");
        }

        [HttpPost]
        [Authorize]
        [Route("GetUserByEmail")]
        public async Task<IHttpActionResult> GetUserByEmail(JObject form)
        {
            var email = string.Empty;
            dynamic jsonObject = form;
            try
            {
                email = jsonObject.Email.Value;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }

            var user = await db.Users.
                Where(u => u.Email.ToLower() == email.ToLower()).
                FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("LoginTwitter")]
        public async Task<IHttpActionResult> LoginTwitter(TwitterResponse profile)
        {
            try
            {
                var firstName = string.Empty;
                var lastName = string.Empty;
                var fullName = profile.Name;
                var posSpace = fullName.IndexOf(' ');
                if (posSpace == -1)
                {
                    firstName = fullName;
                    lastName = fullName;
                }
                else
                {
                    firstName = fullName.Substring(0, posSpace);
                    lastName = fullName.Substring(posSpace + 1);
                }

                var user = await db.Users.Where(u => u.Email == profile.IdStr).FirstOrDefaultAsync();
                if (user == null)
                {
                    user = new User
                    {
                        Email = profile.IdStr,
                        FirstName = firstName,
                        LastName = lastName,
                        ImagePath = profile.ProfileImageUrl,
                        UserTypeId = 3,
                        Telephone = "...",
                    };

                    db.Users.Add(user);
                    UsersHelper.CreateUserASP(profile.IdStr, "User", profile.IdStr);
                }
                else
                {
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.ImagePath = profile.ProfileImageUrl;
                    db.Entry(user).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (DbEntityValidationException e)
            {
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("\n- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("LoginInstagram")]
        public async Task<IHttpActionResult> LoginInstagram(InstagramResponse profile)
        {
            try
            {
                var firstName = string.Empty;
                var lastName = string.Empty;
                var fullName = profile.UserData.FullName;
                var posSpace = fullName.IndexOf(' ');
                if (posSpace == -1)
                {
                    firstName = fullName;
                    lastName = fullName;
                }
                else
                {
                    firstName = fullName.Substring(0, posSpace);
                    lastName = fullName.Substring(posSpace + 1);
                }

                var user = await db.Users.Where(u => u.Email == profile.UserData.Id).FirstOrDefaultAsync();
                if (user == null)
                {
                    user = new User
                    {
                        Email = profile.UserData.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        ImagePath = profile.UserData.ProfilePicture,
                        UserTypeId = 4,
                        Telephone = "...",
                    };

                    db.Users.Add(user);
                    UsersHelper.CreateUserASP(profile.UserData.Id, "User", profile.UserData.Id);
                }
                else
                {
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.ImagePath = profile.UserData.ProfilePicture;
                    db.Entry(user).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (DbEntityValidationException e)
            {
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("\n- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("LoginFacebook")]
        public async Task<IHttpActionResult> LoginFacebook(FacebookResponse profile)
        {
            try
            {
                var user = await db.Users.Where(u => u.Email == profile.Id).FirstOrDefaultAsync();
                if (user == null)
                {
                    user = new User
                    {
                        Email = profile.Id,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        ImagePath = profile.Picture.Data.Url,
                        UserTypeId = 2,
                        Telephone = "...",
                    };

                    db.Users.Add(user);
                    UsersHelper.CreateUserASP(profile.Id, "User", profile.Id);
                }
                else
                {
                    user.FirstName = profile.FirstName;
                    user.LastName = profile.LastName;
                    user.ImagePath = profile.Picture.Data.Url;
                    db.Entry(user).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (DbEntityValidationException e)
            {
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("\n- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Users/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (user.ImageArray != null && user.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(user.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    user.ImagePath = fullPath;
                }
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User model)
        {
            var oldUser = await db.Users.
                Where(u => u.Email.ToLower().Equals(model.Email.ToLower())).
                FirstOrDefaultAsync();
            if (oldUser != null)
            {
                return BadRequest("Error 001");
            }

            if (model.ImageArray != null && model.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(model.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    model.ImagePath = fullPath;
                }
            }

            db.Users.Add(model);
            await db.SaveChangesAsync();
            UsersHelper.CreateUserASP(model.Email, "User", model.Password);

            return CreatedAtRoute("DefaultApi", new { id = model.UserId }, model);
        }

        // DELETE: api/Users/5
        [Authorize]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}