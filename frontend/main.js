// Function to fetch and display page views from the API
async function fetchAndDisplayViewCount() {
    
    try {
         // Update with your live API URL if deployed
        const functionApiUrl = "https://getazureresumecounter.azurewebsites.net/api/getAzureResumeCounter?code=4uQIwKXyKqG3u7reG-6TBBzanRUt2xPFqXCzi19wrMCDAzFu3p7eIg==";
        const localFunctionApi = "http://localhost:7071/api/GetResumeCounter";
        // Make a GET request to the Azure Function API
        const response = await fetch(functionApiUrl);
    

        if (!response.ok) {
            throw new Error(`API error: ${response.statusText}`);
        }

        // Parse the JSON response

        const data = await response.json();
        console.log("API Response:", data.count)

        // Update the displayed counter with the value from Cosmos DB
        const count = data.Count || data.count || data.value;
        const viewDisplayElement = document.getElementById('viewCountDisplay');
        if (viewDisplayElement) {
            viewDisplayElement.textContent = `This page has been viewed ${data.count} times.`;
            
        }
    } catch (error) {
        console.error("Error fetching view count:", error);
        const viewDisplayElement = document.getElementById('viewCountDisplay');
        if (viewDisplayElement) {
            viewDisplayElement.textContent = "Unable to fetch view count.";
        }
    }
}

// Run the function when the page loads
document.addEventListener('DOMContentLoaded', fetchAndDisplayViewCount);



/*window.addEventListener('DOMContentLoaded', (event) => {
    getVisitCount();
})

const functionApi = "http://localhost:7071/api/GetResumeCounter";

const getVisitCount = () => {
    let count = 30;
    fetch(functionApi).then(response => {
        return response.json()
    }).then(response =>
        {
        console.log("Website called function API.");
        count = response.count;
        document.getElementById("counter").innerText = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}*/

//main.js

/*const functionApi = "http://localhost:7071/api/GetResumeCounter";
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
*/