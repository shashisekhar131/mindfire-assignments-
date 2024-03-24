function fetchAirports() {
    $.ajax({
        url: 'https://localhost:7053/api/Airport',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {
         
            $('#airportName').empty();
            $.each(data, function (index, airport) {
                $('#airportName').append('<option value="' + airport.airportId + '">' + airport.airportName + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function fetchAircrafts() {
    $.ajax({
        url: 'https://localhost:7053/api/Aircraft',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {                  

            $('#aircraftName').empty();
            $.each(data, function (index, aircraft) {
                $('#aircraftName').append('<option value="' + aircraft.aircraftId + '">' + aircraft.aircraftNumber + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

$(document).ready(function () {          

// fetch the data and populate into dropdowns 
fetchAirports();
fetchAircrafts();
$('#aircraftName').prop('disabled', true); 

$('#transactionType').change(function() {
    if ($(this).val() === "IN") {
        $('#aircraftName').prop('disabled', true); 
    } else {
        $('#aircraftName').prop('disabled', false); 
    }
});
$('#transactionForm').submit(function (event) {
    event.preventDefault(); 
    
    var  Transaction = {
        transactionType: (($('#transactionType').val() == "IN")?1:0),
        airportId: parseInt($('#airportName').val()),
        aircraftId: (($('#transactionType').val() == "IN")?0:parseInt($('#aircraftName').val())) ,
        quantity: parseFloat($('#quantity').val()),
    };

    $.ajax({
        url: 'https://localhost:7053/api/Transaction/InsertTransaction', 
        type: 'POST',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        contentType: 'application/json',
        data: JSON.stringify(Transaction),
        success: function (response) {
        console.log(response);
        },
        error: function (xhr, status, error) {
            console.error('Error:', error,xhr);
        }
    });
});
});