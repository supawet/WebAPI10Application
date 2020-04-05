using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web;
using System.IO;
using System.Text;
//using Newtonsoft.Json;

using WebAPI10Application.Models;

namespace WebAPI10Application.Controllers
{
    //[AuthenticationFilter]
    //[Authorize]

    [RoutePrefix("api/Log")]

    public class LogController : ApiController
    {
        [HttpPost]
        [ActionName("Add")]
        public LogCMSResponse AddPost([FromBody]LogCMSRequest LogcmsRequest, string id)
        {
            LogPersistance Logpersistance = new LogPersistance();

            LogCMSResponse LogcmsResponse = new LogCMSResponse();
            LogcmsResponse.Status = "Fail";
            LogcmsResponse.Message = "Fail";

            switch (id.ToUpper())
            {
                /*
                case "BAY":
                    {
                        //ret = "Hello Authorized API with ID = " + id + "__" + ODDforeGround.Status;
                        ODDresponse = ODDpersistance.AddForeGroundBAY(ODDforeGround);
                    }
                    break;
                    */
                case "CMS":
                    {
                        /*
                        ODDForeGroundSCB ODDforeGroundSCB = new ODDForeGroundSCB();
                        ODDforeGroundSCB.Reg_ref = ODDforeGround.Reg_ref;
                        ODDforeGroundSCB.Response = ODDforeGround.Response;
                        */
                        //ret = "Hello Authorized API with ID = " + ODDforeGround.Response + "__" + ODDforeGroundSCB.Reg_ref;
                        //ODDresponse.Message = "Hello Authorized API with ID = " + ODDforeGround.Response + "__" + ODDforeGroundSCB.Reg_ref;
                        LogcmsResponse = Logpersistance.AddCMS(LogcmsRequest);
                    }
                    break;
                default:
                    //ret = "Hello Authorized API with ID = " + id + "__" + ODDforeGround.Status;
                    break;
            }
            return LogcmsResponse;
        }
    }
}
