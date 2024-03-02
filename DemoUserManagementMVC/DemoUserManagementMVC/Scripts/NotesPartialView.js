
jQuery.noConflict();
jQuery(document).ready(function ($) {


    $('#notesTable').DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        lengthMenu: [5, 10, 25, 50],
        ajax: {
            type: "Post",
            url: 'Notes/GetNotes',
            dataType: "json",
            data: function (d) {
                // Add additional parameters to the data object
                d.ObjectID = parseInt($("#NotesUserID").val());
                d.ObjectType = parseInt($("#NotesObjectType").val());
            }
        },
        columns: [
            { "data": "NotesID", "name": "NotesID", "autowidth": true },
            { "data": "NoteText", "name": "NoteText", "autowidth": true },
            { "data": "CreatedDate", "name": "CreatedDate", "autowidth": true },

        ], columnDefs: [
            {
                targets: [0],
                searchable: false,
                type: "num"
            }
        ]
    });

    $("#NotesBtn").click(function (event) {
        event.preventDefault();

        var noteText = $("#NotesInput").val();
        var notesUserID = parseInt($("#NotesUserID").val());
        var ObjectType = parseInt($("#NotesObjectType").val());
        $.ajax({
            type: "POST",
            url: 'Notes/SaveNote',
            data: JSON.stringify({ NoteText: noteText, UserID: notesUserID, ObjectType }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.success) {
                } else {
                    // Handle failure

                }
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
            }
        });
    });
});