$(document).ready(function () {
    $('#appointmentDateInput').val(new Date().toISOString().split('T')[0]);
    $('#selectedDateLabel').text('Appointments for the date: ' + $('#appointmentDateInput').val());

    var doctorAppointmentsTable = $('#doctorAppointmentsTable').DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        ajax: {
            type: "Post",
            url: '/DoctorAppointments/GetAppointmentsForDoctor',
            dataType: "json",
            data: function (d) {
                // Add additional parameters to the data object
                d.doctorId = parseInt($("#doctorId").val());
                d.selectedDate = $('#appointmentDateInput').val();
            }
        },
        columns: [
            { "data": "AppointmentID", "name": "AppointmentID", "autowidth": true },
            {
                "data": function (row) {
                    // Assuming "AppointmentTime" is a Date object
                    return new Date(0, 0, 0, row.AppointmentTime.Hours, row.AppointmentTime.Minutes, row.AppointmentTime.Seconds).toLocaleTimeString();
                },
                "name": "AppointmentTime",
                "autowidth": true
            },
            { "data": "PatientName", "name": "PatientName", "autowidth": true },
            { "data": "PatientEmail", "name": "PatientEmail", "autowidth": true },
            { "data": "PatientPhone", "name": "PatientPhone", "autowidth": true },
            { "data": "AppointmentStatus", "name": "AppointmentStatus", "autowidth": true },
            {
                "data": null,
                "render": function (data, type, row) {
                    return '<a href="#" class="btn btn-primary cancel-btn" data-id="' + row.AppointmentID + '">Cancel</a>';
                },
                "name": "Edit",
                "autowidth": true
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return '<a href="#" class="btn btn-primary close-btn" data-id="' + row.AppointmentID + '">Close</a>';
                },
                "name": "Edit",
                "autowidth": true
            }

        ], columnDefs: [
            {
                targets: [0],
                searchable: false,
                type: "num"
            }
        ]
    });

    $('#appointmentDateInput').on('change', function () {
        $('#selectedDateLabel').text('Appointments for the date: ' + $('#appointmentDateInput').val());
        // Update the selectedDate parameter in the DataTable's AJAX data
         doctorAppointmentsTable.ajax.reload();
    });

    // Event handler for cancel button click
    $('#doctorAppointmentsTable').on('click', '.cancel-btn', function (e) {
        e.preventDefault();
        var appointmentId = $(this).data('id');
        $.ajax({
            url: '/DoctorAppointments/CancelAppointment',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ id: appointmentId }),
            dataType: 'json',
            success: function (data) {                
                doctorAppointmentsTable.ajax.reload();
            },
            error: function (error) {
                console.error('Error cancelling appointment: ', error);
            }
        });
    });

    // Event handler for close button click
    $('#doctorAppointmentsTable').on('click', '.close-btn', function (e) {
        e.preventDefault();
        var appointmentId = $(this).data('id');
        $.ajax({
            url: '/DoctorAppointments/CloseAppointment',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ id: appointmentId }),
            dataType: 'json',
            success: function (data) {
                doctorAppointmentsTable.ajax.reload();
            },
            error: function (error) {
                console.error('Error closing appointment: ', error);
            }
        });
    });


});