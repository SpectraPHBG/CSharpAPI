using ApplicationService.ManagementServices;
using BankApiService.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankApiService.Controllers
{
    [RoutePrefix("api/token")]
    public class TokenAuthenticationController : ApiController
    {
        private readonly TokenAuthenticationManagementService service;

        public TokenAuthenticationController()
        {
            service = new TokenAuthenticationManagementService();
        }

        [HttpGet]
        [Route("clientToken/{personalNumber}/{bankBIC}")]
        public IHttpActionResult GenerateClientToken(string personalNumber,string bankBIC)
        {
            ResponseMessage response = new ResponseMessage();
            string token = service.GenerateClientToken(personalNumber, bankBIC);
            if (token == null)
            {
                response.Code = 404;
                response.Error = "The Authentication token could not be generated";
                return Json(response);
            }
            return Json(token);
        }
        [HttpGet]
        [Route("employeeToken/{personalNumber}/{bankBIC}")]
        public IHttpActionResult GenerateBankEmployeeToken(string personalNumber,string bankBIC)
        {
            ResponseMessage response = new ResponseMessage();
            string token = service.GenerateBankEmployeeToken(personalNumber,bankBIC);
            if (token == null)
            {
                response.Code = 404;
                response.Error = "The Authentication token could not be generated";
                return Json(response);
            }
            return Json(token);
        }
    }
}
