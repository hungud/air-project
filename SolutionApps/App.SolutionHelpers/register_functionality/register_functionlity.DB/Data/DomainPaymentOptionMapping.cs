//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace register_functionlity.DB.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class DomainPaymentOptionMapping
    {
        public long Id { get; set; }
        public Nullable<long> DomainID { get; set; }
        public Nullable<long> PaymentOptionId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<long> ModifyBy { get; set; }
        public Nullable<long> CompanyId { get; set; }
    }
}
