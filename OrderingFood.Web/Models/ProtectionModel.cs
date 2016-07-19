using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderingFood.Web.Models
{
    public class ProtectionModel : AuthorizeAttribute
    {
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
                        
        }
    }

}