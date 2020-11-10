var productRows = document.getElementsByClassName("productRows")
var productCell = document.getElementsByClassName("productQty")
  
console.log(productCell[2].innerText)  
   
for (var i = 0; i < productCell.length; i++) {
    
    if (productCell[i].innerText == 0) {
        
        productRows[i].style.backgroundColor = "red";
    }

    else if (productCell[i].innerText >= 1 && productCell[i].innerText <= 5) {
        productRows[i].style.backgroundColor = "yellow";
    }
    else if (productCell[i].innerText > 5) {
        productRows[i].style.backgroundColor = "#F5F5DC";
    }
       
}

