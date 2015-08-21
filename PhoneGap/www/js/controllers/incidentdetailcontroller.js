app.controller('IncidentDetailController', function($scope, Azure, $stateParams, $timeout, $cordovaMedia, $ionicPopover, $ionicHistory) {

  // get incident
  var incidentId = $stateParams.incidentId;
  $scope.incidentId = incidentId;

  // do initial load of incidents
  refreshIncident();

  // get worker name
  var workersList;
  getWorker = function(workerId) {
    return workersList.filter(function (el) { return el.userId == workerId; })[0];
  };
  $scope.getWorkerFullName = function(workerId) {
    var worker = getWorker(workerId);
    if (worker == null) {
        return "unknown";
    }
    return worker.fullName;
  };
  $scope.getWorkerFirstName = function(workerId) {
    var worker = getWorker(workerId);
    if (worker == null) {
        return "unknown";
    }
    return worker.firstName;
  };

  // refresh incident
  $scope.doRefresh = function() {
    refreshIncident();
  };

  function refreshIncident() {

    Azure.getWorkersList()
      .then(function (workersListFromAzure) {

        workersList = workersListFromAzure;

        Azure.getIncident($scope.incidentId)
          .then(function (incident) {

            $scope.incident = incident;

            Azure.getIncidentDetails($scope.incidentId)
              .then(function (incidentDetails) {

                $scope.incidentDetails = incidentDetails;
                $scope.$broadcast('scroll.refreshComplete');

              });

            $timeout(function(){
              var imgHeight = bannerImageImg.offsetHeight;
              $scope.bannerHeight = imgHeight + 25;
            });

          });

      });

  };

  // add popover template
  $ionicPopover.fromTemplateUrl('templates/incident-detail-menu.html', {
    scope: $scope
  }).then(function(popover) {
    $scope.popover = popover;
  });

  // close incident
  $scope.closeIncident = function() {
    Azure.closeIncident($scope.incidentId)
      .then(function () {
        $scope.popover.hide();
        $ionicHistory.goBack();
      });
  };

  // add comment
  $scope.addComment = function() {

    $scope.popover.hide();

  };

});
