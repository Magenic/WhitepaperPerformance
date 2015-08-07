app.factory('Camera', function($q) {

    var cameraService = {};

    // Take Photo
    cameraService.getPicture = function () {

      var deferred = $q.defer();

      navigator.camera.getPicture(function(imageData) {

        deferred.resolve(imageData);

      }, function(errorMessage) {

        deferred.resolve(null);

      }, {
          quality: 50,
          destinationType: Camera.DestinationType.DATA_URL
      });

      return deferred.promise;

    };

    return cameraService;

});
