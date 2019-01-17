using register_functionlity.DB.Data;
using register_functionlity.DB.Eunums;
using register_functionlity.DB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Service
{
    public class RegisterService
    {
        public CompanyDetailModel GetCompanyByPhoneNumber(string phoneNumber)
        {
            using (var context = new AirAdminDBEntities())
            {
                CompanyDetailModel model = null;
                var companyDetail = context.CompanyDetails.Where(m => m.PhoneNumber == phoneNumber).FirstOrDefault();
                if (companyDetail != null)
                {
                    model = new CompanyDetailModel();
                    model.Id = companyDetail.Id;
                    model.Name = companyDetail.Name;
                    model.EMailName = companyDetail.EMailName;
                    model.Fax = companyDetail.Fax;
                    model.WebsiteURL = companyDetail.WebsiteURL;
                    model.GSTRegNo = companyDetail.GSTRegNo;
                    model.Location = companyDetail.Location;
                    model.PhoneNumber = companyDetail.PhoneNumber;
                    model.StreetAddress = companyDetail.StreetAddress;
                }
                return model;
            }
        }
        
        public void SaveCompanyUser(UserModel model)
        {
            long companyDetailId = model.CompanyDetailId;
            if (model.CompanyDetailId == 0)
            {
                CompanyDetail companyDetail = SaveCompany(model);
                companyDetailId = companyDetail.Id;
            }

            //save user
            long Inserted_userid=SaveUser(model, companyDetailId,model.CompanyType);
            if ((model.CompanyType =="2") || (model.CompanyType == "4"))
            {
                //Member Agency Or Independent Agency
                SaveContactDetails(model, Inserted_userid);
            }
        }

        private void SaveContactDetails(UserModel model, long Inserted_userid)
        {
            using (var context = new AirAdminDBEntities())
            {
                //Company Type is Member Agency or Independent Agency
                var AddContactDetails = new ContactDetail
                {
                    UserID = Inserted_userid,
                    PaymentMethod = model.PaymentMethod,
                    CreditCardNumber = model.CreditCardNumber,
                    CreditCardName = model.CreditCardName,
                    CreditCardExpDate = model.CreditCardExpDate,
                    CVV = model.CVV,
                    UserLocation = model.UserLocation,
                    UserStreetAddress = model.UserStreetAddress,
                    UserZip=model.UserZip,
                    UserPhone=model.UserPhone,
                    ContactEmail=model.EmailAddress

                };
                context.ContactDetails.Add(AddContactDetails);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }

        private Int64 SaveUser(UserModel model, long companyDetailId,string CompanyType)
        {
            using (var context = new AirAdminDBEntities())
            {
                int RoleID = (int)UserRoles.Webuser;

                if (CompanyType == "1")
                {
                    RoleID = (int)UserRoles.Agents;
                }



                var user = new User
                {
                    UserName = model.EmailAddress,
                    EmailAddress = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = Helper.Encrypt(model.Password),
                    UserRoleId = RoleID,
                    CompanyDetailId = companyDetailId,

                    Address = " ",
                    PhoneNumber = model.ContactNumber,
                    Ext = model.Extension,
                    IsActive = true,
                    CanUpdatePassword = true
                };
                context.Users.Add(user);
                try { 
                context.SaveChanges();
                    return user.Id;
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
        }

        private CompanyDetail SaveCompany(UserModel model)
        {
            using (var context = new AirAdminDBEntities())
            {
                int CompanyTypeID = 4;
                int ParentID = 1;
                if (model.CompanyType == "1")
                {
                    CompanyTypeID = 2;
                    ParentID = model.ParentCompanyID;
                    
                }
                

                var companyDetail = new CompanyDetail
                {
                    Name = model.CompanyName,
                    EMailName = model.CompanyEmail,
                    WebsiteURL = model.WebsiteURL,
                    PhoneNumber = model.PhoneNumber,
                    GSTRegNo = model.GSTRegNo,
                    Fax = model.FaxNumber,
                    Location=model.Location,
                    CompanyTypeId=CompanyTypeID,
                    StreetAddress =model.StreetAddress,
                    ParentId=ParentID,
                    ApplicationURL = " ",
                    IsActive = "1"
                };
                context.CompanyDetails.Add(companyDetail);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
                return companyDetail;
            }
        }
    }
}
