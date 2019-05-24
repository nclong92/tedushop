var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        //$('#btnAddToCart').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    var productId = parseInt($(this).data('id'));
        //    cart.addItem(productId);
        //});

        $('#frmPayment').validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: "Bạn phải nhập tên.",
                address: "Bạn phải nhập địa chỉ.",
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email không chính xác"
                },
                phone: {
                    required: "Bạn cần nhập số điện thoại",
                    number: "Số điện thoại phải là số"
                }
            }
        });

        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });

        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));

            if (isNaN(quantity) === false) {
                var amount = quantity * price;

                $('#amount-' + productid).text(numeral(amount).format('0,0'));
            } else {
                $('#amount-' + productid).text(0);
            }

            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));

            cart.updateAll();

        });

        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = '/';
        });

        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });

        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            //window.location.href = '/thanh-toan';
            $('#divCheckout').show();
        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });

        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();

            var isValid = $('#frmPayment').valid();

            if (isValid) {
                cart.createOrder();
            }
        });

    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;

                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },

    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;

        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });

        return total;
    },
    addItem: function (productId) {
        //$.ajax({
        //    url: '/ShoppingCart/Add',
        //    data: {
        //        productId: productId
        //    },
        //    type: 'POST',
        //    dataType: 'json',
        //    success: function (response) {
        //        if (response.status) {
        //            alert('Thêm sản phẩm thành công.');
        //        }
        //    }
        //});
    },
    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtMobile').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: 'Thanh toán tiền mặt',
            Status: false
        };

        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    $('#divCheckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('#cartContent').html('Đã đặt hàng thành công. Chúng tôi sẽ liên hệ bạn sớm nhất.');
                    }, 2000);

                }
            }
        });
    },

    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },
    updateAll: function () {
        var cartList = [];

        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });

        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    console.log('Updated');
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        })
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';

                    var data = res.data;

                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceFormat: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            //Amount: item.Quantity * item.Product.Price,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')

                        });
                    });

                    $('#cartBody').html(html);

                    if (html === '') {
                        $('#cartContent').html('Không có sản phẩm nào trong giỏ hàng.');
                    }

                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        })
    }
}

cart.init();