app.factory('Camera', function($q) {

    var cameraService = {};

    // Get Or Take Photo (internal)
    var getOrTakePicture = function (sourceIsCamera) {

      var deferred = $q.defer();

      var optionsSourceType;
      if (sourceIsCamera == true) {
        optionsSourceType = Camera.PictureSourceType.CAMERA;
      } else {
        optionsSourceType = Camera.PictureSourceType.PHOTOLIBRARY;
      }

      navigator.camera.getPicture(function(imageData) {

        deferred.resolve(imageData);

      }, function(errorMessage) {

        deferred.resolve(null);

      }, {
          quality: 50,
          destinationType: Camera.DestinationType.DATA_URL,
          sourceType : optionsSourceType
      });

      return deferred.promise;

    };

    // Get Picture From Camera
    cameraService.getPictureFromCamera = function () {
      return getOrTakePicture(true);
    };

    // Get Picture From Photo Library
    cameraService.getPictureFromPhotoLibrary = function () {
      return getOrTakePicture(false);
    };

    return cameraService;

});
