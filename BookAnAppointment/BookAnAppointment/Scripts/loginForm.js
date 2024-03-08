
function checkIfDoctorExists() {
    var email = $('#email').val();
    var password = $('#password').val();
    $.ajax({
        url: '/DoctorLogin/CheckIfDoctorExists',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({UserEmail: email,UserPassword: password }), 
        dataType: 'json',
        success: function (data) {
            var doctorId = data;
            if (doctorId != -1) window.location.href = "/DoctorAppointments/Index/" + doctorId;
            else $('#email').val("your a not valid doctor");
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
$(document).ready(function () {  
    $('#loginForm').on('submit', function (event) {
        event.preventDefault();
        checkIfDoctorExists();
    });
});