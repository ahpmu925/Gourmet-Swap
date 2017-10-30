using GSwap.Models;
using GSwap.Models.Domain;
using GSwap.Models.Requests.Blog;
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
using System.Web.Http;

namespace GSwap.Web.Controllers
{
    [RoutePrefix("api/blogs")]
    public class BlogApiController : ApiController
    {
        private IBlogService _blogService;
        private IUserAuthData _currentUser;
        public IPrincipal _principal = null;


        public BlogApiController(IBlogService BlogService, IPrincipal user)
        {
            _principal = user;
            _blogService = BlogService;
            _currentUser = _principal.Identity.GetCurrentUser();
        }


        [Route(), HttpGet]
        public HttpResponseMessage GetAll()
        {

            HttpStatusCode code = HttpStatusCode.OK;

            ItemsResponse<Blog> response = new ItemsResponse<Blog>();
            response.Items = _blogService.GetAll();

            if (response.Items == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }

        [Route("categories"), HttpGet]
        public HttpResponseMessage GetAllCategories()
        {

            HttpStatusCode code = HttpStatusCode.OK;

            ItemsResponse<BlogCategories> response = new ItemsResponse<BlogCategories>();
            response.Items = _blogService.GetAllCategories();

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


            ItemResponse<Blog> response = new ItemResponse<Blog>();
            response.Item = _blogService.Get(id);

            if (response.Item == null)
            {
                code = HttpStatusCode.NotFound;
                response.IsSuccessful = false;
            }
            return Request.CreateResponse(code, response);
        }

        [Route(), HttpPost]
        public HttpResponseMessage Add(BlogAddRequest model)

        {
            if (!ModelState.IsValid)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
          
            ItemResponse<int> response = new ItemResponse<int>();
            int newId = _blogService.Add(model, _currentUser.Id);
            response.Item = newId;

            return Request.CreateResponse(HttpStatusCode.OK, response);


        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(BlogUpdateRequest model)

        {
            if (!ModelState.IsValid)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _blogService.Update(model, _currentUser.Id);

            HttpStatusCode code = HttpStatusCode.OK;

            SuccessResponse response = new SuccessResponse();


            return Request.CreateResponse(code, response);

        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)

        {
            _blogService.Delete(id);

            SuccessResponse response = new SuccessResponse();

            HttpStatusCode code = HttpStatusCode.OK;

            return Request.CreateResponse(code, response);

        }

        [Route("all/{page:int}/{pagesize:int}"), HttpGet]
        public HttpResponseMessage GetBlogPaging(int page, int pagesize)
        {


            HttpStatusCode code = HttpStatusCode.OK;


            ItemResponse<PagedList<Blog>> response = new ItemResponse<PagedList<Blog>>();

            response.Item = _blogService.GetBlogPaging(page, pagesize);


            if (response.Item == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }

        [Route("published/{page:int}/{pagesize:int}"), HttpGet]
        public HttpResponseMessage GetPublishedBlogs(int page, int pagesize)
        {


            HttpStatusCode code = HttpStatusCode.OK;


            ItemResponse<PagedList<Blog>> response = new ItemResponse<PagedList<Blog>>();

            response.Item = _blogService.GetPublishedBlogs(page, pagesize);


            if (response.Item == null)
            {
                ErrorResponse error = new ErrorResponse("no message was found");

                code = HttpStatusCode.NotFound;
                return Request.CreateResponse(code, error);
            }
            return Request.CreateResponse(code, response);
        }



    }
}
