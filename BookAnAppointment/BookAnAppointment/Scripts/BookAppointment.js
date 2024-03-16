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
            var errorMessage = $('<span class="error-message text-danger">Please fill in this field.</span>');
            element.addClass('is-invalid').after(errorMessage);
        }
        if (element.prop('type') === "tel") {
            var phoneRegex = /^[0-9]{10}$/;
            if (!phoneRegex.test(element.val())) {
                isValid = false;
                var errorMessage = $('<span class="error-message text-danger"> phone number is invalid.</span>');
                element.addClass('is-invalid').after(errorMessage);
            } 
        }
        if (element.prop('type') === "date") {
            var currentDate = new Date();
            // Set hours to 0 to compare only the date part
            currentDate.setHours(0, 0, 0, 0); 
            var selectedDate = new Date(element.val()); 
            if (selectedDate < currentDate) {
                isValid = false;
                var errorMessage = $('<span class="error-message text-danger">Please select a date that is not in the past.</span>');
                element.addClass('is-invalid').after(errorMessage);
            }
        }
    });

    if (!$('#choosenSlot').data('choosen-slot-start-time')) {
        isValid = false;
        $('#choosenSlot').html(`Choose some slot`).addClass("alert alert-info mt-3");
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
    
    $('<div>', { class: 'alert alert-primary mt-3', text: 'Slot time for this doctor is ' + data.SlotTime + ' minutes' }).appendTo('#slotContainer');
    $('<div>', { class: 'alert alert-info', text: 'Slots for the date: ' + selectedDate }).appendTo('#slotContainer');

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
            .addClass('slot badge mx-1 my-2')
            .text(`${formatTime(slotStartTime)} - ${formatTime(slotEndTime)}`)
            .data('slot-start-time', slotStartTime)
            .css('cursor', 'pointer')
            .css('font-size', '1.25rem'); 

        if (isBooked) {
            slotElement.css('background-color', 'red').click(() => $('#choosenSlot').html(`This slot is already booked`).addClass("alert alert-info mt-3") );
        } else {
            slotElement.css('background-color', 'green');
            slotElement.click(() => handleSlotSelection(slotElement));
        }
        slotContainer.append(slotElement);
    }
}

// Function to format time as HH:mm
function formatTime(date) {
    return `${String(date.getHours()).padStart(2, '0')}:${String(date.getMinutes()).padStart(2, '0')}`;
}
function handleSlotSelection(selectedSlot) {
        
    $('.slot').removeClass('bg-primary text-white');
    selectedSlot.addClass('bg-primary text-white');

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
        $('#slotModal').modal('show'); // Open the modal

    });

    $('#dateSelect').on('change', function () {
        var selectedDoctorId = parseInt($('#doctorSelect').val());
        var selectedDate = $(this).val();

        // Check if selectedDoctorId is a valid number before calling getAvailableSlotsForDoctor
        if (!isNaN(selectedDoctorId)) {
            getAvailableSlotsForDoctor(selectedDoctorId, selectedDate);
            $('#slotModal').modal('show'); // Open the modal
        } else {        
            $('#doctorSelect').after($('<span>').text('Select doctor first').addClass('text-danger'));         
        }
    });

    $('#appointmentForm').on('submit', function (event) {
        event.preventDefault();
        submitAppointment();
    });

    $('#closeModalButton').on('click', function () {
        $('#slotModal').modal('hide');
    });
});