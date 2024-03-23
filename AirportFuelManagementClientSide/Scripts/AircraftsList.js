
function loadAircraftData() {
   
    var aircraftTable = $('#aircraftTable').DataTable();
    $.ajax({
        url: 'https://localhost:7053/api/Aircraft', 
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {
            aircraftTable.clear();
            // Add each aircraft to the DataTable
            data.forEach(function (aircraft) {
                aircraftTable.row.add([
                    aircraft.aircraftId,
                    aircraft.aircraftNumber,
                    aircraft.airLine,
                    aircraft.source,
                    aircraft.destination
                ]);
            });
            // Draw the updated DataTable
            aircraftTable.draw();
        },
        error: function (xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    });
}

$(document).ready(function () {
    
        loadAircraftData();
      
});