using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SimpleServer
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                // if headers are not present - client has not sent credentials - send 401 unauthorized response
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                // if headers are present
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                // decode authToken - base64 to string - username:password
                string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                // split decoded string on ':' -  transform to string array
                string[] credentialsArr = decodedAuthToken.Split(':');
                // destructure string arr
                string username = credentialsArr[0];
                string password = credentialsArr[1];
                // set current principal of executing thread
                if(NbaSecurity.Login(username, password))
                {
                    // if username & password match
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    // if username & password do not match - send unauthorized response
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}