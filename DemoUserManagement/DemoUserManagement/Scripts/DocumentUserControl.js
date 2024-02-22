$(document).ready(function () {


    $('#BtnUpload').on('click', function (event) {
        // Get the selected file
        event.preventDefault();
        var fileInput = $('#fileInput')[0];
        var file = fileInput.files[0];

        var urlParams = new URLSearchParams(window.location.search);
        var id = urlParams.get('id');

        var fileTypeValue = parseInt($('#fileType').val());
        var objType = $("#DocumentInObjectType").val()

        if (file) {
            // Create FormData object and append the file
            var formData = new FormData();
            formData.append('file', file);
            formData.append('UserID', id);
            formData.append('FileType', fileTypeValue);
            formData.append('ObjectType', objType);

            $.ajax({
                url: '/UploadHandler.ashx',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    // Handle the success response
                    console.log("uploaded");
                },
                error: function (xhr, status, error) {
                    console.log(" not uploaded");
                }
            });
        } else {
            console.log("select a filie");
        }

    });
});