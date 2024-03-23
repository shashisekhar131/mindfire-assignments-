$(document).ready(function(){
      
    $('#airportForm').show();
    $('#message').hide();
    $('#airportForm').submit(function(event){
       event.preventDefault();      

       var airport = {   
       airportName: $('#AirportName').val(),
       fuelCapacity:parseInt($('#FuelCapacity').val())
      };


       $.ajax({
           url: 'https://localhost:7053/api/AirPort', 
           type: 'POST', 
           headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
           contentType: 'application/json',
           data: JSON.stringify(airport), 
           success: function(response) {
            $('#airportForm').hide();
            $('#message').text('Airport added successfully').addClass('alert alert-info').show();
        },
           error: function(xhr, status, error) {
               // Handle errors
               console.error('Error:', error,xhr,status);
           }
       });
   });
});