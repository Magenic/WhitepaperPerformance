angular.module('starter.controllers', [])

.controller('DashCtrl', function($scope) {

})

.controller('ChatsCtrl', function($scope) {
  // With the new view caching in Ionic, Controllers are only called
  // when they are recreated or on app start, instead of every page change.
  // To listen for when this page is active (for example, to refresh data),
  // listen for the $ionicView.enter event:
  //
  //$scope.$on('$ionicView.enter', function(e) {
  //});

  var azureUrl = "https://testincidentqueue.azure-mobile.net/";
  var mobileService = new WindowsAzure.MobileServiceClient(azureUrl, azureKey);

  mobileService.login("WindowsAzureActiveDirectory").done(function (results) {
    
    var userId = results.userId;    
    var token = mobileService.currentUser.mobileServiceAuthenticationToken;

    mobileService.invokeApi("StatusList", {
        body: null,
        method: "get"
    }).done(function (results) {
        
        $scope.users = results.result;

    }, function(error) {
        
        alert(error.message);

    });
  })

})

.controller('ChatDetailCtrl', function($scope, $stateParams, Chats) {
  $scope.chat = Chats.get($stateParams.chatId);
})

.controller('AccountCtrl', function($scope) {
  $scope.settings = {
    enableFriends: true
  };
});
