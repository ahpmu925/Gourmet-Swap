using GSwap.Data;
using GSwap.Data.Providers;
using GSwap.Models;
using GSwap.Models.Domain;
using GSwap.Models.Requests.Blog;
using GSwap.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Services
{
    public class BlogService : IBlogService
    {
        private IDataProvider _dataProvider;

        public BlogService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Add(BlogAddRequest model, int userId)
        {


            int id = 0;

            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)
        {

            paramCollection.AddWithValue("@Title", model.Title);
            paramCollection.AddWithValue("@Body", model.Body);
            paramCollection.AddWithValue("@IsPublished", model.IsPublished);
            paramCollection.AddWithValue("@UserId", model.UserId);

            SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            idParameter.Direction = System.Data.ParameterDirection.Output;

            SqlParameter blogCategoryId = new SqlParameter("@IntIdTable", System.Data.SqlDbType.Structured);


            if (model.Ids != null && model.Ids.Any())
            {
                IntIdTable idsTable = new IntIdTable(model.Ids);
                blogCategoryId.Value = idsTable;

            }

            paramCollection.Add(blogCategoryId);
            paramCollection.Add(idParameter);

        };

            Action<SqlParameterCollection> returnParamDelegate = delegate (SqlParameterCollection paramCollection)
            {
                Int32.TryParse(paramCollection["@Id"].Value.ToString(), out id);
            };

            string proc = "dbo.Blogs_Insert";

            _dataProvider.ExecuteNonQuery(proc, inputParamDelegate, returnParamDelegate);


            return id;


        }

        public Blog Get(int id)
        {

            Blog blog = null;
            List<int> myIntegers = null;

            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Id", id);
                //strings have to match the stored proc parameter names 
            };

            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
                {

                    int startingIndex = 0;
                    if (set == 0)
                    {

                        //blog = new Blog();
                        blog = new Blog();
                        //int startingIndex = 0; //startingOrdinal
                        blog.Id = reader.GetSafeInt32(startingIndex++);
                        blog.Title = reader.GetSafeString(startingIndex++);
                        blog.Body = reader.GetSafeString(startingIndex++);
                        blog.IsPublished = reader.GetSafeBool(startingIndex++);
                        blog.UserId = reader.GetSafeInt32(startingIndex++);
                        blog.DateAdded = reader.GetSafeDateTime(startingIndex++);
                        blog.DateModified = reader.GetSafeDateTime(startingIndex++);

                    }

                    else if (set == 1)

                    {


                        //int startingIndex = 0;
                        //blog.Ids = reader.GetSafeInt32(startingIndex++);


                        if (myIntegers == null)
                        {
                            myIntegers = new List<int>();

                        }


                        myIntegers.Add(reader.GetSafeInt32(1));
                    }
                };


            _dataProvider.ExecuteCmd("dbo.Blogs_SelectById", inputParamDelegate, singleRecMapper);
            //int ten = 10;
            //int eleven = 11;
            //int twelve = 12;


            //myIntegers.Add(ten);
            //myIntegers.Add(eleven);
            //myIntegers.Add(twelve);
            blog.Ids = myIntegers;
            return blog;

        }

        private static Blog MapBlog(IDataReader reader)
        {
            Blog blog = new Blog();
            int startingIndex = 0; //startingOrdinal

            blog.Id = reader.GetSafeInt32(startingIndex++);
            blog.Title = reader.GetSafeString(startingIndex++);
            blog.Body = reader.GetSafeString(startingIndex++);
            blog.IsPublished = reader.GetSafeBool(startingIndex++);
            blog.UserId = reader.GetSafeInt32(startingIndex++);
            blog.DateAdded = reader.GetSafeDateTime(startingIndex++);
            blog.DateModified = reader.GetSafeDateTime(startingIndex++);

            return blog;
        }
        


        public List<Blog> GetAll()
        {
            List<Blog> list = null;
            Dictionary<int, List<int>> catStore = null;

            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
            {

                if (set == 0)
                {
                    Blog blog = MapBlog(reader);
                    if (list == null)
                    {
                        list = new List<Blog>();
                    }
                    list.Add(blog);
                }
                else if(set == 1)
                {
                    int catId = 0;//
                    int blogId = 0;//

                    blogId = reader.GetSafeInt32(0);
                    catId = reader.GetSafeInt32(1);

                    if (catStore == null)
                    {
                        catStore = new Dictionary<int, List<int>>();
                    }

                    if(!catStore.ContainsKey(blogId))
                    {
                        catStore.Add(blogId, new List<int>());
                    }

                    catStore[blogId].Add(catId);



                }
            };

            Action<SqlParameterCollection> inputParamDelegate = null;

            _dataProvider.ExecuteCmd("dbo.Blogs_SelectAll", inputParamDelegate, singleRecMapper);

            if(list != null && catStore!= null)
            {
                foreach (Blog currentBlog in list)
                {
                    if(catStore.ContainsKey(currentBlog.Id))
                    {
                        currentBlog.Ids = catStore[currentBlog.Id];
                    }
                }

            }



            return list;

        }

        public List<BlogCategories> GetAllCategories()
        {
            List<BlogCategories> list = null;
            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)

            {
                BlogCategories categories = new BlogCategories();
                int startingIndex = 0; //startingOrdinal

                categories.Id = reader.GetSafeInt32(startingIndex++);
                categories.Name = reader.GetSafeString(startingIndex++);


                if (list == null)
                {
                    list = new List<BlogCategories>();
                }

                list.Add(categories);
            };

            Action<SqlParameterCollection> inputParamDelegate = null;

            _dataProvider.ExecuteCmd("dbo.BlogCategories_SelectCategories", inputParamDelegate, singleRecMapper);

            return list;

        }

        public void Update(BlogUpdateRequest model, int UserId)

        {


            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@Title", model.Title);
                paramCollection.AddWithValue("@Body", model.Body);
                paramCollection.AddWithValue("@IsPublished", model.IsPublished);
                paramCollection.AddWithValue("@UserId", UserId);

                SqlParameter blogCategoryId = new SqlParameter("@IntIdTable", System.Data.SqlDbType.Structured);


                if (model.Ids != null && model.Ids.Any())
                {
                    IntIdTable idsTable = new IntIdTable(model.Ids);
                    blogCategoryId.Value = idsTable;
                }

                paramCollection.Add(blogCategoryId);

            };

            _dataProvider.ExecuteNonQuery("dbo.Blogs_Update", inputParamDelegate);
        }

        public void Delete(int id)
        {
            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {
                paramCollection.AddWithValue("@Id", id);
            };

            _dataProvider.ExecuteNonQuery("dbo.Blogs_DeleteById", inputParamDelegate);
        }


        public PagedList<Blog> GetBlogPaging(int page, int pagesize)
        {
            int totalCount = 0;

            List<Blog> list = null;

            PagedList<Blog> pagedList = null;



            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
            {
                Blog singleBlog = MapBlog(reader);

                totalCount = reader.GetSafeInt32(7);



                if (list == null)
                {
                    list = new List<Blog>();
                }

                list.Add(singleBlog);
            };



            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {

                paramCollection.AddWithValue("@PageNumber", page);
                paramCollection.AddWithValue("@RecsPerPage", pagesize);


                //strings have to match the stored proc parameter names
            };


            _dataProvider.ExecuteCmd("dbo.Blogs_Pagination", inputParamDelegate, singleRecMapper);

            if (pagedList == null && list != null)
            {
                pagedList = new PagedList<Blog>(list, page, pagesize, totalCount);
            }
            return pagedList;

        }


        public PagedList<Blog> GetPublishedBlogs(int page, int pagesize)
        {
            int totalCount = 0;

            List<Blog> list = null;

            PagedList<Blog> pagedList = null;



            Action<IDataReader, short> singleRecMapper = delegate (IDataReader reader, short set)
            {
                Blog singleBlog = MapBlog(reader);

                totalCount = reader.GetSafeInt32(7);



                if (list == null)
                {
                    list = new List<Blog>();
                }

                list.Add(singleBlog);
            };



            Action<SqlParameterCollection> inputParamDelegate = delegate (SqlParameterCollection paramCollection)

            {

                paramCollection.AddWithValue("@PageNumber", page);
                paramCollection.AddWithValue("@RecsPerPage", pagesize);


                //strings have to match the stored proc parameter names
            };


            _dataProvider.ExecuteCmd("dbo.Blogs_Pagination", inputParamDelegate, singleRecMapper);

            if (pagedList == null && list != null)
            {
                pagedList = new PagedList<Blog>(list, page, pagesize, totalCount);
            }
            return pagedList;

        }
    }
}


