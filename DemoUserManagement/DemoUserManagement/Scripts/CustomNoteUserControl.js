$(document).ready(function () {


    $('#NotesBtn').on('click', function (event) {
        event.preventDefault();
        let urlParams = new URLSearchParams(window.location.search);
        let UserID = urlParams.get('id');
        let ObjectType = $('#NoteInObjectType').val();
        let NoteText = $('#NotesInput').val();
        $.ajax({
            url: 'UserDetails.aspx/InsertNotes',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ NoteText, UserID, ObjectType }),
            dataType: 'json',
            success: function (data) {
                console.log(data.d);


            },
            error: function (xhr, status, error) {
                console.log('Error loading states: ', error);
                console.log(xhr.responseText);
            }
        });

    });
});

