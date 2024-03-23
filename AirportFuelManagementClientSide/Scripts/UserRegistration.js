$(document).ready(function () {
    $('#userRegistrationForm').submit(function (event) {
        event.preventDefault(); // Prevent form submission


// Create data object to be sent to the server
var user = {
  Email: $('#email').val(),
  Password: $('#password').val(),
  Name: $('#name').val()
};
        // AJAX request to post form data to Web API
        $.ajax({
            url: 'https://localhost:7053/api/User',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function (response) {
                console.log('Registration successful:', response);
                // Handle success response here
            },
            error: function (xhr, status, error) {
                console.log('Registration failed:', error);
                // Handle error response here
            }
        });
    });
});