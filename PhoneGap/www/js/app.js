var app = angular.module('IncidentApp', ['ionic', 'ngCordova']);

app.run(function($ionicPlatform, $state, ClearNavigationHistory) {
  $ionicPlatform.ready(function() {

    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
    if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);

    }

    if (window.StatusBar) {
      // org.apache.cordova.statusbar required
      StatusBar.styleLightContent();
    }


    document.addEventListener("offline", onOffline, false);
    document.addEventListener("online", onOnline, false);

    function onOffline() {
        ClearNavigationHistory.clear();
        $state.go('offline');
    }

    function onOnline() {
        ClearNavigationHistory.clear();
        $state.go('login');
    }

  });
});

app.config(function($stateProvider, $urlRouterProvider) {

  // Ionic uses AngularUI Router which uses the concept of states
  // Learn more here: https://github.com/angular-ui/ui-router
  // Set up the various states which the app can be in.
  // Each state's controller can be found in controllers.js
  $stateProvider

  .state('login', {
    url: '/login',
    templateUrl: 'templates/login.html',
    controller: 'LoginController'
  })

  .state('dash', {
    url: '/dash',
    templateUrl: 'templates/dash.html',
    controller: 'DashController'
  })

  .state('incidents', {
    url: '/incidents/:userId/:userFullName',
    templateUrl: 'templates/incidents.html',
    controller: 'IncidentsController'
  })

  .state('incident-detail', {
    url: '/incident-detail/:incidentId',
    templateUrl: 'templates/incident-detail.html',
    controller: 'IncidentDetailController'
  })

  .state('add-incident', {
    url: '/add-incident',
    templateUrl: 'templates/add-incident.html',
    controller: 'AddIncidentController'
  })

  .state('offline', {
    url: '/offline',
    templateUrl: 'templates/offline.html',
    controller: 'OfflineController'
  })

  // if none of the above states are matched, use this as the fallback
  $urlRouterProvider.otherwise('/login');

});

function guid() {
  function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1);
  }
  return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
    s4() + '-' + s4() + s4() + s4();
}
