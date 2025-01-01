// main.js

// Function to track and display page views
function trackPageViews() {
    // Retrieve the current view count from localStorage
    let viewCount = localStorage.getItem('pageViewCount');

    // Initialize the view count if not already set
    if (!viewCount) {
        viewCount = 0;
    } else {
        viewCount = parseInt(viewCount, 10); // Convert to an integer
    }

    // Increment the view count
    viewCount++;

    // Update the view count in localStorage
    localStorage.setItem('pageViewCount', viewCount);

    // Display the view count on the webpage
    const viewDisplayElement = document.getElementById('viewCountDisplay');
    if (viewDisplayElement) {
        viewDisplayElement.textContent = `You have visited this page ${viewCount} times.`;
    }
}

// Run the function to track views when the page loads
document.addEventListener('DOMContentLoaded', trackPageViews);
