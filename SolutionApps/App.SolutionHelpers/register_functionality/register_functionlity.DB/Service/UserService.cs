using register_functionlity.DB.Data;
using register_functionlity.DB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Service
{
    public class UserService
    {
        public UserModel GetUser(string userName, string password)
        {
            using (var context = new AirAdminDBEntities())
            {
                try
                {
                    var model = new UserModel();
                    string encPwd = Helper.Encrypt(password);
                    var user = context.Users.FirstOrDefault(m => m.UserName.ToLower() == userName.ToLower() && m.Password == encPwd);
                    if (user != null)
                    {
                        model.Id = user.Id;
                        model.UserName = user.UserName;
                        model.FirstName = user.FirstName;
                        model.LastName = user.LastName;
                        model.EmailAddress = user.EmailAddress;
                        model.CompanyDetailId = user.CompanyDetailId;
                        model.UserRoleId = user.UserRoleId;
                        model.RoleName = GetUserRole(model.UserRoleId);
                    }
                    return model;
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }

        private string GetUserRole(int userRoleId)
        {
            using (var context = new AirAdminDBEntities())
            {
                return context.UserRoles.FirstOrDefault(m => m.Id == userRoleId).Name;
            }
        }

        public UserModel GetUserByUserName(string userName)
        {
            using (var context = new AirAdminDBEntities())
            {
                var model = new UserModel();
                var user = context.Users.FirstOrDefault(m => m.UserName.ToLower() == userName.ToLower());
                if (user != null)
                {
                    model.Id = user.Id;
                    model.UserName = user.UserName;
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.EmailAddress = user.EmailAddress;
                    model.CompanyDetailId = user.CompanyDetailId;
                    model.UserRoleId = user.UserRoleId;
                    model.PhoneNumber = user.PhoneNumber;
                    model.RoleName = GetUserRole(model.UserRoleId);
                }
                var contactdetails = context.ContactDetails.Where(x => x.UserID == user.Id).SingleOrDefault();
                if (contactdetails != null)
                {
                    model.PaymentMethod = contactdetails.PaymentMethod;
                    model.CreditCardNumber = contactdetails.CreditCardNumber;
                    model.CreditCardName = contactdetails.CreditCardName;
                    model.CVV = Convert.ToInt32(contactdetails.CVV);
                    model.UserLocation = contactdetails.UserLocation;
                    model.UserStreetAddress = contactdetails.UserStreetAddress;
                    model.UserZip = contactdetails.UserZip;
                    model.UserPhone = Convert.ToInt64(contactdetails.UserPhone);
                    model.UserEmail = contactdetails.ContactEmail;
                    if (contactdetails.CreditCardExpDate != null)
                    {
                        if (contactdetails.CreditCardExpDate.Length > 0)
                        {
                            string[] CreditCard_Expiry = contactdetails.CreditCardExpDate.Split('-');
                            model.ExpiryMonth = CreditCard_Expiry[0].ToString();
                            model.ExpiryYear = CreditCard_Expiry[1].ToString();
                        }
                    }
                }
                return model;
            }
        }

        public void UpdateUserProfile(UserModel model)
        {
            using (var context = new AirAdminDBEntities())
            {
                var user = context.Users.FirstOrDefault(m => m.UserName.ToLower() == model.UserName.ToLower());
                long UserID = user.Id;
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumber = model.PhoneNumber;
                    context.Entry(user).State = EntityState.Modified;
                }

                var contactdetails = context.ContactDetails.Where(x => x.UserID == user.Id).SingleOrDefault();
                if (contactdetails == null)
                {
                    ContactDetail add_detail = new ContactDetail();
                    add_detail.ContactEmail = model.UserEmail;
                    add_detail.CreditCardExpDate = model.CreditCardExpDate;
                    add_detail.CreditCardName = model.CreditCardName;
                    add_detail.CreditCardNumber = model.CreditCardNumber;
                    add_detail.CVV = model.CVV;
                    add_detail.PaymentMethod = model.PaymentMethod;
                    add_detail.UserLocation = model.UserLocation;
                    add_detail.UserPhone = model.UserPhone;
                    add_detail.UserStreetAddress = model.UserStreetAddress;
                    add_detail.UserZip = model.UserZip;
                    add_detail.UserID = UserID;
                    context.ContactDetails.Add(add_detail);
                }
                else
                {
                    contactdetails.UserID = user.Id;
                    contactdetails.ContactEmail = model.UserEmail;
                    contactdetails.CreditCardExpDate = model.CreditCardExpDate;
                    contactdetails.CreditCardName = model.CreditCardName;
                    contactdetails.CreditCardNumber = model.CreditCardNumber;
                    contactdetails.CVV = model.CVV;
                    contactdetails.PaymentMethod = model.PaymentMethod;
                    contactdetails.UserLocation = model.UserLocation;
                    contactdetails.UserPhone = model.UserPhone;
                    contactdetails.UserStreetAddress = model.UserStreetAddress;
                    contactdetails.UserZip = model.UserZip;
                    context.Entry(contactdetails).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }

        public UserModel GetUserByID(int UserID)
        {
            using (var context = new AirAdminDBEntities())
            {
                var model = new UserModel();
                var user = context.Users.FirstOrDefault(m => m.Id == UserID);
                if (user != null)
                {
                    model.Id = user.Id;
                    model.UserName = user.UserName;
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.EmailAddress = user.EmailAddress;
                    model.CompanyDetailId = user.CompanyDetailId;
                    model.UserRoleId = user.UserRoleId;
                    model.RoleName = GetUserRole(model.UserRoleId);
                }
                return model;
            }
        }

        public UserModel GetUserByEmail(string email)
        {
            using (var context = new AirAdminDBEntities())
            {
                var model = new UserModel();
                var user = context.Users.FirstOrDefault(m => m.EmailAddress.ToLower() == email.ToLower());
                if (user != null)
                {
                    model.Id = user.Id;
                    model.UserName = user.UserName;
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.EmailAddress = user.EmailAddress;
                    model.CompanyDetailId = user.CompanyDetailId;
                    model.UserRoleId = user.UserRoleId;
                    model.RoleName = GetUserRole(model.UserRoleId);
                }
                return model;
            }
        }

        public void SaveResetPasswordCode(long id, string code)
        {
            using (var context = new AirAdminDBEntities())
            {
                var user = context.Users.FirstOrDefault(m => m.Id == id);
                if(user != null)
                {
                    user.ResetPasswordCode = code;
                    context.SaveChanges();
                }
            }
        }

        public void ResetPassword(long id, string code, string password)
        {
            using (var context = new AirAdminDBEntities())
            {
                var user = context.Users.FirstOrDefault(m => m.Id == id && m.ResetPasswordCode == code);
                if (user != null)
                {
                    user.Password = Helper.Encrypt(password);
                    user.ResetPasswordCode = null;
                    context.SaveChanges();
                }
            }
        }
    }
}
