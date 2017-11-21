//-Cart Index -- Allow to edit quantity of entries
$('.cart-item-enable').click(function () {
    $(this).closest('.input-table-row').find('.cart-item-quantity').prop("readonly", false);
    $(this).closest('.input-table-row').find('.cart-item-quantity').focus()
});