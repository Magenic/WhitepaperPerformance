app.controller('LoginController', function(Azure, $state, ClearNavigationHistory, $scope) {

  $scope.$on('$ionicView.enter', function(e) {

    Azure.login()
      .then(function (result) {

          ClearNavigationHistory.clear();

          if (result == true) {

            Azure.getUserProfile()
              .then(function (userProfile) {

                var isManager = userProfile.manager;
                if (isManager) {

                  $state.go('dash');

                } else {

                  $state.go('incidents');

                }

              });

          } else {

            $state.go('/LoginFailed');

          }

      });

  });

});
