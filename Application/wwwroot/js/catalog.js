$(document).ready(function () {       
    $('.add-to-cart-form').submit(function (event) {
        event.preventDefault(); // Prevent the default behavior (page reload)

        var form = $(this);
        var formData = form.serialize();

        if (isAuthenticated) {
            // Send AJAX request if the user is signed in
            $.ajax({
                url: '/Products/AddToCart', // Controller action
                type: 'POST',
                data: formData,
                success: function (response) {
                    // If the item is added to the cart, display the popup
                    if (response.success) {
                        var myModal = new bootstrap.Modal(document.getElementById('cartModalLogged'), {});
                        myModal.show();
                    }
                },
                error: function () {
                    alert('An error occurred while adding to the cart.');
                }
            });
        } else { 
            // If the user is not authenticated, show the login popup
            var myModal = new bootstrap.Modal(document.getElementById('cartModalNotLogged'), {});
            myModal.show();
        }
    });
});
