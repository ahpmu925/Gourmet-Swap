(function () {

    angular.module(APP.NAME)
        .factory('adminContactService', AdminContactService);

    AdminContactService.$inject = ["$log", "$http"];

    function AdminContactService($log, $http) {



        var svc = {};

        svc.getContact = _getContact;

        svc.updateStatus = _updateStatus;

        svc.getContactByStatus = _getContactByStatus;

        svc.getPagination = _getPagination;

        svc.getSearch = _getSearch;

        svc.sendReply = _sendReply;

        return svc;


        function _getContact(onSuccess, onError) {

            var settings = {
                url: '/api/messages',
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }

        function _updateStatus(id, status, onSuccess, onError) {


        
            var settings = {
                url: '/api/messages/' + id + '/status',
                method: 'PUT',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify({
                    id: id,
                    status: status }),
                withCredentials: true,
            };
            
            return $http(settings)
                .then(function (response) {
                    onSuccess(response, id, status);
                }, onError)

        }

        function _getContactByStatus(status, onSuccess, onError) {

            var settings = {
                url: '/api/messages/status/' + status,
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }

        function _getPagination(status, page, pagesize, onSuccess, onError) {
            var settings = {
                url: '/api/messages/inbox/' + status + '/' + page + '/' + pagesize,
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                 withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }


        function _getSearch(status, page, pagesize, searchTerm, onSuccess, onError) {
            var settings = {
                url: '/api/messages/inbox/' + status + '/' + page + '/' + pagesize,
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                params: {
                    searchTerm: searchTerm
                },
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }

        function _sendReply(data, onSuccess, onError) {

            var settings = {
                url: '/api/messages/reply',
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

    };
})();