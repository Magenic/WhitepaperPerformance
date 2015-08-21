app.factory('Audio', function($q) {

    var audioService = {};

    // Record Audio
    audioService.recordAudio = function () {

      var deferred = $q.defer();

      navigator.device.capture.captureAudio(function(mediaFiles) {

        deferred.resolve(mediaFiles[0].fullPath);

      }, function(errorMessage) {

        deferred.resolve(null);

      }, {
          limit: 1
      });

      return deferred.promise;

    };

    return audioService;

});
