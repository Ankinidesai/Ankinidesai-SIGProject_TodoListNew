$(document).ready(function () {
    $.ajax({
        url: '/TodoDetails/tabView',
        success: function (result) {
            $('#tabView').html(result);
        }
    });
});




//this js helps to load the document and ajax will sends areq to url and that url returns the partial view that 
//builds the table