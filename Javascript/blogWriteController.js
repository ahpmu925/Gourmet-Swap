(function () {
    angular.module(APP.NAME)
        .controller('blogWriteController', BlogWriteController);

    BlogWriteController.$inject = ['blogService', "$state", "$stateParams", "toastr"]

    function BlogWriteController(blogService, $state, $stateParams, toastr) {

        var vm = this;

        vm.blog = null;

        var ids = [];

        vm.onCreateBtnClicked = _onCreateBtnClicked;

        vm.onEditBtnClicked = _onEditBtnClicked;

        vm.$onInit = _onInit;

        function _onInit() {

            vm.blog = $stateParams.blog;
            blogService.getCategories(_categoriesSuccess, _categoriesError);

        }


        function _categoriesSuccess(response) {

            vm.categories = response.data.items;

            if (vm.blog != null) {
                for (var j = 0; j < vm.categories.length; j++) {

                    for (var i = 0; i < vm.blog.ids.length; i++) {

                        if (vm.blog.ids[i] == vm.categories[j].id) {

                            vm.categories[j].selected = true;
                            vm.showEdit = true;
                        }
                    }
                }
            }
        };
      
        function _categoriesError(response) {

        };

        //    function _checkBox(category) {

        //        if (vm.blog.ids.indexOf(category) != -1) {

        //            return true;

        //        }

        //        else {
        //            return false;
        //        }
        //    }
        //}



        function _onCreateBtnClicked() {

            event.preventDefault();

            for (var i = 0; i < vm.categories.length; i++) {

                if (vm.categories[i].selected) {

                    ids.push(vm.categories[i].id);

                }
                vm.blog.ids = ids;

            }

            blogService.addBlog(vm.blog, _onAddBlogSuccess, _onAddBlogError);
        }

        function _onAddBlogSuccess(response) {
            toastr.success("You have successfully created a blog");
        };
        function _onAddBlogError(response) {
            toastr.error("Error");
        };

        function _onEditBtnClicked() {
            event.preventDefault();
            for (var i = 0; i < vm.categories.length; i++) {

                if (vm.categories[i].selected) {

                    ids.push(vm.categories[i].id);

                }
                vm.blog.ids = ids;

            }
            blogService.updateBlog(vm.blog.id, vm.blog, _onUpdateBlogSuccess, _onUpdateBlogError);

        }



        function _onUpdateBlogSuccess(response) {
            toastr.success("You have successfully edited a blog");
        };
        function _onUpdateBlogError(response) {
            toastr.error("Error");
        };
    }
})();

