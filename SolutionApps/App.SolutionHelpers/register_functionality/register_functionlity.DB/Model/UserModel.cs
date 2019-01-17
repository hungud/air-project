using register_functionlity.DB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Model
{
    [NotMapped]
    public class UserModel : User
    {
        [Required]
        [Display(Name = "Phone Number")]
        public new string PhoneNumber { get; set; }

        public int ParentCompanyID { get; set; }

        public string RoleName { get; set; }
        public bool ShowCompany { get; set; }
        public bool ShowUser { get; set; }
        public bool DisabledUserFields { get; set; }
        public bool DisabledCompanyFields { get; set; }
        public string CompanyType { get; set; }
        //public string CompanyName { get; set; }

        #region CompanyDetail
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string CompanyContactNumber { get; set; }

        [Required]
        [Display(Name = "City/State/Country")]
        public string Location { get; set; }

        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string CompanyEmail { get; set; }


        [Display(Name = "Website URL")]
        public string WebsiteURL { get; set; }


        [Display(Name = "GST RegNo")]
        public string GSTRegNo { get; set; }


        #endregion

        //public bool HideCompany { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [Display(Name = "Extension")]
        public string Extension { get; set; }

        [Required]
        [Display(Name = "Email Address / Username")]
        public new string EmailAddress { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public new string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public new string LastName { get; set; }

        [Required]
        public new string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and COnfirm password should be samme.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Payment Method")]
        public new string PaymentMethod { get; set; }

        [Display(Name = "Credit Card Number")]
        public new string CreditCardNumber { get; set; }

        [Display(Name = "Name On Card")]
        public new string CreditCardName { get; set; }

        [Display(Name = "Expiry Date")]
        public new string CreditCardExpDate { get; set; }

        [Display(Name = "Card Verification Number")]
        public new int CVV { get; set; }


        [Display(Name = "City/State/Country")]
        public string UserLocation { get; set; }

        [Display(Name = "Street Address")]
        public string UserStreetAddress { get; set; }

        [Display(Name = "Zip")]
        public string UserZip { get; set; }

        [Display(Name = "Billing Phone (e.g 1234567890)")]
        public Int64 UserPhone { get; set; }

        [Display(Name = "Contact Email")]
        public string UserEmail { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }

    }
}
