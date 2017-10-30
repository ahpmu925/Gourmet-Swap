(function () {
    angular.module(APP.NAME)
        .controller('blogController', BlogController);

    BlogController.$inject = ['blogService', "$state"]

    function BlogController(blogService, $state) {

        var vm = this;

        vm.$onInit = _onInit;

        vm.editBlog = _editBlog;

        vm.newBlog = _newBlog;

        function _onInit() {



            blogService.getBlogs(_blogSuccess, _blogError);
         
        }

        function _blogSuccess(response) {

            vm.blogs = response.data.items;


        };

        function _blogError(response) {

        };

        function _editBlog(blog) {

            $state.go('admin.write', { id: blog.id, blog: blog });

        }

        function _newBlog(blog) {

            $state.go('admin.write', { blog: blog });

        }
    }
})();

