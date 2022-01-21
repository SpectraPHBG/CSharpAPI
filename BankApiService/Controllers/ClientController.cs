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
    [RoutePrefix("api/{JWTToken}/client")]
    public class ClientController : ApiController
    {
        private readonly ClientManagementService service = null;
        private readonly TokenAuthenticationManagementService tokenService = null;
        public ClientController()
        {
            service = new ClientManagementService();
            tokenService = new TokenAuthenticationManagementService();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllClientsStandard(string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetAllClientsStandard());
        }
        [HttpGet]
        [Route("id/{id}")]
        public IHttpActionResult GetClientByID(long id,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientTokenAndConfirmIdentity(JWTToken,id) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetClientByID(id));
        }
        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult GetClientsByName(string name, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetAllClientsByName(name));
        }
        [HttpGet]
        [Route("pNumber/{personalNumber}")]
        public IHttpActionResult GetClientsByPersonalNumber(string personalNumber, string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateClientToken(JWTToken) && !tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetClientByPersonalNumber(personalNumber));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(ClientDetailedDTO clientDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Save(clientDTO);
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
        public IHttpActionResult Edit(ClientDetailedDTO clientDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Edit(clientDTO);
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
    }
}
