app.controller('IncidentsController', function($scope, Azure, $stateParams, $state) {

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

  $scope.selectIncident = function(incidentId) {

    $state.go('incident-detail', {
      incidentId: incidentId
    });

  };

  function refreshIncidents() {

    Azure.getIncidentList(userId)
      .then(function (incidentList) {

        $scope.incidentList = incidentList;
        $scope.$broadcast('scroll.refreshComplete');

      });

  };

});
