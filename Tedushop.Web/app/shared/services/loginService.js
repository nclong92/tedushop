(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData',
        function ($http, $q, authenticationService, authData) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).then(function successCallback(response) {
                    console.log('Thanh cong');
                    console.log(response);
                    userInfo = {
                        accessToken: response.data.access_token,
                        userName: userName
                    };
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;
                    deferred.resolve(null);
                }, function errorCallback(response) {
                    console.log('that bai');
                    console.log(response.data.error);
                    console.log(response);
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";
                    deferred.resolve(response);
                });
                return deferred.promise;
            }

            this.logOut = function () {
                console.log('logout start');
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                console.log('logout end');
            }
        }]);
})(angular.module('tedushop.common'));
