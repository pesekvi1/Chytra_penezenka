$(document).ready(function () {
    loaddatepicker();
});

$(document).ajaxComplete(function () {
    loaddatepicker();
});

function loaddatepicker() {

    //Only if it's a non touch device.
    if (!Modernizr.touch) {
        if (Modernizr.inputtypes.date) {

            //Disable default html5 datepicker
            $('input[type="date"]').on("click", function (event) {
                event.preventDefault();
            });

            //Enable jquery-ui date time picker
            $('input[type="date"]').datepicker({
                dateFormat: "yy-mm-dd"
            });

        } else {
            if (!Modernizr.inputtypes.date) {
                //This happens if device is touch and 
                //does not support html5 date time
                $('input[type="date"]').datepicker({
                    dateFormat: "yy-mm-dd"
                });
            }
        }
    }
}