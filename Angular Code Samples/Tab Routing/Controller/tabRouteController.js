(function () {

    angular.module(APPNAME)
        .controller('tabController', TabController);

    TabController.$inject = ['$scope', '$baseController'];

    function TabController(
        $scope
        , $baseController) {

        var vm = this;

        $baseController.merge(vm, $baseController);

        // RUN: scope
        vm.$scope = $scope;

        // DEFINE: Tab Route & Label
        vm.tabs = [
            { link: '#/home', label: 'Home' },
            { link: '#/reviews', label: 'Review' },
            { link: '#/media', label: 'Media' },
            { link: '#/followers', label: 'Followers' }
        ];

        // SET: Default Tab
        vm.selectedTab = vm.tabs[0];

        // SET: Selected Tab
        vm.tabClass = _tabClass;
        vm.setSelectedTab = _setSelectedTab;

        function _tabClass(tab) {
            if (vm.selectedTab == tab) {
                return "active";

            } else {
                return "";

            }
        }

        function _setSelectedTab(tab) {
            console.log("Set Selected Tab as: ", tab);
            vm.selectedTab = tab;
        }

    }

})();