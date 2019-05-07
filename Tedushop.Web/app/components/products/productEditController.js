(function (app) {

    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.product = {};

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.UpdateProduct = UpdateProduct;

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, $scope.product,
                function (result) {
                    $scope.product = result.data;
                }, function (error) {
                    notificationService.displayError(error.data);
                }
            );
        }

        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        }

        $scope.GetSeoTitile = GetSeoTitile;

        function GetSeoTitile() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.productCategories = [];

        function loadProductCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        loadProductCategory();
        loadProductDetail();
    }

})(angular.module('tedushop.products'));