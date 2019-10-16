// 2 functions to handle onclick events for the search function
function searchName() {
    // Searching by name - pass in type 0 (search by name) and value from textbox
    var value = document.getElementById("SearchNameText").value;
    if (value.length > 0) {
        window.location.href = '/Catalog/SearchResult?type=0&value=' + value
    }
}

function searchCategory() {
    // Searching by category - pass in type 1 (search by category) and value from dropdown box
    var obj = document.getElementById("SearchCategory");
    var value = obj.value;
    if (value != "") {
        window.location.href = '/Catalog/SearchResult?type=1&value=' + value
    }
}