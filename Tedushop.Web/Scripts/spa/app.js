/// <reference path="../../bower_components/angular/angular.js" />

var myApp = angular.module('myModule', []);

myApp.controller("schoolController", schoolController);
//myApp.controller("studentController", studentController);
//myApp.controller("teacherController", teacherController);
myApp.service('Validator', Validator);

schoolController.$inject = ['$scope', 'Validator'];

//myController.$inject = ['$scope'];

// declare
//function studentController($scope) {
//    $scope.message = "this is my message from student";
//}

//function teacherController($scope) {
//    $scope.message = "this is my message from teacher";
//}

function schoolController($scope, Validator) {
    //$scope.message = "Announcement from school.";
    $scope.checkNumber = function () {
        $scope.message = Validator.checkNumber($scope.num);
    }

    $scope.num = 1;
}

function Validator($window) {
    return {
        checkNumber: checkNumber
    }

    function checkNumber(input) {
        if (input % 2 == 0) {
            return 'This is even';
        } else {
            return 'This is odd';
        }
    }
}