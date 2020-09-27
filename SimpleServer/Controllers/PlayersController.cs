using SimpleServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace SimpleServer.Controllers
{
    public class PlayersController : ApiController
    {
        // GET: api/Players
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            // if user does not exist - 401 returned...
            // can utilize linq for custom queries if needed
            using (NBAContext db = new NBAContext())
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.Players.ToList());
            }
        }
    }
}
