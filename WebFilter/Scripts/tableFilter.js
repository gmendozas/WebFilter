/*
// New selector
jQuery.expr[':'].Contains = function (a, i, m) {
    return jQuery(a).text().toUpperCase()
        .indexOf(m[3].toUpperCase()) >= 0;
};

// Overwrites old selector
jQuery.expr[':'].contains = function (a, i, m) {
    return jQuery(a).text().toUpperCase()
        .indexOf(m[3].toUpperCase()) >= 0;
};

$("#searchInput").keyup(function () {
    var rows = $(".table tbody").find("tr").hide();
    
    if (this.value.length) {
        var data = this.value.split(" ");
        $.each(data, function (i, v) {
            rows.filter(":contains('" + v + "')").show();
        });
    } else rows.show();
});
*/

$('#searchButton').click(function (e) {    
    var url = '@Url.Action("Table\\Filter")';
    $.get(url, { filter: $("#searchInput").val() }, function (result) {
        debugger;
        $('#GatitosViewGrid').html(result);
    });
});