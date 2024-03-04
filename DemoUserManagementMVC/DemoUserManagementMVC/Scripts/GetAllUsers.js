
    jQuery.noConflict();
    jQuery(document).ready(function ($) {

        $('#userDetailsTable').DataTable({
            processing: true,
            serverSide: true,
            filter: true,
            ajax: {
                type: "Post",
                url: '/Users/GetData',
                dataType: "json"
            },
            columns: [
                { "data": "UserID", "name": "UserID", "autowidth": true },
                { "data": "FirstName", "name": "FirstName", "autowidth": true },
                { "data": "LastName", "name": "LastName", "autowidth": true },
                { "data": "Password", "name": "Password", "autowidth": true },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return '<a href="/UserRegistrationV2/Index/' + row.UserID + '" class="btn btn-primary">Edit</a>';
                    },
                    "name": "Edit",
                    "autowidth": true
                }

            ], columnDefs: [
                {
                    targets: [0],
                    searchable: false,
                    type: "num"
                }
            ]
        });
    });
