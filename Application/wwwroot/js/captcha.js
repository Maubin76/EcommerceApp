
$(document).ready(function () {
    // Get the site key from the input's data-sitekey attribute
    var siteKey = document.getElementById("inputToken").getAttribute("data-sitekey");

    grecaptcha.ready(function () {
        grecaptcha.execute(siteKey, { action: 'submit' }).then(function (token) {
            console.log(token); // For debugging
            document.getElementById("inputToken").value = token; // Set the token to the hidden input
        });
    });
});
