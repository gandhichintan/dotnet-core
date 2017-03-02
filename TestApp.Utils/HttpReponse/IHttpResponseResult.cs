using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Utils.HttpReponse
{
    public interface IHttpResponseResult
    {
        dynamic result { get; set; }
        string message  { get; set; }
    }
}
