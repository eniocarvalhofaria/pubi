
$(document).ready(function () {



    $("#< %= btnResgatar.ClientID %>").click(function () {

        $.ajax({
            type: "POST",
            url: "Default.aspx/Exemplos", // url da pagina/nome do metodo
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{}", //parametros da função
            success: function (json) {
                var JSONObject = json.d;
                var html = '';
                for (var i = 0; i < JSONObject.length; i++) {
                    html = html + JSONObject[i].Valorbool + ' - ' + JSONObject[i].Valortxt + '';
                }

                $('#Ap').html(html).hide("slow").show("Slow");

            },

            error: function (XMLHttpRequest, textStatus, error) {

                alert(XMLHttpRequest.responseText);

            }

        });

        return false; //Prescindivel para que o ASP.NET __DoPostBack não seja executado

    });

});
        