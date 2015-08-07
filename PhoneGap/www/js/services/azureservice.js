app.factory('Azure', function($q) {

    var azureUrl = "https://testincidentqueue.azure-mobile.net/";
    var mobileService;
    var azureService = {};

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

    return azureService;

});
