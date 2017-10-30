(function () {

    angular.module(APP.NAME)
        .factory('blogService', BlogService);

    BlogService.$inject = ["$http"];

    function BlogService($http) {



        var svc = {};

        svc.getBlogs = _getBlogs;

        svc.updateBlog = _updateBlog;

        svc.addBlog = _addBlog;

        svc.getCategories = _getCategories;

        return svc;

        function _getBlogs(onSuccess, onError) {

            var settings = {
                url: '/api/blogs',
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }

        function _updateBlog(id, data, onSuccess, onError) {

            var settings = {
                url: '/api/blogs/' + id,
                method: 'PUT',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify(data),
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }

        function _addBlog(data, onSuccess, onError) {

            var settings = {
                url: '/api/blogs',
                method: 'POST',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify(data),
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }

        function _getCategories(onSuccess, onError) {

            var settings = {
                url: '/api/blogs/categories',
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }


    };
})();