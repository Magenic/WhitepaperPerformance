app.factory('Azure', function($q, $http, $window, $cordovaFile) {

    var azureUrl = "https://testincidentqueue.azure-mobile.net/";
    var storageAccount = "https://testincidentqueue.blob.core.windows.net/incidentbinaries/";
    var storageAccountName = "/incidentbinaries";

    var mobileService;
    var azureService = {};

    //
    // Upload Audio to Azure
    //

    var uploadAudioFileFromArrayOfUrls = function(fileArray) {

      var deferred = $q.defer();

      if (fileArray == null || fileArray.length == 0) {
        deferred.resolve(null);
      } else {

        fileNameWithPath = fileArray[0];
        var posOfLastSlash = fileNameWithPath.lastIndexOf("/");

        var filePath = 'file://' + fileNameWithPath.substring(0, posOfLastSlash);
        var fileName = fileNameWithPath.substring(posOfLastSlash + 1);

        $cordovaFile.readAsBinaryString(filePath, fileName)
          .then(function (audioFile) {

            saveBlobToAzure(audioFile, 'wav', 'audio/x-wav').then(function (audioPath) {

              deferred.resolve(audioPath);

            }, function (error ) {
              alert("Error: " + err);
            });

          }, function (error) {
            alert("Error: " + err);
          });

      }

      return deferred.promise;

    };

    //
    // Upload Photo / Audio to Azure
    //

    var saveBlobToAzure = function (base64data, blobExtension, contentType) {

      var deferred = $q.defer();

      getSASUrl().then(function (sasURL) {

        // convert base64 data
        var byteArray;
        if (contentType == 'image/jpeg') {
          byteArray = convertBase64toBlob(base64data);
        } else {
          byteArray = base64data;
        }

        // generate blob name and SAS url
        var blobName = guid() + "." + blobExtension;
        var storageAccountNameWithBlobName = storageAccountName + "/" + blobName;
        var sasUrlWithBlobName = sasURL.replace(storageAccountName, storageAccountNameWithBlobName);

        // put file
        $http.put(sasUrlWithBlobName, byteArray, {
          headers: {
            'x-ms-blob-type': 'BlockBlob',
            'x-ms-blob-content-type': contentType
          }
        }).then(function (response) {

          // success
          deferred.resolve(storageAccount + blobName);

        }, function (error) {

          alert.message(error);

        });;

      });

      return deferred.promise;

    };

    // Get SAS URL
    var getSASUrl = function () {

      var deferred = $q.defer();

      var options = {
          body: null,
          method: "get"
      };

      var onSuccess = function (results) {
          deferred.resolve(results.result);
      };

      var onFailure = function(error) {
          alert(error.message);
      };

      mobileService.invokeApi("SASGenerator", options).done(onSuccess, onFailure);

      return deferred.promise;

    };

    // Convert Base64 To blob
    function convertBase64toBlob(base64data) {
        var binary = atob(base64data.split(',')[1]);
        var array = [];
        for(var i = 0; i < binary.length; i++) {
            array.push(binary.charCodeAt(i));
        }
        var z = [new Uint8Array(array)];
        return new Blob(z, {type: 'image/png'});
    }

    // Login
    azureService.login = function () {

      var deferred = $q.defer();

      mobileService = new WindowsAzure.MobileServiceClient(azureUrl, azureKey);
      mobileService.login("WindowsAzureActiveDirectory").done(function (results) {

        // var userId = results.userId;
        // var token = mobileService.currentUser.mobileServiceAuthenticationToken;

        deferred.resolve(true);

      })

      return deferred.promise;

    };

    // Get Status List
    azureService.getStatusList = function () {

      var deferred = $q.defer();

      if (mobileService.currentUser == null) {

            alert('you are not logged in');
            deferred.resolve(null);

      } else {

        mobileService.invokeApi("StatusList", {
            body: null,
            method: "get"
        }).done(function (results) {

            var statusList = [];

            results.result.forEach(function(result) {

              var status = {
                numberOfIncidents: Math.floor(Math.random() * 100),
                averageWaitTime: Math.floor(Math.random() * 100),
                user: result.user
              };

              statusList.push(status);

            });

            //deferred.resolve(results.result);

            deferred.resolve(statusList);

        }, function(error) {

            alert(error.message);

        });

      }

      return deferred.promise;

    };

    // Get User Profile
    azureService.getUserProfile = function () {

      var deferred = $q.defer();

      mobileService.invokeApi("Profile", {
          body: null,
          method: "get"
      }).done(function (results) {

          deferred.resolve(results.result);

      }, function(error) {

          alert(error.message);

      });

      return deferred.promise;

    };

    // Get Incident List
    azureService.getIncidentList = function (assignedToUserId) {

      var deferred = $q.defer();

      var incidentTable = mobileService.getTable('Incident')
        .where({

            assignedToId: assignedToUserId

          })
        .read().done(function (incidentTable) {

            deferred.resolve(incidentTable);

        }, function (err) {

            alert("Error: " + err);

        });

      return deferred.promise;

    };

    // Get Single Incident
    azureService.getIncident = function (incidentId) {

      var deferred = $q.defer();

      var incidentTable = mobileService.getTable('Incident')
        .where({

            Id: incidentId

          })
        .read().done(function (incidentTable) {

            deferred.resolve(incidentTable[0]);

        }, function (err) {

            alert("Error: " + err);

        });

      return deferred.promise;

    };

    // Get Single Incident
    azureService.getIncidentDetails = function (incidentId) {

      var deferred = $q.defer();

      var incidentTable = mobileService.getTable('IncidentDetail')
        .where({

            IncidentId: incidentId

          })
        .read().done(function (incidentDetailTable) {

            deferred.resolve(incidentDetailTable);

        }, function (err) {

            alert("Error: " + err);

        });

      return deferred.promise;

    };

    // Get User Profile
    azureService.getWorkersList = function () {

      var deferred = $q.defer();

      mobileService.invokeApi("WorkerList", {
          body: null,
          method: "get"
      }).done(function (results) {

          deferred.resolve(results.result);

      }, function(error) {

          alert(error.message);

      });

      return deferred.promise;

    };

    // Get Worker Name
    azureService.getWorker = function (workerId) {

      var deferred = $q.defer();

      mobileService.invokeApi("WorkerList", {
          body: null,
          method: "get"
      }).done(function (results) {

          deferred.resolve(results.result.filter(function (el) { return el.userId == workerId; }));

      }, function(error) {

          alert(error.message);

      });

      return deferred.promise;

    };

    // Save New Incident
    azureService.saveNewIncident = function (attachedPhotos, attachedAudioRecordings, subject, assignedToId, description) {

      var deferred = $q.defer();

      // save photo, get photo url
      saveBlobToAzure(attachedPhotos[0], 'png', 'image/jpeg').then(function (imagePath) {

        // save audio, get audio url
        uploadAudioFileFromArrayOfUrls(attachedAudioRecordings).then(function (audioPath) {

          // save incident
          mobileService.getTable('Incident').insert({
             Subject: subject,
             AssignedToId: assignedToId,
             Description: description,
             ImageLink: imagePath,
             AudioLink: audioPath,
             Closed: false,
             DateOpened: new Date(0),
             DateClosed: new Date(0)
          }).done(function (result) {

             deferred.resolve(result);

          }, function (err) {

             alert("Error: " + err);

          });

        });

      });

      return deferred.promise;

    };

    return azureService;

});
