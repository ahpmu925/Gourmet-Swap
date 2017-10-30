(function () {
    angular.module(APP.NAME)
        .controller('adminContactController', AdminContactController);

    AdminContactController.$inject = ['adminContactService', "$stateParams", "$state", "eventService"]

    function AdminContactController(adminContactService, $stateParams, $state, eventService) {

        var vm = this;

        vm.flags = {};

        vm.checkBox = _checkBox;

        vm.checkAll = _checkAll;

        vm.updateArchive = _updateArchive;

        vm.pageChanged = _pageChanged;

        vm.toMessage = _toMessage;

        vm.replyEmail = _replyEmail;

        vm.$onInit = _onInit;

        vm.currentPage = 1;

        vm.numPerPage = 10;

        vm.search;

        eventService.listen("onSearchRequested", _onEventListen);




        function _onInit() {


            vm.pageIndex = vm.currentPage - 1;
            adminContactService.getSearch($stateParams.status, vm.pageIndex, vm.numPerPage, vm.search, _contactSuccess, _contactError);

        }

        function _pageChanged() {

            vm.pageIndex = vm.currentPage - 1;
            adminContactService.getSearch($stateParams.status, vm.pageIndex, vm.numPerPage, vm.search, _contactSuccess, _contactError);


        }

        function _contactSuccess(response) {

            vm.messages = response.data.item.pagedItems;


            if (vm.messages != null) {
                for (var i = 0; i < vm.messages.length; i++) {
                    var currentMessage = vm.messages[i];
                    //Do not use vm.messages below this line inside the loop


                    currentMessage.dateAdded = moment(currentMessage.dateAdded).format('MM-DD-YYYY');
                    //changes the value of datedAdded property of currentMessage

                }



                vm.totalCount = response.data.item.totalCount;
                vm.maxSize = response.data.item.totalPages;
                vm.status = $stateParams.status;

                if (vm.status == 1) {
                    vm.showArchive = true;
                }

                else {
                    vm.showArchive = false;

                }
            }
        };

        function _contactError(response) {
            vm.messages = null;
          
           
        };


        function _checkAll() {
            angular.forEach(vm.messages, _processSingleItem);

        }

        function _processSingleItem(item) {


            item.selected = vm.selectedAll;

            if (vm.selectedAll) {
                vm.flags[item.id] = true;
                vm.enableArchiveBtn = true;
            } else {
                delete vm.flags[item.id];
                vm.enableArchiveBtn = false;
            }
        }

        function _checkBox(item) {

            if (item.selected) {

                vm.flags[item.id] = true;

            } else {

                delete vm.flags[item.id];
            }
            var flagsLength = Object.keys(vm.flags).length;
            if (flagsLength == vm.messages.length) {
                vm.selectedAll = true;


            } else {
                vm.selectedAll = false;

            }

            if (flagsLength > 0) {
                vm.enableArchiveBtn = true;

                if (flagsLength == 1) {
                    vm.enableReplyBtn = true;
                } else if (flagsLength > 1) {
                    vm.enableReplyBtn = false;
                }

            } else {
                vm.enableArchiveBtn = false;
                vm.enableReplyBtn = false;
            }

        }


        function _updateArchive() {
            for (var i = 0; i < vm.messages.length; i++) {
                if (vm.messages[i].selected) {



                    var status = 2;
                    var id = vm.messages[i].id;

                    adminContactService.updateStatus(vm.messages[i].id, status, _onUpdateStatusSuccess, _onUpdateStatusError);


                }
            }
            function _onUpdateStatusSuccess(response) {

                var index = vm.messages.findIndex(x => x.id == idUpdated);
                vm.messages.splice(index, 1);


            };
            function _onUpdateStatusError(response) {

            };
        }


        function _toMessage(item) {

            $state.go('admin.inbox.message', { id: item.id, message: item });
        }

        function _onEventListen(events, data) {

            var searchTerm = data;
            vm.search = searchTerm;
            _pageChanged();
        }

        function _replyEmail() {
            for (var i = 0; i < vm.messages.length; i++) {
                if (vm.messages[i].selected) {

                    var id = vm.messages[i].id;

                    $state.go('admin.inbox.compose', { id: id, message: vm.messages[i] });

                }
            }
        };
    }
})();

