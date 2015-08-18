app.controller('IncidentDetailController', function($scope, Azure, $stateParams) {

  // get incident
  var incidentId = $stateParams.incidentId;
  $scope.incidentId = incidentId;

  // do initial load of incidents
  refreshIncident();

  $scope.doRefresh = function() {
    refreshIncident();
  };

  function refreshIncident() {

    Azure.getIncident($scope.incidentId)
      .then(function (incident) {

        $scope.incident = incident;
        $scope.$broadcast('scroll.refreshComplete');

      });

  };

});
