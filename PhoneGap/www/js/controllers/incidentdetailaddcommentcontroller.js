app.controller('IncidentDetailAddCommentController', function($scope, Azure, Camera, Audio, $ionicPopover, $state, $ionicLoading, $cordovaFile, $stateParams) {

  // get incident Id from params
  //var incidentId = $stateParams.incidentId;

  // create model
  $scope.comment = {
    incidentId: $stateParams.incidentId,
    detailText: "",
    attachedPhoto: null,
    attachedAudio: null
  };

  // $scope.incidentId = incidentId;
  // $scope.comment = '';
  // $scope.attachedPhoto = null;
  // $scope.attachedAudio = null;

  // save button disabled state - has to have at least something
  $scope.canSave = function() {
    if ($scope.comment.detailText == '' && $scope.comment.attachedPhoto == null && $scope.comment.attachedAudio == null) {
      return false;
    }
    return true;
  };

  // save comment
  $scope.saveComment = function() {

    // show 'saving...' activity indication
    $scope.show = function() {
        $ionicLoading.show({
          template: 'Saving...'
        });
      };

    // save comment
    Azure.addIncidentComment($scope.comment.incidentId, $scope.comment.detailText, $scope.comment.attachedPhoto, $scope.comment.attachedAudio)
    .then(function () {

        // hide activity indication
        $scope.hide = function(){
          $ionicLoading.hide();
        };

        // go to detail page for incident
        $state.go('incident-detail', {
          incidentId: $scope.comment.incidentId
        });

    });

  };

  // add popover template
  $ionicPopover.fromTemplateUrl('templates/add-incident-attachmenu.html', {
    scope: $scope
  }).then(function(popover) {
    $scope.popover = popover;
  });

  // take photo
  $scope.getPictureFromCamera = function() {

    Camera.getPictureFromCamera().then(function (imageData) {
      $scope.comment.attachedPhoto = "data:image/jpeg;base64," + imageData;
    });

    $scope.popover.hide();

  };

  $scope.getPictureFromPhotoLibrary = function() {

    Camera.getPictureFromPhotoLibrary().then(function (imageData) {
      $scope.comment.attachedPhoto = "data:image/jpeg;base64," + imageData;
    });

    $scope.popover.hide();

  };

  $scope.removeAttachedPhoto = function() {
    $scope.comment.attachedPhoto = null;
  };

  $scope.recordAudio = function() {

    Audio.recordAudio().then(function (audioUrl) {
      $scope.comment.attachedAudio = audioUrl;
    });

    $scope.popover.hide();

  };

  $scope.removeAttachedAudio = function() {
    $scope.comment.attachedAudio = null;
  };

});
