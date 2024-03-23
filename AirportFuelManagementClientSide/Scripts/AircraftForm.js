$(document).ready(function(){
    $('#airportForm').show();
    $('#message').hide();
    $('#aircraftForm').submit(function(event){       
  
        event.preventDefault();
        
        var aircraft = {
            AircraftNumber: $('#AircraftNumber').val(),
            AirLine: $('#AirLine').val(),
            Source: $('#Source').val(),
            Destination: $('#Destination').val()
        };

        $.ajax({
            url: 'https://localhost:7053/api/Aircraft', 
            type: 'POST', 
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            },
            contentType: 'application/json',
            data: JSON.stringify(aircraft), 
            success: function(response) {
                $('#aircraftForm').hide();
                $('#message').text('Aircraft added successfully').addClass('alert alert-info').show();     
           },
            error: function(xhr, status, error) {
                console.error('Error:', error);
            }
        });
    });
});


