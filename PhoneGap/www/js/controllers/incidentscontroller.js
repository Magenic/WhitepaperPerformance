app.controller('IncidentsController', function($scope, Azure, $stateParams) {

  // get user
  var userId = $stateParams.userId;
  var userFullName = $stateParams.userFullName;
  $scope.selectedUserId = userId;
  $scope.selectedUserFullName = userFullName;

  // setup open / closed
  $scope.showOpen = true;

  // do initial load of incidents
  refreshIncidents();

  $scope.doRefresh = function() {
    refreshIncidents();
  };

  function refreshIncidents() {

    Azure.getIncidentList(userId)
      .then(function (incidentList) {

        $scope.incidentList = incidentList;
        $scope.$broadcast('scroll.refreshComplete');

      });

  };

});
