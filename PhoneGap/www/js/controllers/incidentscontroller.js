app.controller('IncidentsController', function($scope, Azure, $stateParams, $state, $ionicLoading) {

  // get user
  var userId = $stateParams.userId;
  var userFullName = $stateParams.userFullName;
  $scope.selectedUserId = userId;
  $scope.selectedUserFullName = userFullName;

  // setup open / closed
  $scope.currentlyShowingOpen = true;

  // do initial load of incidents
  refreshIncidents();

  $scope.doRefresh = function() {
    refreshIncidents();
  };

  $scope.showOpen = function() {
    if ($scope.currentlyShowingOpen != true) {
        $scope.currentlyShowingOpen = true;
        refreshIncidents();
    }
  };

  $scope.showClosed = function() {
    if ($scope.currentlyShowingOpen != false) {
        $scope.currentlyShowingOpen = false;
        refreshIncidents();
    }
  };

  $scope.selectIncident = function(incidentId) {

    $state.go('incident-detail', {
      incidentId: incidentId
    });

  };

  function refreshIncidents() {

    $ionicLoading.show({
      template: 'loading'
    });

    Azure.getIncidentList(userId)
      .then(function (incidentList) {

        var showClosed = !$scope.currentlyShowingOpen;
        $scope.incidentList = incidentList.filter(function (el) { return el.closed == showClosed; });
        $scope.$broadcast('scroll.refreshComplete');
        $ionicLoading.hide();

      });

  };

});
