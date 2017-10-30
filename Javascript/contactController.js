(function () {

    angular
        .module(APP.NAME).controller("contactController", ContactController);

    ContactController.$inject = ['contactService', "toastr"];

    function ContactController(contactService, toastr) {
        var vm = this;

        vm.contact = null;

        vm.onSubmitBtnClicked = _onSubmitBtnClicked;

        function _onSubmitBtnClicked(isValid) {

            
             if (isValid) {

                 contactService.addContact(vm.contact, _onAddContactSuccess, _onAddContactError);

             }

             else {
                 toastr.error("Oops!");
             }
        }

        function _onAddContactSuccess(response) {
            toastr.success("Your Message Has Been Sent. We'll Get Back To You Shortly");
        };
        function _onAddContactError(response) {

        };

    }

})();
