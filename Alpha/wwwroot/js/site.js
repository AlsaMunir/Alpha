$(document).ready(function () {
    // Event listener for "Add to Cart" buttons
    $('.add-to-cart-btn').click(function (event) {
        event.preventDefault(); // Prevent default link behavior

        // Get data attributes from the button (product ID, name, price, and image URL)
        const productId = $(this).attr('data-id');
        const productName = $(this).attr('data-name');
        const productPrice = $(this).attr('data-price');
        const productImageUrl = $(this).attr('data-url'); // This is the new part

        // Send product data to the backend using jQuery's AJAX method
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                productId: productId,
                productName: productName,
                price: parseFloat(productPrice),
                imageUrl: productImageUrl // Sending the image URL
            }),
            success: function (data) {
                if (data.success) {
                    alert(`"${productName}" added to cart! Total items: ${data.cartCount}`);
                    // Optionally, update the cart count in the header
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
