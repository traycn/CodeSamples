using Microsoft.Practices.Unity;
using GWIG.Web.Domain;
using GWIG.Web.Models.Requests.Addresses;
using GWIG.Web.Models.Responses;
using GWIG.Web.Services;
using GWIG.Web.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GWIG.Web.Controllers.Api
{
    [RoutePrefix("api/addresses")]
    public class AddressesApiController : ApiController
    {

        [Dependency]
        public IAddressesService _AddressesService { get; set; }

        // POST: Address Model
        [Route(), HttpPost]
        [Authorize]
        public HttpResponseMessage AddAddress(InsertAddressModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            model.UserId = UserService.GetCurrentUserId();

            _AddressesService.InsertAddress(model);

            ItemResponse<InsertAddressModel> Response = new ItemResponse<InsertAddressModel>();

            Response.Item = model;

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }


        // UPDATE: Address by Ids
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateAddress(UpdateAddressModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _AddressesService.UpdateAddress(model, id);

            ItemResponse<UpdateAddressModel> Response = new ItemResponse<UpdateAddressModel>();

            Response.Item = model;

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }


        // GET: Address by Id
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage SelectById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var AddressItems = _AddressesService.SelectIdAddress(id);

            ItemResponse<int> Response = new ItemResponse<int>();

            Response.Item = id;

            return Request.CreateResponse(HttpStatusCode.OK, AddressItems);
        }


        // GET: All Addresses
        [Route(), HttpGet]
        public HttpResponseMessage SelectAllAddresses(Addresses model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<Addresses> ListofAddresses = _AddressesService.SelectAll();

            ItemsResponse<Addresses> Response = new ItemsResponse<Addresses>();

            Response.Items = ListofAddresses;

            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }


        // DELETE: Address by Id
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _AddressesService.DeleteIdAddress(id);

            ItemResponse<int> Response = new ItemResponse<int>();

            Response.Item = id;

            return Request.CreateResponse(HttpStatusCode.OK, "Delete Address with ID: " + id);
        }
    }
}

