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

      if (mobileService.currentUser == null) {

            alert('you are not logged in');

      } else {

        mobileService.invokeApi("StatusList", {
            body: null,
            method: "get"
        }).done(function (results) {
            
            return results.result;

        }, function(error) {
            
            alert(error.message);

        });

      }

    };

    return azureService;

});