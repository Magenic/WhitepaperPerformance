app.controller('AddIncidentController', function($scope, Azure, $ionicPopover) {

  // create incident model
  $scope.incident = {
    subject: "",
    description: "",
    assignTo: "",
    attachedPhoto: null,
    attachedAudio: null
  };

  // save button disabled state
  $scope.canSave = function() {

    // required subject and at least one image
    if ($scope.incident.subject === "") {
      return false;
    }

    return true;

  };

  // get workers list
  Azure.getWorkersList()
    .then(function (workersList) {

      $scope.workersList = workersList;
      $scope.incident.assignTo = workersList[0].userId;

    });

  // add popover template
  $ionicPopover.fromTemplateUrl('templates/add-incident-attachmenu.html', {
    scope: $scope
  }).then(function(popover) {
    $scope.popover = popover;
  });

  // take photo
  $scope.takePhoto = function() {
    alert('take photo');
    $scope.popover.hide();
  };

  // add image
  $scope.attachImage = function() {
    alert('attach image');
    $scope.popover.hide();
  };

  // add audio
  $scope.recordAudio = function() {
    alert('record audio');
    $scope.popover.hide();
  };

});
