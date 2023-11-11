// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function validateTask() {
    var taskName = document.getElementById("nameInput").value.trim();
    if (taskName == "") {
        alert('Please enter a valid name');
        return false;
    } 

    return true; 
}
