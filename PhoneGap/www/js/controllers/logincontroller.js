app.controller('LoginController', function(Azure, $state, ClearNavigationHistory, $scope) {

  $scope.logon = function() {

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

                  $state.go('incidents', { userId: userProfile.userId, userFullName: userProfile.fullName });

                }

              });

          } else {

            $state.go('/LoginFailed');

          }

      });

  };

});
