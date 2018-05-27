/*
*
* Управляет эконкой загрузки страницы.
*
*/

$(document).ready(function () {

    let loader = $("#pageLoader");


    window.onclick = function (e) {
        if (e.target.localName === 'a' && e.target.attributes['href'].value.includes("#") === false) {
            loader.show();
        }
    };

    hideLoader();

    $(window).on('beforeunload', function () {
        hideLoader();
    });
});



function isVisible(el) {
    return el.is(":visible");
}

function hideLoader() {
    $("#pageLoader").hide();
}