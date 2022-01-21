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
    [RoutePrefix("api/{JWTToken}/city")]
    public class CityController : ApiController
    {
        private readonly CityManagementService service =null;
        private readonly TokenAuthenticationManagementService tokenService = null;
        public CityController()
        {
            service = new CityManagementService();
            tokenService = new TokenAuthenticationManagementService();
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllCities(string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetAllCities());
        }
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetCityById(long id,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            return Json(service.GetCityById(id));
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(CityDTO cityDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Save(cityDTO);
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
        public IHttpActionResult Edit(CityDTO cityDTO,string JWTToken)
        {
            ResponseMessage response = new ResponseMessage();
            if (!tokenService.ValidateBankEmployeeToken(JWTToken))
            {
                response.Code = 401;
                response.Error = "Token is missing/not valid or has expired";
                return Json(response);
            }
            Tuple<string, bool> responseTuple = service.Edit(cityDTO);
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
