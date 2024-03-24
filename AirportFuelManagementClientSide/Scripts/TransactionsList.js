 
    $(document).ready(function () {

        var transactionTable = $('#transactionTable').DataTable({
            lengthMenu:  [3,5,10,15] , 
            pageLength: 3
            });
        $('#transactionTable tbody').on('click', '.reverse-transaction-btn', function() {
            var id = $(this).data('transaction-id');
            window.location.href = "../Views/ReverseTransactionForm.html?parentTransactionId=" + id;
        });
        
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
                        var reverseTransactionBtn = '<button class="btn btn-primary btn-sm reverse-transaction-btn" data-transaction-id="' + transaction.transactionId + '">reverse</button>';

                        transactionTable.row.add([
                            transaction.transactionId,
                            transaction.transactionTime,
                            (transaction.transactionType == 0)?"OUT":"IN",
                            transaction.airportName,
                            transaction.aircraftName,
                            transaction.quantity,
                            transaction.transactionIdparent,
                            reverseTransactionBtn
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