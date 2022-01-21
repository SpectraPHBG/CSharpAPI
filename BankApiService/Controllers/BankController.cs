using ApplicationService.DTOs;
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
    [RoutePrefix("api/{JWTToken}/bank")]
    public class BankController : ApiController
    {
        private readonly BankManagementService service = null;
        private readonly TokenAuthenticationManagementService tokenService = null;
        public BankController()
        {
            service = new BankManagementService();
            tokenService = new TokenAuthenticationManagementService();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllBanks(string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetAllBanks());
        }
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult GetBanksByName(string name,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBanksByName(name));
        }
        [HttpGet]
        [Route("id/{id}")]
        public IHttpActionResult GetBankByID(long id, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankByID(id));
        }
        [HttpGet]
        [Route("bic/{bic}")]
        public IHttpActionResult GetBankByBIC(string bic, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankByBIC(bic));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(BankDTO bankDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeExecToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Save(bankDTO);
            if (responseTuple.Item2)
            {
                response.Code = 201;
                response.Body = responseTuple.Item1;
            }
            else
            {
                response.Code = 200;
                response.Error = responseTuple.Item1;
            }
            return Json(response);
        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult Edit(BankDTO bankDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeExecToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Edit(bankDTO);
            if (responseTuple.Item2)
            {
                response.Code = 200;
                response.Body = responseTuple.Item1;
            }
            else
            {
                response.Code = 200;
                response.Error = responseTuple.Item1;
            }
            return Json(response);
        }
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(long id,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeExecToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Delete(id);
            if (responseTuple.Item2)
            {
                response.Code = 200;
                response.Body = responseTuple.Item1;
            }
            else
            {
                response.Code = 200;
                response.Error = responseTuple.Item1;
            }
            return Json(response);
        }
    }
}
