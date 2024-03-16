function compareTimes(time1, time2) {
    return new Date('2024-01-01 ' + time1) - new Date('2024-01-01 ' + time2);
}
function calculateTimeDifference(startTime, endTime) {
    var t1 = new Date('2024-01-01 ' + startTime);
    var t2 = new Date('2024-01-01 ' + endTime);
    var diff = (t2 - t1) / 1000 / 60; // Difference in minutes
    return diff;
}
function collectDoctorFormData() {

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

    var sessionLength = parseInt($('#appointmentSlotTime').val());
    var startTime = $('#dayStartTime').val();
    var endTime = $('#dayEndTime').val();

    if (startTime && endTime) {
        if (compareTimes(startTime, endTime) >= 0) {
            isValid = false;
            var errorMessage = $('<span class="error-message text-danger">End time should be greater than start time.</span>');
            $('#dayEndTime').after(errorMessage);
        }
        if (calculateTimeDifference(startTime, endTime) < sessionLength) {
            isValid = false;
            var errorMessage = $('<span class="error-message text-danger">The time between start and end should be greater than or equal to the session length.</span>');
            $('#dayEndTime').after(errorMessage);
        }        
    }  

    if (isValid) {
        var doctorInfo = {
            DoctorName: $('#userName').val(),
            DayStartTime: $('#dayStartTime').val(),
            DayEndTime: $('#dayEndTime').val(),
            AppointmentSlotTime: parseInt($('#appointmentSlotTime').val()), 
            Email: $('#email').val(),
            Password: $('#password').val()
        };
        if (parseInt($('#doctorId').val()) != 0) {
            doctorInfo.DoctorID = parseInt($('#doctorId').val());
            updateDoctorData(doctorInfo);
        } else {
            insertDoctorData(doctorInfo);
        }
    }
}
function insertDoctorData(doctorInfo) {
    $.ajax({
        url: '/DoctorRegistration/InsertDoctor',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(doctorInfo),
        dataType: 'json',
        success: function (data) {
            window.location.href = "/DoctorLogin/Index";
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
function updateDoctorData(doctorInfo) {
    $.ajax({
        url: '/DoctorRegistration/UpdateDoctor',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(doctorInfo),
        dataType: 'json',
        success: function (data) {
            if (data) {
                $('#successMessage').html("updated successfully").addClass("alert alert-info").show();
                $('#doctorRegistrationForm').hide();
            }else{
                $('#successMessage').html("something went wrong ").addClass("alert alert-info").show();
                $('#doctorRegistrationForm').hide();
            }
            $('#doctorId').val(0);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
function populateDoctorForm(doctorDetails) {

    $('#userName').val(doctorDetails.DoctorName);
    $('#email').val(doctorDetails.Email);
    $('#password').val(doctorDetails.Password);
    $('#appointmentSlotTime').val(doctorDetails.AppointmentSlotTime);
    $('#dayStartTime').val(formatTime(doctorDetails.DayStartTime));
    $('#dayEndTime').val(formatTime(doctorDetails.DayEndTime));

    $('#createDoctorAccountBtn').html("save");
    
}
// Function to format time as "HH:mm"
function formatTime(timeObj) {
    var hours = timeObj.Hours < 10 ? '0' + timeObj.Hours : timeObj.Hours;
    var minutes = timeObj.Minutes < 10 ? '0' + timeObj.Minutes : timeObj.Minutes;
    return hours + ':' + minutes;
}
function getDoctorDetails(doctorId) {
    $.ajax({
        url: '/DoctorRegistration/GetDoctorDetails',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({doctorId}),
        dataType: 'json',
        success: function (data) {
            populateDoctorForm(data);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}

$(document).ready(function () {
    $('#successMessage').hide();
    $('#doctorRegistrationForm').show();

    $('#doctorRegistrationForm').on('submit', function (event) {
        event.preventDefault();
        collectDoctorFormData();
    });

    if (parseInt($('#doctorId').val()) != 0) {
        $('#successMessage').html("your timings can't be updated as they effect previous appointments").addClass("text text-danger").show();

        getDoctorDetails(parseInt($('#doctorId').val()));
    }
    

});