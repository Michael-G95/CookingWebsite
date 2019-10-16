function recalcPortions(){
    ingredientAmounts = document.getElementsByClassName('ingredientQuantity');
    oldPortions = parseFloat(document.getElementById('RecipePortions').innerHTML)
    newPortions = parseFloat(document.getElementById('PortionsInput').value);
    multiplier = newPortions / oldPortions;

    for (i = 0; i < ingredientAmounts.length; i++) {
        ingredientAmounts[i].innerHTML = parseFloat(ingredientAmounts[i].innerHTML.trim()) * multiplier;
    }
    document.getElementById('RecipePortions').innerHTML = newPortions;
    
}