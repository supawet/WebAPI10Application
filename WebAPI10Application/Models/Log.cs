using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebAPI10Application.Models
{
    public class Log
    {
    }

    //----------------------------- CMS ------------------------------
    public class LogCMSRequest
    {
        public string Source_IP_Address { get; set; }
        public string Target_IP_Address { get; set; }
        public string User_Email { get; set; }
        public string Login_Result { get; set; }
    }

    public class LogCMSResponse
    {
        //public string Reference_no { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
    //----------------------------- /CMS ------------------------------
}