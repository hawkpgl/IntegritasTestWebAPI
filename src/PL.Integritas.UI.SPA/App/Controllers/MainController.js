/// <reference path="../angular.js" />
/// <reference path="../IntegritasApp.js" />

IntegritasApp.controller("MainController", function ($scope) {

    //Since there is no authentication, the shopping cart number is reset on application refresh.
    $scope.cartNumber = Math.floor(Math.random() * (10000 - 1 + 1)) + 1;

    $scope.date = new Date();
});