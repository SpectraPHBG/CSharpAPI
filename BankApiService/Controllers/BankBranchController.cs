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
    [RoutePrefix("api/{JWTToken}/bank_branch")]
    public class BankBranchController : ApiController
    {
        private readonly BankBranchManagementService service = null;
        private readonly TokenAuthenticationManagementService tokenService = null;
        public BankBranchController()
        {
            service = new BankBranchManagementService();
            tokenService = new TokenAuthenticationManagementService();
        }
        [HttpGet]
        [Route("{bankID}")]
        public IHttpActionResult GetAllBankBranches(long bankID,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankBranchesByBank(bankID));
        }
        [HttpGet]
        [Route("city/{cityID}")]
        public IHttpActionResult GetBranchesByCity(long cityID,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankBranchesByCity(cityID));
        }
        [HttpGet]
        [Route("id/{id}")]
        public IHttpActionResult GetBranchByID(long id, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankBranchByID(id));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(Bank_BranchDTO branchDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeExecToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Save(branchDTO);
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
        public IHttpActionResult Edit(Bank_BranchDTO branchDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeExecToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Edit(branchDTO);
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
