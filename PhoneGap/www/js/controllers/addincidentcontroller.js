app.controller('AddIncidentController', function($scope, Azure, Camera, Audio, $ionicPopover, $state, $ionicLoading, $cordovaFile, $cordovaMedia) {

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
    if ($scope.incident.subject === "" || $scope.incident.attachedPhoto == null) {
      return false;
    }

    return true;

  };

  // save incident
  $scope.saveIncident = function() {

    // show 'saving...' activity indication
      $ionicLoading.show({
        template: 'Saving...'
      });

    // save incident
    Azure.saveNewIncident($scope.incident.attachedPhoto, $scope.incident.attachedAudio, $scope.incident.subject, $scope.incident.assignTo, $scope.incident.description)
    .then(function (newIncident) {

        // hide activity indication
        $ionicLoading.hide();

        // go to detail page for new incident
        $state.go('incident-detail', {
          incidentId: newIncident.id
        });

    });

  };

  // play audio
  $scope.playMedia = function() {
    var src = $scope.incident.attachedAudio;
    var media = $cordovaMedia.newMedia(src);
    media.play();
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

      $scope.incident.attachedPhoto = "data:image/jpeg;base64," + imageData;

    });

    $scope.popover.hide();

  };

  $scope.getPictureFromPhotoLibrary = function() {

    Camera.getPictureFromPhotoLibrary().then(function (imageData) {

      $scope.incident.attachedPhoto = "data:image/jpeg;base64," + imageData;

    });

    $scope.popover.hide();

  };

  $scope.removeAttachedPhoto = function() {
    $scope.incident.attachedPhoto = null;
  };

  $scope.recordAudio = function() {

    Audio.recordAudio().then(function (audioUrl) {

      $scope.incident.attachedAudio = audioUrl;

    });

    $scope.popover.hide();

  };

  $scope.removeAttachedAudio = function() {
    $scope.incident.attachedAudio = null;
  };

});
