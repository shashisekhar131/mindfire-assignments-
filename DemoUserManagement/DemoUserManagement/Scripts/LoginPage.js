
function AuthenticateUser(UserEmail, UserPassword) {
    $.ajax({
        url: 'LoginPage.aspx/CheckIfUserExists',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ UserEmail, UserPassword }), // Send the countryName parameter
        dataType: 'json',
        success: function (data) {
            console.log(data.d);
            var User = data.d;
            if (User["IsUserExists"]) {
                console.log("your a valid user");
                document.getElementById("tip").innerHTML = "your a valid user";
                window.location.href = "/UserDetails.aspx?id=" + User["UserID"];
            } else {
                console.log("please first sign up");
                document.getElementById("tip").innerHTML = "please create an account";
            }


        },
        error: function (xhr, status, error) {
            console.log('Error loading states: ', error);
            console.log(xhr.responseText);
        }
    });
}


$(document).ready(function () {

    $('#SubmitBtn').on('click', function (event) {
        event.preventDefault(); // Prevent the default form submission
        console.log("entered" + $('#UserEmail').val() + $('#UserPassword').val());
        AuthenticateUser($('#UserEmail').val(), $('#UserPassword').val());

    });

    $('#NewUserBtn').on('click', function (event) {
        event.preventDefault();
        window.location.href = "/UserDetails.aspx";
    })

});