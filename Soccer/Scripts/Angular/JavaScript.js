(function () {
    var MainController = function ($scope, $http) {

        var onLoad = function (response) {
            $scope.players = response.data;
        };

    }
})();
