// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function optimiseForPrint() {
    document.querySelector(".hideforprint").classList.add('d-none');
    document.querySelector(".hideforprint2").classList.add('d-none');
    document.querySelector(".hideforprint3").classList.add('d-none');
    window.print();
    document.querySelector(".hideforprint").classList.remove('d-none');
    document.querySelector(".hideforprint2").classList.remove('d-none');
    document.querySelector(".hideforprint3").classList.remove('d-none');
}