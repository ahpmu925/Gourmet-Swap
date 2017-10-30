using GSwap.Models;
using GSwap.Models.Domain;
using GSwap.Models.Requests;
using GSwap.Models.Requests.Email;
using GSwap.Models.Responses;
using GSwap.Services;
using GSwap.Services.Interfaces;
using GSwap.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

namespace GSwap.Web.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessagesApiController : ApiController
    {
        private IMessageService _messageService;
        private IEmailService _emailService;
        private IUserAuthData _currentUser;
        public IPrincipal _principal = null;


        public MessagesApiController(IMessageService MessageService, IEmailService emailService, IPrincipal user)
        {
            _principal = user;
            _messageService = MessageService;
            _emailService = emailService;
            _currentUser = _principal.Identity.GetCurrentUser();
        }

        [Route(), HttpGet]
        public HttpResponseMessage GetAll()
        {

            HttpStatusCode code = HttpStatusCode.OK;

            ItemsResponse<ContactMessage> response = new ItemsResponse<ContactMessage>();
            response.Items = _messageService.GetAll();

            if (response.Items == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }


        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
           
            HttpStatusCode code = HttpStatusCode.OK;


            ItemResponse<ContactMessage> response = new ItemResponse<ContactMessage>();
            response.Item = _messageService.Get(id);

            if (response.Item == null)
            {
                code = HttpStatusCode.NotFound;
                response.IsSuccessful = false;
            }
            return Request.CreateResponse(code, response);
        }

        [Route(), HttpPost][AllowAnonymous]
        public async Task<HttpResponseMessage> Add(ContactUsAddRequest model)

        {
            if (!ModelState.IsValid)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            ItemResponse<int> response = new ItemResponse<int>();
            int newId = 0;
            if (_currentUser == null )
            {
                newId = _messageService.Add(model, 0);
                
            }
            else
            {
                newId = _messageService.Add(model, _currentUser.Id);
            }

           
            response.Item = newId;

            bool result = await _emailService.SendEmailAsync(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
       




        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(ContactUsUpdateRequest model)

        {
            if (!ModelState.IsValid)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _messageService.Update(model, _currentUser.Id);

            HttpStatusCode code = HttpStatusCode.OK;

            SuccessResponse response = new SuccessResponse();


            return Request.CreateResponse(code, response);

        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)

        {
            _messageService.Delete(id);

            SuccessResponse response = new SuccessResponse();

            HttpStatusCode code = HttpStatusCode.OK;

            return Request.CreateResponse(code, response);

        }
        [Route("{id:int}/status"), HttpPut]
        public HttpResponseMessage UpdateStatus(ContactUsStatusRequest model)

        {
            if (!ModelState.IsValid)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _messageService.UpdateStatus(model, _currentUser.Id, model.Status);

            HttpStatusCode code = HttpStatusCode.OK;

            SuccessResponse response = new SuccessResponse();


            return Request.CreateResponse(code, response);

        }


        [Route("status/{status:int}"), HttpGet]
        public HttpResponseMessage GetStatus(int status)
        {
            
            HttpStatusCode code = HttpStatusCode.OK;


            ItemsResponse<ContactMessage> response = new ItemsResponse<ContactMessage>();

            response.Items = _messageService.GetStatus(status);

            if (response.Items == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }


        [Route("inbox/{status:int}/{page:int}/{pagesize:int}"), HttpGet]
        public HttpResponseMessage GetMessages(int status, int page, int pagesize)
        {


            HttpStatusCode code = HttpStatusCode.OK;


            ItemResponse<PagedList<ContactMessage>> response = new ItemResponse<PagedList<ContactMessage>>();

            response.Item = _messageService.GetMessages(status, page, pagesize);


            if (response.Item == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }


        [Route("inbox/{status:int}/{page:int}/{pagesize:int}"), HttpGet]
        public HttpResponseMessage SearchMessages(int status, int page, int pagesize, string searchTerm)
        {


            HttpStatusCode code = HttpStatusCode.OK;


            ItemResponse<PagedList<ContactMessage>> response = new ItemResponse<PagedList<ContactMessage>>();

            response.Item = _messageService.SearchMessages(status, page, pagesize, searchTerm);


            if (response.Item == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }

           [Route("reply"), HttpPost]
            public async Task<HttpResponseMessage> Reply(ContactUsReplyRequest model)
            {

                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                };

            bool result = await _emailService.Reply(model);
                SuccessResponse success = null;
                if (result == true)
                {
                    success = new SuccessResponse();
                }
                return Request.CreateResponse(HttpStatusCode.OK, success);
            }

        }

    }
    

