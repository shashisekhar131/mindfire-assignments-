jQuery.noConflict();

jQuery(document).ready(function () {


    $('#documentsTable').DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        lengthMenu: [5, 10, 25, 50],
        ajax: {
            type: "Post",
            url: 'Documents/GetDocuments',
            dataType: "json",
            data: function (d) {
                // Add additional parameters to the data object
                d.ObjectID = parseInt($("#DocumentsUserID").val());
                d.ObjectType = parseInt($("#DocumentsObjectType").val());
            }
        },
        columns: [
            { "data": "DocumentID", "name": "DocumentID", "autowidth": true },
            {
                "data": "DocumentOriginalName",
                "name": "DocumentOriginalName",
                "autowidth": true,
                "render": function (data, type, full, meta) {
                    // Assuming full is your data object, use it to construct the link
                    return '<a href="' + 'Documents/Download' +
                        '?fileName=' + full.DocumentGuidName + '" target="_blank">' +
                        data + '</a>';
                }
            },
            { "data": "TimeStamp", "name": "TimeStamp", "autowidth": true },
            { "data": "DocumentType", "name": "DocumentType", "autowidth": true },



        ], columnDefs: [
            {
                targets: [0],
                searchable: false,
                type: "num"
            }
        ]
    });

    $("#BtnUpload").click(function (event) {
        event.preventDefault();

        var fileInput = $("#fileInput")[0];
        var file = fileInput.files[0];

        if (!file) {
            alert("Please select a file");
            return;
        }
        var UserID = parseInt($("#DocumentsUserID").val());
        var ObjectType = parseInt($("#DocumentsObjectType").val());

        var fileType = parseInt($("#fileType").val());

        var formData = new FormData();
        formData.append("file", file);
        formData.append("DocumentType", fileType);
        formData.append("ObjectType", ObjectType);
        formData.append("ObjectID", UserID);
        $.ajax({
            type: "POST",
            url: 'Documents/UploadDocument',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                if (result.success) {
                    location.reload();
                } else {
                    // Handle failure
                    alert("Failed to upload document.");
                }
            },
            error: function (xhr, status, error) {
                console.log('Error uploading document: ', error);
                console.log(xhr.responseText);
            }
        });
    });
});