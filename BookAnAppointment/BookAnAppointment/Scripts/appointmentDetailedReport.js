$(document).ready(function () {
    var appointmentDetailedTable = $('#appointmentDetailedTable').DataTable();

    // Set the default value of the month input to the current year and month
    var currentDate = new Date();
    var defaultMonth = currentDate.toISOString().slice(0, 7);

    $('#detailedDateInput').val(defaultMonth);

    // Initial load with default month
    loadAppointmentDetailedData();

    $('#detailedDateInput').on('change', function () {
        loadAppointmentDetailedData();
    });

    function loadAppointmentDetailedData() {
        var selectedMonth = $('#detailedDateInput').val();
        var doctorId = $('#doctorId').val();
        $.ajax({
            url: '/DoctorAppointments/GetDetailedForMonth',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ selectedMonth, doctorId: parseInt(doctorId) }),
            success: function (data) {
                // Update DataTable with new data
                appointmentDetailedTable.clear();
                // Converting each object to an array and add to the DataTable
                data.forEach(function (appointment) {
                    appointmentDetailedTable.row.add([
                        new Date(parseInt(appointment.Date.replace("/Date(", "").replace(")/", ""), 10)).toLocaleDateString(),
                        appointment.PatientName,
                        appointment.Status
                    ]);
                });
                // Draw the updated DataTable
                appointmentDetailedTable.draw();
            },
            error: function (xhr, status, error) {
                console.log(error);
                console.log(xhr.responseText);
            }
        });
    }
    $('#exportPdfBtn').on('click', function () {
        var columns = ["Date", "Patient Name", "Status"];
        var rows = [];
        // Insert data table row data in rows array
        appointmentDetailedTable.rows().data().each(function (value, index) {
            rows.push(value);
        });

        var doc = new jsPDF();
        // We can adjust the position and styling as needed
        doc.text("Appointment detailed Report for " + $('#doctorName').val() +" on " + $('#detailedDateInput').val(), 14, 15);

        // Set table header
        doc.autoTable({
            head: [columns],
            body: rows,
            startY: 20
        });
        doc.save('detailed_appointments_report.pdf');
    });
});
