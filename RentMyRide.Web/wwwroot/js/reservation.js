var dataTable;


$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Customer/Reservation/GetAll"
        },
        "columns": [
            { "data": "pickUpDate", "widht": "10%" },
            { "data": "dropOffDate", "widht": "10%" },
            { "data": "status", "widht": "10%" },
            { "data": "car.make", "widht": "10%" },
        ]
    });

}
