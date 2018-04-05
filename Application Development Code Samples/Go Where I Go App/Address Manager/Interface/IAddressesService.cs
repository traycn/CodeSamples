using GWIG.Web.Domain;
using GWIG.Web.Models.Requests.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWIG.Web.Services.Interface
{
    public interface IAddressesService
    {
        // POST: Address
        AddressRequestModel InsertAddress(AddressRequestModel model);

        // UPDATE: Address by Id
        void AddressRequestModel(AddressRequestModel model, int Id);

        // GET: Address by Id
        AddressRequestModel SelectIdAddress(int AddressId);

        // GET: All Addresses
        List<AddressRequestModel> SelectAll();

        // DELETE: Address by Id
        void DeleteIdAddress(int Id);
    }
}