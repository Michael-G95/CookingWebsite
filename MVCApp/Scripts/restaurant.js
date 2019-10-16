function previous() {
    var ID = parseInt(document.getElementById('RestaurantID').innerText);
    var xHttp = new XMLHttpRequest();
    xHttp.open("GET", "/Restaurant/GetRestaurant?ID="+(ID-1), true);
    xHttp.onreadystatechange = function () { // Create callback function to start the slider once the HTTPRequest has completed

        if (this.readyState === 4 && this.status === 200 && xHttp.responseText !== "\"N/A\""){ // state 4 = response complete, status code 200 = OK
            updatePage(JSON.parse(xHttp.responseText));
        } else if (this.readyState === 4){
            document.getElementById('WarningText').innerText = 'No more restaurants to show!';
        }
    };
    xHttp.send();
}

function next() {
    var ID = parseInt(document.getElementById('RestaurantID').innerText);
    var xHttp = new XMLHttpRequest();
    xHttp.open("GET", "/Restaurant/GetRestaurant?ID="+(ID+1), true);
    xHttp.onreadystatechange = function () { // Create callback function to start the slider once the HTTPRequest has completed
        if (this.readyState === 4 && this.status === 200 && xHttp.responseText!=="\"N/A\"") { // state 4 = response complete, status code 200 = OK
            updatePage(JSON.parse(xHttp.responseText));
        } else if (this.readyState === 4) {
            document.getElementById('WarningText').innerText = 'No more restaurants to show!';
        }
    };
    xHttp.send();
}

function updatePage(data) {
    
    document.getElementById('RestaurantDetails').innerHTML = data.RestaurantDetails;
    document.getElementById('RestaurantAddress').innerHTML = data.RestaurantAddress;
    document.getElementById('RestaurantName').innerText = data.RestaurantName;
    document.getElementById('RestaurantID').innerText = data.RestaurantID;
    document.getElementById('RestaurantUrl').href = data.RestaurantURL;
    document.getElementById('RestaurantImg').src = '/Content/images/Restaurant/' + data.RestaurantName + '.jpg'
    document.getElementById('RestaurantMap').src = data.MapsSRC    
    

    document.getElementById('WarningText').innerText = ""; // Clear the warning

}