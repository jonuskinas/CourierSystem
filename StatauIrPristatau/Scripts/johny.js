function dataChange() {
    let label = document.getElementById('bank_label');
    let bankData = document.getElementById("bank_data");
    if (bankData.value == "000000") {
        label.innerHTML = 'Banko duomenys teisingi!';
        label.style.color = "#7FFF00";
        label.style.visibility;
    }
    else {
        label.innerHTML = 'Banko duomenys neteisingi!';
        label.style.color = "#FF0000";
        label.style.visibility;
    }
}
function payClick(ordId) {
    let bankData = document.getElementById("bank_data");
    let label = document.getElementById('bank_label');
    let successLabel = document.getElementById('success_mess');
    if (bankData.value == "000000") {
        $.ajax({
            type: "GET",
            url: "/Customer/updateOrder",
            data: { id: ordId, value: "1" },
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: successFunc,
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
        function successFunc(data) {
            if (data) {
                successLabel.innerHTML = 'Apmokėjimas atliktas!';
                successLabel.style.color = "#7FFF00";
                successLabel.style.visibility;
                document.getElementById('close_modal').click();
 
            }
        }

    }
    else {
        $.ajax({
            type: "GET",
            url: "/Customer/updateOrder",
            data: { id: ordId, value: "2" },
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: successFunc,
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
        function successFunc(data) {
            if (data) {
                label.innerHTML = 'Apmokėjimas neatliktas, jūsų užsakymas pridėtas prie neapmokėtų užsakymų!';
                label.style.color = "#FF0000";
                label.style.visibility;
            }
        }
    }
}