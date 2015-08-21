app.controller('IncidentDetailController', function($scope, Azure, $stateParams, $timeout, $cordovaMedia) {

  // get incident
  var incidentId = $stateParams.incidentId;
  $scope.incidentId = incidentId;

  // do initial load of incidents
  refreshIncident();

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

  $scope.doRefresh = function() {
    refreshIncident();
  };

  $scope.playAudio = function(audioUrl) {

    new Audio(audioUrl).play();

    // $cordovaMedia.newMedia(audioUrl).play();
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

});
