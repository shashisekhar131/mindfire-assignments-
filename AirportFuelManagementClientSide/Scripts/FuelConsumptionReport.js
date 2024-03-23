
$(document).ready(function () {
    var fuelConsumptionTable = $('#fuelConsumptionTable').DataTable({
        lengthMenu:  [3,5,10,15] , 
        pageLength: 3
        });

    $.ajax({
        url: 'https://localhost:7053/api/Transaction/FuelConsumptionReport',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {
            console.log(data);
            data.forEach(function (record) {                
                // Concatenate transactions into a single string
                var transactionsString = record.transactions.map(function (transaction) {
                    fuelConsumptionTable.row.add([
                    record.airportName,
                    `Transaction Time: ${transaction.transactionTime}, Type: ${transaction.transactionType}, Quantity: ${transaction.quantity}, Aircraft Name: ${transaction.aircraftName}`,
                    record.airportFuelAvailable
                  ]);
                });
               
               
            });

            fuelConsumptionTable.draw();
        },
        error: function (xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    });

    $('#exportPdfBtn').on('click', function () {
     var columns = ["Airport", "Transactions", "Fuel Available"];
     var rows = [];
     // Insert data table row data in rows array
     fuelConsumptionTable.rows().data().each(function (value, index) {
         rows.push(value);
     });

     var doc = new jsPDF();
     // We can adjust the position and styling as needed
     doc.text("fuel consumption report", 14, 15);

     // Set table header
     doc.autoTable({
         head: [columns],
         body: rows,
         startY: 20
     });
     doc.save('fuel_consumption_report.pdf');
 });
});
