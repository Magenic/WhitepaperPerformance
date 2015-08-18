app.controller('AddIncidentController', function($scope, Azure, Camera, Audio, $ionicPopover, $state) {

  // create incident model
  $scope.incident = {
    subject: "",
    description: "",
    assignTo: "",
    attachedPhoto: null,
    attachedAudio: null
  };

  $scope.attachedPhotos = [];
  $scope.attachedAudioRecordings = [];

  // save button disabled state
  $scope.canSave = function() {

    // required subject and at least one image
    if ($scope.incident.subject === "" || $scope.attachedPhotos.length == 0) {
      return false;
    }

    return true;

  };

  // save incident
  $scope.saveIncident = function() {
    Azure.saveNewIncident($scope.attachedPhotos, $scope.attachedAudioRecordings, $scope.incident.subject, $scope.incident.assignTo, $scope.incident.description)
    .then(function (newIncident) {

        $state.go('incident-detail', {
          incidentId: newIncident.id
        });

    });
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
  $scope.getPictureFromCamera = function() {

    Camera.getPictureFromCamera().then(function (imageData) {

      $scope.attachedPhotos.push("data:image/jpeg;base64," + imageData);

    });

    $scope.popover.hide();

  };

  $scope.getPictureFromPhotoLibrary = function() {

    Camera.getPictureFromPhotoLibrary().then(function (imageData) {

      $scope.attachedPhotos.push("data:image/jpeg;base64," + imageData);

    });

    $scope.popover.hide();

  };

  $scope.removeAttachedPhotos = function(attachedPhoto) {
    $scope.attachedPhotos.splice($scope.attachedPhotos.indexOf(attachedPhoto), 1);
  };

  $scope.recordAudio = function() {

    Audio.recordAudio().then(function (audioUrl) {

      $scope.attachedAudioRecordings.push(audioUrl);

    });

    $scope.popover.hide();

  };

  $scope.removeAttachedAudio = function(attachedAudio) {
    $scope.attachedAudioRecordings.splice($scope.attachedAudioRecordings.indexOf(attachedAudio), 1);
  };

});
