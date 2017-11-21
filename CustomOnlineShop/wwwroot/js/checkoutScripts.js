
// Set input in form based on radio button selection for shipping info

        $(document).ready(function () {
            $('input:radio[name=shippingInfo]').change(function () {
                $("#SelectedShippingInfo").val(this.value);
            });
        });


// Set input in form based on radio button selection for payment method

        $(document).ready(function () {
            $('input:radio[name=paymentMethod]').change(function () {
                $("#SelectedPaymentMethod").val(this.value);
            });
        });


// Choose option 

        $(function () {
            $('div.product-chooser').not('.disabled').find('div.product-chooser-item').on('click', function () {
                $(this).parent().parent().find('div.product-chooser-item').removeClass('selected');
                $(this).addClass('selected');
                $(this).find('input[type="radio"]').prop("checked", true);
                $(this).find('input[type="radio"]').change();
            });
        });
