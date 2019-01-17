using register_functionlity.DB.Data;
using register_functionlity.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace register_functionlity.DB.Service
{
    public class MISCServices
    {
        public List<BlockAirlineModel> GetBlockedAirlines(string domain, int CompanyTypeId)
        {
            try
            {
                using (AirAdminDBEntities db = new AirAdminDBEntities())
                {
                    if (CompanyTypeId != 1)
                    {

                        return db.CompanyDetails.Join(db.AirlineRestrictionAirs, cd => cd.ParentId, ar => ar.DefaultCompanyId, (cd, ar) =>
                                new BlockAirlineModel
                                {
                                    IATACode = ar.IATACode,
                                    IsNetfareMarkup = ar.IsNetfareMarkup == null ? false : ar.IsNetfareMarkup.Value,
                                    IsPublicMarkup = ar.IsPublicMarkup == null ? false : ar.IsPublicMarkup.Value,
                                    WebsiteUrl = cd.WebsiteURL,
                                    IsDeleted= ar.IsDeleted                                    
                                }).Where(k => k.WebsiteUrl == domain && k.IsDeleted== false).ToList();

                    }
                    else
                    {
                        return db.CompanyDetails.Join(db.AirlineRestrictionAirs, cd => cd.Id, ar => ar.DefaultCompanyId, (cd, ar) =>
                                new BlockAirlineModel
                                {
                                    IATACode = ar.IATACode,
                                    IsNetfareMarkup = ar.IsNetfareMarkup == null ? false : ar.IsNetfareMarkup.Value,
                                    IsPublicMarkup = ar.IsPublicMarkup == null ? false : ar.IsPublicMarkup.Value,
                                    WebsiteUrl = cd.WebsiteURL,
                                    IsDeleted = ar.IsDeleted
                                }).Where(k => k.WebsiteUrl == domain && k.IsDeleted == false).ToList();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
