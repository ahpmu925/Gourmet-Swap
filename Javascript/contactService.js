(function () {

    angular
        .module(APP.NAME).factory("contactService", ContactService);

    ContactService.$inject = ["$log", "$http"];

    function ContactService($log, $http) {

        var vm = this;          

        var svc = {};
        svc.addContact = _addContact;
        svc.getContact = _getContact;
        svc.deleteContact = _deleteContact;
   
        return svc;

        function _addContact(data, onSuccess, onError) {

            var settings = {
                url: '/api/messages',
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
     

        function _getContact(onSuccess, onError) {

            var settings = {
                url: 'api/messages',
                method: 'GET',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                withCredentials: true,
            };

            return $http(settings)
                .then(onSuccess, onError)

        }


        function _deleteContact(id, onSuccess, onError) {

            var settings = {
                url: 'api/messages/' + id,
                method: 'DELETE',
                headers: {},
                cache: false,
                contentType: 'application/json; charset=UTF-8',
                withCredentials: true,
            };

            return $http(settings)
                .then(function (response) {
                    onSuccess(response, id);
                }, onError)

        }

    }
})();
