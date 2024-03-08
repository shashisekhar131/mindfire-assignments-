


function collectDoctorFormData() {
    var DoctorInfo = {};

    var isValid = true;
    // Remove all error messages elements added
    $('.error-message').remove();
    // Remove  bootstrap class for all input elemnts
    $('.is-invalid').removeClass('is-invalid');

    $('[data-input="user-input"]').each(function () {
        var element = $(this);

        if (element.val() === undefined || element.val() === null || element.val().trim() === '') {
            isValid = false;
            element.addClass('is-invalid');

            var errorMessage = $('<span class="error-message text-danger">Please fill in this field.</span>');
            element.after(errorMessage);
        }
    });

    if (isValid) {

        var doctorInfo = {
            DoctorName: $('#userName').val(),
            DayStartTime: $('#dayStartTime').val(),
            DayEndTime: $('#dayEndTime').val(),
            AppointmentSlotTime: parseInt($('#appointmentSlotTime').val()), 
            Email: $('#email').val(),
            Password: $('#password').val()
        };
        postFormData(doctorInfo);

    }
}


function postFormData(doctorInfo) {
    $.ajax({
        url: '/DoctorRegistration/InsertDoctor',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(doctorInfo),
        dataType: 'json',
        success: function (data) {

            alert("updated info");
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });

}


$(document).ready(function () {
    $('#doctorRegistrationForm').on('submit', function (event) {
        event.preventDefault();
        collectDoctorFormData();
    });

});