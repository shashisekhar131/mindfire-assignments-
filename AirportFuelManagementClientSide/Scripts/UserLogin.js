$(document).ready(function() {

    $('#loginForm').submit(function(event) {
      event.preventDefault(); 

      var UserEmail = $('#userEmail').val();
      var UserPassword = $('#password').val();      
  
      $.ajax({
        url: 'https://localhost:7053/api/Login/AuthenticateUser', 
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({UserEmail,UserPassword}),
        success: function(response) {
          localStorage.setItem('jwtToken', response.token);
          window.location.href="../Views/AirportsList.html";
        },
        error: function(xhr, status, error) {
          console.error('Error occurred:', error);
        }
      });
    });
  });
