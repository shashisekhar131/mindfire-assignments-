function loadAirportData(jwtToken) {
    var airportTable = $('#airportTable').DataTable();

    $.ajax({
        url: 'https://localhost:7053/api/Airport', 
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + jwtToken 
        },
        success: function (data) {
            airportTable.clear();
            // Add each airport to the DataTable
            data.forEach(function (airport) {
                airportTable.row.add([
                    airport.airportId,
                    airport.airportName,
                    airport.fuelCapacity,
                    airport.fuelAvailable
                ]);
            });
            // Draw the updated DataTable
            airportTable.draw();
        },
        error: function (xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    });
}

$(document).ready(function () {
    var jwtToken = (localStorage.getItem('jwtToken') || null);
    if(jwtToken == null) window.location.href = 'UserLogin.html';
    else{
        loadAirportData(jwtToken);
    }    
});