using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using GWIG.Data.Providers;
using GWIG.Web.Domain;
using GWIG.Web.Models.Requests;
using GWIG.Data;
using GWIG.Web.Models.Requests.Addresses;
using GWIG.Web.Services.Interface;

namespace GWIG.Web.Services
{
    public class AddressesService : BaseService, IAddressesService
    {
        // POST: Address
        public AddressRequestModel Insert(AddressRequestModel model)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@Address1", model.Address1);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@State", model.State);
                   paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                   paramCollection.AddWithValue("@Latitude", model.Latitude);
                   paramCollection.AddWithValue("@Longitude", model.Longitude);

                   SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@Id"].Value.ToString(), out Id);
               });

            // DATA VALIDATION. SELECT: Address by Id
            if (Id > 0)
            {
                return SelectIdAddress(Id);
            }

            else

            {
                return null;
            }
        }

        // UPDATE: Address by Id
        public void AddressRequestModel(AddressRequestModel model, int Id)
        {
            int updateAddressId = Id;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_UpdateById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Address1", model.Address1);
                   paramCollection.AddWithValue("@City", model.City);
                   paramCollection.AddWithValue("@State", model.State);
                   paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                   paramCollection.AddWithValue("@Latitude", model.Latitude);
                   paramCollection.AddWithValue("@Longitude", model.Longitude);

                   paramCollection.AddWithValue("@Id", updateAddressId);
               });
        }

        // GET: Address by Id
        public AddressRequestModel SelectById(int AddressId)
        {
            AddressRequestModel AddressItem = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", AddressId);
                }
            , map: delegate (IDataReader reader, short set)
            {
                AddressItem = new AddressRequestModel();

                int startingIndex = 0; //startingOrdinal

                AddressItem.UserId = reader.GetSafeString(startingIndex++);
                AddressItem.Date = reader.GetSafeDateTime(startingIndex++);
                AddressItem.Address1 = reader.GetSafeString(startingIndex++);
                AddressItem.City = reader.GetSafeString(startingIndex++);
                AddressItem.State = reader.GetSafeString(startingIndex++);
                AddressItem.ZipCode = reader.GetSafeString(startingIndex++);
                AddressItem.Latitude = reader.GetSafeDecimal(startingIndex++);
                AddressItem.Longitude = reader.GetSafeDecimal(startingIndex++);
                AddressItem.Id = reader.GetSafeInt32(startingIndex++);

            });

            return AddressItem;
        }

        // SELECT: All Addresses
        public List<Addresses> SelectAll()
        {
            List<Addresses> listAddresses = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_SelectAll"
                , inputParamMapper: null
            , map: delegate (IDataReader reader, short set)
            {
                Addresses AddressItem = new Addresses();

                int startingIndex = 0;

                AddressItem.UserId = reader.GetSafeString(startingIndex++);
                AddressItem.Date = reader.GetSafeDateTime(startingIndex++);
                AddressItem.Address1 = reader.GetSafeString(startingIndex++);
                AddressItem.City = reader.GetSafeString(startingIndex++);
                AddressItem.State = reader.GetSafeString(startingIndex++);
                AddressItem.ZipCode = reader.GetSafeString(startingIndex++);
                AddressItem.Latitude = reader.GetSafeDecimal(startingIndex++);
                AddressItem.Longitude = reader.GetSafeDecimal(startingIndex++);
                AddressItem.Id = reader.GetSafeInt32(startingIndex++);

                if (listAddresses == null)
                {
                    listAddresses = new List<Addresses>();
                }

                listAddresses.Add(AddressItem);

            });

            return listAddresses;
        }

        // DELETE: Address By Id
        public void DeleteById(int Id)
        {
            int deleteAddressId = Id;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_DeleteById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", deleteAddressId);

               });
        }
    }
}
