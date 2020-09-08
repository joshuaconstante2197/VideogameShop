
function check() {
    var div = document.getElementById("creditCardFieldsView");
    var creditRadioButton = document.getElementById("creditRadioButton");

    if (creditRadioButton.checked) {
        div.style.display = "block";
    }
    else {
        div.style.display = "none";
    }
}
