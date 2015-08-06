app.factory('ClearNavigationHistory', function($ionicHistory) {

    var clearNavigationHistoryService = {};

    clearNavigationHistoryService.clear = function () {

      //hack to not show login view in back nav stack
      //see issue here: https://github.com/driftyco/ionic/issues/1287
      $ionicHistory.currentView($ionicHistory.backView());

    };

    return clearNavigationHistoryService;

});
