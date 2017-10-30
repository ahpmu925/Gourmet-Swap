using GSwap.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSwap.Models.Domain;
using GSwap.Models.Requests;
using GSwap.Data.Providers;
using System.Data.SqlClient;
using System.Data;
using GSwap.Data;
using GSwap.Models;

namespace GSwap.Services
{
    public class MessageService : IMessageService
    {
        private IDataProvider _dataProvider;

        public MessageService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Add(ContactUsAddRequest model, int userId)
        {
            int id = 0;
            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@FirstName", model.FirstName);
                paramCollection.AddWithValue("@LastName", model.LastName);
                paramCollection.AddWithValue("@Email", model.Email);
                paramCollection.AddWithValue("@PhoneNo", model.PhoneNo);
                paramCollection.AddWithValue("@City", model.City);
                paramCollection.AddWithValue("@Address", model.Address);
                paramCollection.AddWithValue("@Message", model.Message);
                paramCollection.AddWithValue("@UserId", userId);

                SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                idParameter.Direction = System.Data.ParameterDirection.Output;


                paramCollection.Add(idParameter);

            };

            Action<SqlParameterCollection> returnParamDelegate = delegate (SqlParameterCollection paramCollection)
                {
                    Int32.TryParse(paramCollection["@Id"].Value.ToString(), out id);
                };

            string proc = "dbo.Messages_Insert";

            _dataProvider.ExecuteNonQuery(proc, inputParamDelegate, returnParamDelegate);


            return id;

        }

        public ContactMessage Get(int id)
        {

            ContactMessage contact = null;

            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
            {
                contact = new ContactMessage();

                int startingIndex = 0; 

                contact.Id = reader.GetSafeInt32(startingIndex++);
                contact.FirstName = reader.GetSafeString(startingIndex++);
                contact.LastName = reader.GetSafeString(startingIndex++);
                contact.Email = reader.GetSafeString(startingIndex++);
                contact.PhoneNo = reader.GetSafeString(startingIndex++);
                contact.City = reader.GetSafeString(startingIndex++);
                contact.Address = reader.GetSafeString(startingIndex++);
                contact.Message = reader.GetSafeString(startingIndex++);
                contact.UserId = reader.GetSafeInt32(startingIndex++);
                contact.DateAdded = reader.GetSafeDateTime(startingIndex++);
                contact.DateModified = reader.GetSafeDateTime(startingIndex++);
                contact.Status = reader.GetSafeInt32(startingIndex++);

            };
            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Id", id);
          
            };

            _dataProvider.ExecuteCmd("dbo.Messages_SelectById", inputParamDelegate, singleRecMapper);

            return contact;

        }





        public List<ContactMessage> GetAll()
        {
            List<ContactMessage> list = null;
            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)

            {
                ContactMessage contact = MapContactMessage(reader);

                if (list == null)
                {
                    list = new List<ContactMessage>();
                }

                list.Add(contact);
            };

            Action<SqlParameterCollection> inputParamDelegate = null;

            _dataProvider.ExecuteCmd("dbo.Messages_SelectAll", inputParamDelegate, singleRecMapper);

            return list;

        }

        private static ContactMessage MapContactMessage(IDataReader reader)
        {
            ContactMessage contact = new ContactMessage();
            int startingIndex = 0; 

            contact.Id = reader.GetSafeInt32(startingIndex++);
            contact.FirstName = reader.GetSafeString(startingIndex++);
            contact.LastName = reader.GetSafeString(startingIndex++);
            contact.Email = reader.GetSafeString(startingIndex++);
            contact.PhoneNo = reader.GetSafeString(startingIndex++);
            contact.City = reader.GetSafeString(startingIndex++);
            contact.Address = reader.GetSafeString(startingIndex++);
            contact.Message = reader.GetSafeString(startingIndex++);
            contact.UserId = reader.GetSafeInt32(startingIndex++);
            contact.DateAdded = reader.GetSafeDateTime(startingIndex++);
            contact.DateModified = reader.GetSafeDateTime(startingIndex++);
            contact.Status = reader.GetSafeInt32(startingIndex++);
        
            return contact;
        }

        public void Update(ContactUsUpdateRequest model, int UserId)

        {
            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@FirstName", model.FirstName);
                paramCollection.AddWithValue("@LastName", model.LastName);
                paramCollection.AddWithValue("@Email", model.Email);
                paramCollection.AddWithValue("@PhoneNo", model.PhoneNo);
                paramCollection.AddWithValue("@City", model.City);
                paramCollection.AddWithValue("@Address", model.Address);
                paramCollection.AddWithValue("@Message", model.Message);
                paramCollection.AddWithValue("@UserId", UserId);



            };

            _dataProvider.ExecuteNonQuery("dbo.Messages_Update", inputParamDelegate);
        }

        public void Delete(int id)
        {
            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Id", id);
            };

            _dataProvider.ExecuteNonQuery("dbo.Messages_DeleteById", inputParamDelegate);
        }


        public void UpdateStatus(ContactUsStatusRequest model, int UserId, int Status)

        {
            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@UserId", UserId);
                paramCollection.AddWithValue("@Status", Status);

            };

            _dataProvider.ExecuteNonQuery("dbo.Messages_UpdateStatus", inputParamDelegate);
        }

        public List<ContactMessage> GetStatus(int status)
        {

            List<ContactMessage> list = null;

            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
            {
                ContactMessage getByStatus = MapContactMessage(reader);


                if (list == null)
                {
                    list = new List<ContactMessage>();
                }

                list.Add(getByStatus);
            };

            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Status", status);
         
            };

            _dataProvider.ExecuteCmd("dbo.Messages_SelectByStatus", inputParamDelegate, singleRecMapper);

            return list;

        }

        public PagedList<ContactMessage> GetMessages(int status, int page, int pagesize)
        {
            int totalCount = 0;

            
            List<ContactMessage> list = null;

            PagedList<ContactMessage> pagedList = null;



            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
              {
                  ContactMessage singleContactMessage = MapContactMessage(reader);

                  totalCount = reader.GetSafeInt32(12);

                  if (list == null)
                  {
                      list = new List<ContactMessage>();
                  }

                  list.Add(singleContactMessage);
              };

         

            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Status", status);
                paramCollection.AddWithValue("@PageNumber", page);
                paramCollection.AddWithValue("@RecsPerPage", pagesize);
                

           
            };


            _dataProvider.ExecuteCmd("dbo.Messages_Pagination", inputParamDelegate, singleRecMapper);

            if (pagedList == null && list != null)
            {
                pagedList = new PagedList<ContactMessage>(list, page, pagesize, totalCount);
            }
            return pagedList;

        }

        public PagedList<ContactMessage> SearchMessages(int status, int page, int pagesize, string searchTerm)
        {
            int totalCount = 0;


            List<ContactMessage> list = null;

            PagedList<ContactMessage> pagedList = null;



            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
            {
                ContactMessage searchContactMessage = MapContactMessage(reader);

                totalCount = reader.GetSafeInt32(12);
               

                if (list == null)
                {
                    list = new List<ContactMessage>();
                }

                list.Add(searchContactMessage);
            };



            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Status", status);
                paramCollection.AddWithValue("@PageNumber", page);
                paramCollection.AddWithValue("@RecsPerPage", pagesize);
                paramCollection.AddWithValue("@SearchTerm", searchTerm);


                //strings have to match the stored proc parameter names
            };


            _dataProvider.ExecuteCmd("dbo.Messages_Search", inputParamDelegate, singleRecMapper);

            if (pagedList == null && list != null)
            {
                pagedList = new PagedList<ContactMessage>(list, page, pagesize, totalCount);
            }
            return pagedList;

        }
    }
}












