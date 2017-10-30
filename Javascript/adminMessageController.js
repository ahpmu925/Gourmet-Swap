(function () {
    angular.module(APP.NAME)
        .controller('adminMessageController', AdminMessageController);

    AdminMessageController.$inject = ['adminContactService', "$stateParams", "$state", "$scope", "$rootScope"]

    function AdminMessageController(adminContactService, $stateParams, $state, $scope, $rootScope) {

        var vm = this;

        vm.$onInit = _onInit;

        function _onInit() {

            vm.message = $stateParams.message;
            _updateStatus();
        }

        function _updateStatus() {

            var status = 3;
            var id = $stateParams.message.id;

            adminContactService.updateStatus(id, status, _onUpdateStatusSuccess, _onUpdateStatusError);


        }

        function _onUpdateStatusSuccess(response) {



        };
        function _onUpdateStatusError(response) {

        };
    };
})();

