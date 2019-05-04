(function (app) {

    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        function loadProductCategoryDetail() {
            apiService.get('api/productcategory/' + $stateParams.id,
                function (result) {
                    $scope.productCategory = result.data;
                }, function (error) {
                    notificationService.displayError(error.data);
                }
            );
        }

        $scope.UpdateProductCategory = UpdateProductCategory;

        function UpdateProductCategory() {
            apiService.put('api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        }

        $scope.parentCategories = [];

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentCategory();
        loadProductCategoryDetail();
    }

})(angular.module('tedushop.product_categories'));