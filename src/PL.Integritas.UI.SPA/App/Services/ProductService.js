/// <reference path="../angular.js" />

var ProductService = angular.module('ProductService', []);

ProductService.factory('ProductApi', function ($http) {

    var urlBase = "http://localhost:3998/api";
    var ProductApi = {};

    ProductApi.getProducts = function () {
        return $http.get(urlBase + '/Product');
    };

    return ProductApi;
});