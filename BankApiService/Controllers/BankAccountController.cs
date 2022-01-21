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
    [RoutePrefix("api/{JWTToken}/bank_account")]
    public class BankAccountController : ApiController
    {
        private readonly BankAccountManagementService service = null;
        private readonly TokenAuthenticationManagementService tokenService = null;
        public BankAccountController()
        {
            service = new BankAccountManagementService();
            tokenService = new TokenAuthenticationManagementService();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllBankAccounts(string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetAllBankAccounts());
        }
        [HttpGet]
        [Route("{iban}")]
        public IHttpActionResult GetBankAccountByIban(string iban,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankAccountByIban(iban));
        }
        [HttpGet]
        [Route("id/{id}")]
        public IHttpActionResult GetBankAccountByID(long id,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientTokenAndConfirmIdentity(JWTToken,id) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetBankAccountByID(id));
        }
        //[HttpPost]
        //[Route("{clientIDS}/{bankID}")]
        //public IHttpActionResult Save(string clientIDS, int bankID,string JWTToken)
        //{
        //    ResponseMessage response = new ResponseMessage();
        //    if (!tokenService.ValidateBankEmployeeToken(JWTToken))
        //    {
        //        response.Code = 401;
        //        response.Error = "Token is missing/not valid or has expired";
        //        return Json(response);
        //    }
        //    Tuple<string, bool> responseTuple = service.Save(clientIDS,bankID);
        //    if (responseTuple.Item2)
        //    {
        //        response.Code = 201;
        //        response.Body = responseTuple.Item1;
        //    }
        //    else
        //    {
        //        response.Code = 200;
        //        response.Error = responseTuple.Item1;
        //    }
        //    return Json(response);
        //}
        [HttpPost]
        [Route("{clientID}/{accountID}")]
        public IHttpActionResult Save(long clientID, long accountID, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.AddAccountHolder(clientID, accountID);
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
        [Route("setActiveStatus")]
        public IHttpActionResult SetActiveStatus(Bank_AccountDetailedDTO accountDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.SetActiveStatus(accountDTO);
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
        [HttpPut]
        [Route("depositSum")]
        public IHttpActionResult DepositSumToAccount(TransactionManager manager, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.DepositSumToAccount(manager);
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
        [HttpPut]
        [Route("transferSum")]
        public IHttpActionResult TransferSumToAccount(TransactionManager manager, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.TransferSumToAccount(manager);
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
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
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
        [HttpDelete]
        [Route("{clientID}/{accountID}")]
        public IHttpActionResult Delete(long clientID, long accountID, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.RemoveAccountHolder(clientID, accountID);
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
    }
}
