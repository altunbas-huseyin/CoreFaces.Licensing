using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Licensing
{

    public class Licence
    {
        public Licence(string type, IHttpContextAccessor iHttpContextAccessor, string domain, string password)
        {
            try
            {
                if (domain == "")
                { return; }
                if (domain.Contains(":"))
                { return; }

                bool isResult = CoreFaces.Helper.IsLocalExtension.IsLocal(iHttpContextAccessor.HttpContext.Request);
                if (isResult)
                    return;

                bool result = Verify(type, iHttpContextAccessor, domain, password);
                if (!result)
                { throw new DllNotFoundException("License error.!!!"); }
            }
            catch (Exception ex)
            {
                throw new DllNotFoundException("License error.!!!");
            }
        }
        public bool Verify(string type, IHttpContextAccessor iHttpContextAccessor, string domain, string password)
        {
            bool result = false;
            bool resultVerify = BCrypt.Net.BCrypt.Verify(domain + type, password);
            if (resultVerify)
            {
                result = true;
            }
            return result;
        }
    }
}
