// The azure maps key
const subscriptionKey = 'Your_Azure_Maps_Subscription_Key';

// The HTML for the user's marker
const userMarkerHtml = '<div class="user-marker">You are here</div>';

// Create an instance of the Azure Maps client
const map = new atlas.service.Map('map', {
    'subscription-key': subscriptionKey,
    center: userLocation, // Center the map on the user's location
    zoom: 10
});

// Get the user's location
navigator.geolocation.getCurrentPosition(function(position) {

    const userLocation = new atlas.data.Position(position.coords.longitude, position.coords.latitude);
    
    // Create the marker for the user's location
    const userMarker = new atlas.HtmlMarker({
        htmlContent: userMarkerHtml,
        position: userLocation
    });

    // Add the marker to the map
    map.markers.add(userMarker);

    // Create a directions request
    const directionsRequest = {
        query: [userLocation, destination],
        outputSpat: 'polyline'
    };

    // Calculate the route to the marker
    map.directions.calculateRoute(directionsRequest).then(function(response) {
        const data = response.geojson();

        // Add the route to the map
        map.addSource('route', data);
        map.addLayer({
            id: 'route',
            type: 'line',
            source: 'route'
        });

        // Get the directions to the marker
        const directions = response.routes[0].legs[0];
        
        // Display the directions on the map
        document.getElementById('directions').innerHTML = directions;
    });
});