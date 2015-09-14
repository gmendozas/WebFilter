$('#campoUno').change(function (e) {
    if ($(this).val().trim() == "")
        $('#miPartialView').empty();
    else {
        $.ajax({
            url: '/DatePicker/MostrarPartialView',
            data : {tipo : $(this).val()},
            dataType: "html",
            success: function (data) {
                $('#miPartialView').empty();
                $('#miPartialView').html(data);
            }
        });
    }        
});