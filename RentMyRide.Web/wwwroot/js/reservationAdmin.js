var dataTable;


$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Reservation/GetAll"
        },
        "columns": [
            { "data": "pickUpDate", "widht": "10%" },
            { "data": "dropOffDate", "widht": "10%" },            
            { "data": "car.make", "widht": "10%" },
            { "data": "status", "widht": "10%" },
            {
                "data": "null",
                "render": function (data, type, row) {
                    return `
                    <div class="w-75 btn-group" role="group">
                        <p>
                            ${row.applicationUser.firstName} ${row.applicationUser.lastName}
                        </p>

                    </div>
                    `
                },
                "widht": "10%"
            },
            {
                "data": "null",
                "render": function (data, type, row) {
                    return `
                    <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Reservation/Update?ReservationId=${row.id}&CarId=${row.car.id}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i>Edit
                        </a>
                    </div>
                    `
                },
                "widht": "15%"
            },
        ]
    });

}
