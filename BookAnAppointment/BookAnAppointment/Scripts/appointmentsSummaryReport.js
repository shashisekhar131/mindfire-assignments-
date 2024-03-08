
$(document).ready(function () {
    var appointmentSummaryTable = $('#appointmentSummaryTable').DataTable();

    // Set the default value of the month input to the current year and month
    var currentDate = new Date();
    var defaultMonth = currentDate.toISOString().slice(0, 7);

    $('#summaryDateInput').val(defaultMonth);
    alert(selectedMonth = $('#summaryDateInput').val());

    // Initial load with default month
    loadAppointmentSummaryData();

    // Event handler for month input change
    $('#summaryDateInput').on('change', function () {
        loadAppointmentSummaryData();
    });
    function loadAppointmentSummaryData() {
        var selectedMonth = $('#summaryDateInput').val();
        var doctorId = $('#doctorId').val();
        $.ajax({
            url: '/DoctorAppointments/GetSummaryForMonth',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ selectedMonth, doctorId: parseInt(doctorId) }),
            success: function (data) {
                // Update DataTable with new data
                // Assuming data is your array of objects

                appointmentSummaryTable.clear();
                // Convert each object to an array and add to the DataTable
                data.forEach(function (appointment) {
                    appointmentSummaryTable.row.add([
                        new Date(parseInt(appointment.Date.replace("/Date(", "").replace(")/", ""), 10)).toLocaleDateString(),
                        appointment.TotalAppointments,
                        appointment.ClosedAppointments,
                        appointment.CancelledAppointments
                    ]);
                });

                // Draw the updated DataTable
                appointmentSummaryTable.draw();

            },
            error: function (xhr, status, error) {
                console.log(error);
                console.log(xhr.responseText);
            }
        });
    }
    $('#exportPdfBtn').on('click', function () {
        var columns = ["Date", "Total Appointments", "Closed Appointments", "Cancelled Appointments"];
        var rows = [];
        // Insert data table row data in rows array
        appointmentSummaryTable.rows().data().each(function (value, index) {
            rows.push(value);
        });

        var doc = new jsPDF();
        // Set table header
        doc.autoTable({
            head: [columns],
            body: rows
        });
        doc.save('appointment_summary.pdf');
    });   
});
