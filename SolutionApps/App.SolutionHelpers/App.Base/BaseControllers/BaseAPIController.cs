using System.Web.Http;
namespace App.Base
{
    /// <summary>
    /// Developed by RMSI Ltd (Rakesh Pal & Team)
    /// </summary>
    public class BaseAPIController : ApiController
    {
      
        public BaseAPIController()
        {
        }

       
        #region APIController Events
        protected override void Dispose(bool disposing)
        {
                base.Dispose(disposing);
        }
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion APIController Events
        public void ExceptionLog(System.Exception filterContext)
        {
            ErrorsLog.ErrorsLogInstance.ManageException(filterContext);
            //RedirectToAction("Error", "Home");
        }
    }
}
