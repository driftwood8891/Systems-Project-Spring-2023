// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/******************************************** 
 * functionhandleHeaderPopup handles the header popup when you click on your account name
 * function clearHeaderPopup clears the popup when you click somewhere in the body
*********************************************/
function handleHeaderPopup() {
    var popup = document.getElementsByClassName("headerPopup");

    setTimeout(function () {
        for (var i = 0; i < popup.length; i++) {
            if (popup[i].classList.contains("addHeaderPopup")) {
                popup[i].classList.add("removeHeaderPopup");
                popup[i].classList.remove("addHeaderPopup");
            } else {
                popup[i].classList.remove("removeHeaderPopup");
                popup[i].classList.add("addHeaderPopup");
            }
        }
    }, 25);
}

function clearHeaderPopup() {
    var popup = document.getElementsByClassName("headerPopup");

    for (var i = 0; i < popup.length; i++) {
        popup[i].classList.add("removeHeaderPopup");
        popup[i].classList.remove("addHeaderPopup");
    }
}
clearHeaderPopup();

// clear header when clicking anywhere else
const box = document.querySelector(".account-icon-container")
document.addEventListener("click", function (event) {
    if (event.target.closest(".account-icon-container")) { return }
    clearHeaderPopup();
})

