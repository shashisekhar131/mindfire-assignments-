 
    $(document).ready(function () {
        var transactionTable = $('#transactionTable').DataTable();
    
        // Initial load
        loadTransactionData();
    
        function loadTransactionData() {
            $.ajax({
                url: 'https://localhost:7053/api/Transaction', // Replace with your API endpoint
                type: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
                },
                success: function (data) {
                    console.log(data);
                    transactionTable.clear();
                    // Add each transaction to the DataTable
                    data.forEach(function (transaction) {
                        transactionTable.row.add([
                            transaction.transactionId,
                            transaction.transactionTime,
                            (transaction.transactionType == 0)?"OUT":"IN",
                            transaction.airportName,
                            transaction.aircraftName,
                            transaction.quantity,
                            transaction.transactionIdparent,
                            transaction.reverse // Replace this with appropriate data for reverse column
                        ]);
                    });
                    // Draw the updated DataTable
                    transactionTable.draw();
                },
                error: function (xhr, status, error) {
                    console.log(error);
                    console.log(xhr.responseText);
                }
            });
        }
    });