
function check() {
    var div = document.getElementById("creditCardFieldsView");
    var creditRadioButton = document.getElementById("creditRadioButton");
    var input = document.getElementsByClassName("creditCardFields");

    if (creditRadioButton.checked) {
        for (var i = 0; i < input.length; i++) {
            input[i].required = true;
        }
        div.style.display = "block";
    }
    else {
        for (var i = 0; i < input.length; i++) {
            input[i].required = false;
        }
        div.style.display = "none";
    }

    
}

