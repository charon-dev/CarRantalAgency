var dataTable;


$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Customer/Renting/GetAll"
        },
        "columns": [
            { "data": "startDate", "widht": "10%" },
            { "data": "endDate", "widht": "10%" },
            { "data": "status", "widht": "10%" },
            { "data": "totalCharge", "widht": "10%" },
            { "data": "car.make", "widht": "10%" },
        ]
    });

}

