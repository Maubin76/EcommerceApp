// Add event listeners to all star labels for hover effects
document.querySelectorAll('.star-rating label').forEach(label => {
    label.addEventListener('mouseenter', function () {
        // Change the color of the stars when hovering over them
        this.style.color = '#ffdb3a'; // Highlight the current star
        let sibling = this.previousElementSibling;
        while (sibling) {
            sibling.style.color = '#ffdb3a'; // Highlight previous stars
            sibling = sibling.previousElementSibling;
        }
    });

    label.addEventListener('mouseleave', function () {
        // Reset the color of the stars when the mouse leaves
        document.querySelectorAll('.star-rating label').forEach(l => l.style.color = '#ddd'); // Default color
        const checked = document.querySelector('.star-rating input:checked'); // Find the selected star
        if (checked) {
            const id = checked.id.split('-')[1]; // Get the rating value from the selected star's ID
            for (let i = 1; i <= id; i++) {
                document.querySelector(`#star-${i} ~ label`).style.color = '#ffc107'; // Highlight selected stars
            }
        }
    });
});

// Handle form submission
document.querySelector('#reviewForm').addEventListener('submit', function(event) {
    event.preventDefault(); // Prevent default form submission

    // Get the selected rating and the comment text
    const selectedRating = document.querySelector('input[name="rating"]:checked');
    const comment = document.querySelector('textarea[name="comment"]').value;

    // Send the review data to the server via AJAX
    $.ajax({
        url: '/Review/AddReview', // Backend endpoint for adding a review
        type: 'POST',
        data: {
            rating: selectedRating.value,  // The selected rating value
            comment: comment,             // The comment text
            itemId: model.id              // The ID of the item being reviewed
        },
        success: function (response) {
            // If the review is added successfully, display a success message
            if (response.success) {
                alert("Item added with success");
            }
        },
        error: function () {
            // Show an error message if the review submission fails
            alert('An error occurred while adding the review');
        }
    });
});
