/**
 * Displays stars in a given container based on a rating.
 * @param {HTMLElement} container - The container where the stars will be displayed.
 * @param {number} rating - The decimal rating (0 to 5).
 */
function renderStars(container, rating) {
  const starCount = 5; // Total number of stars
  container.innerHTML = ''; // Clear previous content

  for (let i = 0; i < starCount; i++) {
      const star = document.createElement('div');
      star.className = 'star';

      const filled = document.createElement('div');
      filled.className = 'filled';

      // Calculate how much of the star is filled
      const fillWidth = Math.max(0, Math.min(1, rating - i)) * 100;
      filled.style.width = `${fillWidth}%`;

      star.appendChild(filled);
      container.appendChild(star);
  }
}

// Find all containers with the `star-rating` class
document.querySelectorAll('.star-rating').forEach(container => {
  const rating = parseFloat(container.getAttribute('data-rating')) || 0; // Get the rating
  renderStars(container, rating); // Render stars for the rating
});
