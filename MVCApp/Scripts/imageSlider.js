function startImageSlider(ms, json) {
    /*
     * Parameters:
     * ms: Interval in milliseconds for the slider
     * json : Location of the JSON file which contains the locations of pictures to display in the slider
    */
    var xHttp = new XMLHttpRequest(); // Create a HttpRequest object - used to retrieve the JSON file
    xHttp.open("GET", json, true); // Add the header to request - using GET here, async
    xHttp.onreadystatechange = function () { // Create callback function to start the slider once the HTTPRequest has completed
        
        if (this.readyState == 4 && this.status == 200) { // state 4 = response complete, status code 200 = OK
            setImageTimer(JSON.parse(xHttp.responseText),ms); // Call create timer function, using JSON.Parse to convert the response into a javascript object
        }
    };
    xHttp.send(); // Send the GET Request to server
}



function setImageTimer(imagesJSON, ms) {
    /* This is called after successfully recieving the json file containing the images to scroll through
     * Parameters:
     * imagesJSON - JSON data needed to list which images will be scrolled in each element (so this isn't hardcoded and can be changed easily)
     * ms - time between images changing
    */
    var imgNo = 1;// Variable to hold the current image scroll number, declare in this scope so that each intervall callback doesn't reset the value of imgNo

    // Create anonymous function, set a timer to call based on parameter ms
    // This function takes no parameters and increases imgNo 1..3 and changes them
    setInterval(() => {
        var items = document.getElementsByClassName("content-img"); // Get all of the images to scroll 

        var itemsLength = items.length; // more efficient to access this once rather than each time in loop

        if (imgNo > 2) { // Check if i is 3, if so loop back round to first image (1)
            imgNo = 1
        } else {
            imgNo++; // If i isn't 3, increment it
        }
        for (let i = 0; i < itemsLength; i++) {
            items[i].src = imagesJSON[items[i].id][imgNo]; // Set the source of each image element to the 1st/2nd/3rd location in the json file, for its id number
        }

    }, ms); 
}