(function () {
    angular.module(APP.NAME)
        .controller('adminInboxController', AdminInboxController);

    AdminInboxController.$inject = ["$state",  "eventService", 'adminContactService']

    function AdminInboxController($state, eventService, adminContactService) {

        var vm = this;

        vm.searchTerm;

        vm.onSearchClicked = _onSearchClicked;

        function _onSearchClicked() {

            eventService.broadcast("onSearchRequested", vm.searchTerm);

        }

    };
})();

