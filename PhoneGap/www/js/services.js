app.factory('ClearNavigationHistory', function($ionicHistory) {

    var clearNavigationHistoryService = {};

    clearNavigationHistoryService.clear = function () {

      //hack to not show login view in back nav stack
      //see issue here: https://github.com/driftyco/ionic/issues/1287
      $ionicHistory.currentView($ionicHistory.backView());

    };

    return clearNavigationHistoryService;

});

app.factory('Azure', function($q) {

    var azureUrl = "https://testincidentqueue.azure-mobile.net/";
    var mobileService;
    var azureService = {};

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

    return azureService;

});




