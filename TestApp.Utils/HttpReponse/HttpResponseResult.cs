using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Utils.HttpReponse
{
    public class HttpResponseResult : IHttpResponseResult
    {
        public string message { get; set; }

        public dynamic result { get; set; }
    }
}
