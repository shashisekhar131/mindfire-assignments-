function submitAppointment() {
    collectAppointmentFormData();
}



function collectAppointmentFormData() {


    var isValid = true;
    $('.error-message').remove();
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

    if (!$('#choosenSlot').data('choosen-slot-start-time')) {
        isValid = false;
    }


    if (isValid) {
        var slotTime = $('#choosenSlot').data('choosen-slot-start-time');

        var patientInfo = {
            AppointmentDate: slotTime,
            DoctorID: parseInt($('#doctorSelect').val()),
            PatientName: $('#patientName').val(),
            PatientEmail: $('#patientEmail').val(),
            PatientPhone: $('#patientPhone').val()
        };
        postFormData(patientInfo);
    }
}



function postFormData(patientInfo) {
    $.ajax({
        url: '/BookAppointment/InsertAppointment',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(patientInfo),
        dataType: 'json',
        success: function (data) {

            if (data) {
                $('#successMessage').text('Appointment successfully booked!').addClass('alert alert-info').show();
                $('#appointmentForm').hide();
            } else {
                $('#successMessage').text('Something went wrong try again').addClass('alert alert-info').show();
                $('#appointmentForm').hide();
            }
        
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });

}

function loadAllDoctors() {
    $.ajax({
        url: '/BookAppointment/GetAllDoctors',
        type: 'GET',
        success: function (data) {
            populateDoctorDropdown(data);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
function populateDoctorDropdown(doctors) {
    var doctorsDropdown = $('#doctorSelect');
    doctorsDropdown.empty();

    // Adding a disabled and selected option as a placeholder
    doctorsDropdown.append($('<option>', {
        value: "",
        text: "Select a doctor",
        disabled: true,
        selected: true
    }));
    $.each(doctors, function (index, doctor) {
        doctorsDropdown.append($('<option>', {
            value: doctor.DoctorID,
            text: doctor.DoctorName
        }));
    });
}

function getAvailableSlotsForDoctor(selectedDoctorId, selectedDate) {
    $('#selectedDate').html('<span>Slots for the date: ' + selectedDate + '</span>');
    $.ajax({
        url: '/BookAppointment/GetAvailableSlotsForDoctor',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ doctorId: parseInt(selectedDoctorId), date: selectedDate }),
        success: function (data) {
            renderSlots(data, selectedDate);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
}
function renderSlots(data, selectedDate) {
    const slotContainer = $('#slotContainer');
    slotContainer.empty();
    
    $('<div>', { class: '', text: 'Slot time for this doctor is ' + data.SlotTime + ' minutes' }).appendTo('#slotContainer');
    $('<div>', { class: '', text: 'Slots for the date: ' + selectedDate }).appendTo('#slotContainer');

    // Get total number of slots = total time / each slot time
    const totalSlots = Math.floor(
        (data.EndTime.Hours * 60 + data.EndTime.Minutes - data.StartTime.Hours * 60 - data.StartTime.Minutes) / data.SlotTime
    );


    for (let i = 0; i < totalSlots; i++) {
        // Create single slot
        const slotStartTime = new Date(selectedDate);
        slotStartTime.setHours(data.StartTime.Hours, data.StartTime.Minutes + i * data.SlotTime, 0, 0);

        const slotEndTime = new Date(slotStartTime);
        slotEndTime.setMinutes(slotStartTime.getMinutes() + data.SlotTime);

        // If booked styling accordingly
        const isBooked = data.BookedSlots.some(
            (bookedSlot) =>
                bookedSlot.Hours === slotStartTime.getHours() && bookedSlot.Minutes === slotStartTime.getMinutes()
        );

        const slotElement = $('<span>')
            .addClass('slot badge badge-pill mx-1')
            .text(`${formatTime(slotStartTime)} - ${formatTime(slotEndTime)}`)
            .data('slot-start-time', slotStartTime);

        if (isBooked) {
            slotElement.css('background-color', 'red');
        } else {
            slotElement.css('background-color', 'green');
        }

        slotElement.click(() => handleSlotSelection(slotElement));

        slotContainer.append(slotElement);
    }
}

// Function to format time as HH:mm
function formatTime(date) {
    return `${String(date.getHours()).padStart(2, '0')}:${String(date.getMinutes()).padStart(2, '0')}`;
}


function handleSlotSelection(selectedSlot) {

    $('#slotContainer').hide();

    const startDate = selectedSlot.data('slot-start-time');
    $('#choosenSlot').data('choosen-slot-start-time', startDate);
    const formattedDate = startDate.toLocaleDateString();
    const formattedTime = startDate.toLocaleTimeString();
    $('#choosenSlot').html(`You have chosen ${formattedDate} ${formattedTime}`).addClass("alert alert-info mt-3");
}


$(document).ready(function () {
    loadAllDoctors();
    // Set default as today's date
    $('#dateSelect').val(new Date().toISOString().split('T')[0]);
    $('#sucessMessage').hide();
    $('#doctorSelect').on('change', function () {
        var selectedDoctorId = $(this).val();
        var currentDate = $('#dateSelect').val();
        getAvailableSlotsForDoctor(selectedDoctorId, currentDate);
        $('#slotContainer').show();
    });

    $('#dateSelect').on('change', function () {
        var selectedDoctorId = parseInt($('#doctorSelect').val());
        var selectedDate = $(this).val();

        // Check if selectedDoctorId is a valid number before calling getAvailableSlotsForDoctor
        if (!isNaN(selectedDoctorId)) {
            getAvailableSlotsForDoctor(selectedDoctorId, selectedDate);
            $('#slotContainer').show();
        } else {        
            $('#doctorSelect').after($('<span>').text('Select doctor first').addClass('text-danger'));         
        }
    });

    $('#appointmentForm').on('submit', function (event) {
        event.preventDefault();
        submitAppointment();
    });
});