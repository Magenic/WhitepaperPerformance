app.controller('LoginController', function(Azure, $state, ClearNavigationHistory) {

  Azure.login()
    .then(function (result) {

        ClearNavigationHistory.clear();

        if (result == true) {

          $state.go('dash');

        } else {

          $state.go('/LoginFailed');

        }

    });

});

app.controller('DashController', function($scope, Azure) {

});

app.controller('OfflineController', function($scope, Azure) {

});


// this is the one from the tabs stuff
app.controller('DashCtrl', function($scope) {

});

app.controller('ChatsCtrl', function($scope) {
  // With the new view caching in Ionic, Controllers are only called
  // when they are recreated or on app start, instead of every page change.
  // To listen for when this page is active (for example, to refresh data),
  // listen for the $ionicView.enter event:
  //
  //$scope.$on('$ionicView.enter', function(e) {
  //});

});

app.controller('ChatDetailCtrl', function($scope, $stateParams, Chats) {
  $scope.chat = Chats.get($stateParams.chatId);
});

app.controller('AccountCtrl', function($scope) {
  $scope.settings = {
    enableFriends: true
  };
});
