$(document).ready(function () {
    // Event listener for "Add to Cart" buttons
    $('.add-to-cart-btn').click(function (event) {
        event.preventDefault();

       
        const productId = $(this).attr('data-id');
        const productName = $(this).attr('data-name');
        const productPrice = $(this).attr('data-price');
        const productImageUrl = $(this).attr('data-url'); 
       
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                productId: productId,
                productName: productName,
                price: parseFloat(productPrice),
                imageUrl: productImageUrl 
            }),
            success: function (data) {
                if (data.success) {
                    alert(`"${productName}" added to cart! Total items: ${data.cartCount}`);
                    
                    $('#cart-count').text(`Cart: ${data.cartCount} items`);
                } else {
                    alert('Failed to add product to cart.');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
            }
        });
    });
});
