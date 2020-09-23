// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
//currency formatter
const numberFormatter = new Intl.NumberFormat('en-Us', {
    style: 'currency',
    currency: 'USD',
});
// Write your JavaScript code.
$(document).ready(function () {
    console.log("ready")
    $('#dataTable').DataTable();

});

