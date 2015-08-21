app.controller('DashController', function($scope, Azure, $state) {

  $scope.isIOS = ionic.Platform.isIOS();
  $scope.isAndroid = ionic.Platform.isAndroid();

  refreshDashboard();


  function refreshDashboard() {

    Azure.getStatusList()
      .then(function (statusList) {

        $scope.maxNumberOfIncidents = Math.max.apply(Math,statusList.map(function(o){
          if (o.numberOfIncidents) {
            return o.numberOfIncidents;
          }
          return 0;
        }))
        $scope.maxAverageWaitTime = Math.max.apply(Math,statusList.map(function(o){
          if (o.averageWaitTime) {
            return o.averageWaitTime;
          }
          return 0;
        }))

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
