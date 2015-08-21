app.controller('DashController', function($scope, Azure, $state) {

  $scope.isIOS = ionic.Platform.isIOS();
  $scope.isAndroid = ionic.Platform.isAndroid();

  refreshDashboard();

  function refreshDashboard() {

    Azure.getStatusList()
      .then(function (statusList) {

        $scope.statusList = statusList;
        $scope.$broadcast('scroll.refreshComplete');

      });

  };

  $scope.doRefresh = function() {
    refreshDashboard();
  };

  $scope.selectUser = function(userId, userFullName) {

    // go to incident queue for user ...
    $state.go('incidents', {
      userId: userId,
      userFullName: userFullName
    });

  };

  $scope.addIncident = function() {

    // add new incident
    $state.go('add-incident');

  };

});
