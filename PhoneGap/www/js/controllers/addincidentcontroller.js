app.controller('AddIncidentController', function($scope, Azure, Camera, Audio, $ionicPopover) {

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
  $scope.getPictureFromCamera = function() {

    Camera.getPictureFromCamera().then(function (imageData) {

      $scope.imageSource = "data:image/jpeg;base64," + imageData;

    });

    $scope.popover.hide();

  };

  $scope.getPictureFromPhotoLibrary = function() {

    Camera.getPictureFromPhotoLibrary().then(function (imageData) {

      $scope.imageSource = "data:image/jpeg;base64," + imageData;

    });

    $scope.popover.hide();

  };

  $scope.recordAudio = function() {

    Audio.recordAudio().then(function (audioUrl) {

      $scope.audioSource = audioUrl;

    });

    $scope.popover.hide();

  };

  $scope.imageHasBeenAdded = function() {

    if ($scope.imageSource == null) {
      return false;
    }

    return true;

  };

});
