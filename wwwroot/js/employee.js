$("#tableEmployee").DataTable({
    dom: "Bfrtip",
    buttons: [
        {

            className: 'btn btn-outline-primary createEmployeeBtn',
        },
        {
            extend: 'excelHtml5',
            className: 'btn btn-outline-primary',
            exportOptions: {
                columns: ':visible'
            }
        },
        {
            extend: 'pdfHtml5',
            className: 'btn btn-outline-primary',
            exportOptions: {
                columns: [0, 1, 2, 3, 4, 5, 6, 7]
            }
        },
        {
            extend: 'colvis',
            className: 'btn btn-outline-primary',
        }
    ],
    ajax: {
        ordering: false,
        url: "https://localhost:7290/api/Employee",
        dataSrc: "data",
        dataType: "json"
    },
    columns: [
        {
            data: null,
            render: function (data, type, row, meta) {
                // Use the 'meta.row' value to calculate the row number
                return meta.row + 1;
            }
        },
        { data: "nik" },
        {
            data: null,
            render: function (data, type, row) {
                return data.firstName + " " + data.lastName;
            }
        },
        {
            data: "birthDate",
            render: function (data, type, row) {
                if (type === "display" || type === "filter") {
                    // Format the date as "DD-MM-YYYY"
                    var date = new Date(data);
                    var day = String(date.getDate()).padStart(2, '0');
                    var month = String(date.getMonth() + 1).padStart(2, '0');
                    var year = date.getFullYear();
                    return day + '-' + month + '-' + year;
                }
                return data; // Return the raw date for sorting and other purposes
            }
        },
        {
            data: "gender",
            render: function (data, type, row) {
                // 'gender' is a numeric value (e.g., 1 for male, 0 for female)
                return data === 1 ? 'Male' : 'Female';
            }
        },
        {
            data: "hiringDate",
            render: function (data, type, row) {
                if (type === "display" || type === "filter") {
                    // Format the date as "DD-MM-YYYY"
                    var date = new Date(data);
                    var day = String(date.getDate()).padStart(2, '0');
                    var month = String(date.getMonth() + 1).padStart(2, '0');
                    var year = date.getFullYear();
                    return day + '-' + month + '-' + year;
                }
                return data; // Return the raw date for sorting and other purposes
            }
        },
        { data: "email" },
        { data: "phoneNumber" },
        {
            data: null,
            render: function (data, type, row, meta) {
                if (type === "display" || type === "filter") {
                    // Create an HTML button and return it as a string
                    return `<div class="action-employee">
                    <button type="button" id="editEmployeeBtn" class="btn btn-primary"><i class="bi bi-pencil-square"></i></button>
                    <button type="button" class="btn btn-danger"><i class="bi bi-trash"></i></button> 
                    </div>`;
                }
                return data; // Return the raw data for sorting and other purposes
            }

        }
    ]

});
function getData() {
    $.ajax({
        url: 'https://localhost:7290/api/Employee', // Replace with the URL of the API or resource you want to fetch data from
        method: 'GET',
        dataType: 'json', // Specify the expected data type (json, xml, html, etc.)
        success: function (data) {
            // The data variable now contains the response from the server
            // You can process and use the data here
            return data;
        },
        error: function (xhr, status, error) {
            // Handle any errors or failed requests
            console.error('Request failed:', error);
        }
    });
}

let createEmployee = () => {
    var createEmployee = {};
    createEmployee.firstName = $("#firstName").val();
    createEmployee.lastName = $("#lastName").val();
    createEmployee.birthDate = $("#birthDate").val();
    createEmployee.gender = parseInt($("#gender").val());
    createEmployee.hiringDate = $("#hiringDate").val();
    createEmployee.email = $("#email").val();
    createEmployee.phoneNumber = $("#phoneNumber").val();

    $.ajax({
        url: "https://localhost:7290/api/Employee",
        type: "POST",
        data: JSON.stringify(createEmployee),
        dataType: "json",
        contentType: "application/json;charset=utf-8"

    }).done((result) => {
        //$("#tableEmployee").DataTable().row.add().draw()
    }).fail((error) => {
        alert(error.responseText);
    });
};

let editEmployee = () => {
    editEmployee = getData();
    editEmployee.firstName = $("#firstName").val();
    editEmployee.lastName = $("#lastName").val();
    editEmployee.birthDate = $("#birthDate").val();
    editEmployee.gender = parseInt($("#gender").val());
    editEmployee.hiringDate = $("#hiringDate").val();
    editEmployee.email = $("#email").val();
    editEmployee.phoneNumber = $("#phoneNumber").val();

    $.ajax({
        url: "https://localhost:7290/api/Employee",
        type: "POST",
        data: JSON.stringify(createEmployee),
        dataType: "json",
        contentType: "application/json;charset=utf-8"

    }).done((result) => {
        //$("#tableEmployee").DataTable().row.add().draw()
    }).fail((error) => {
        alert(error.responseText);
    });
};

$(document).ready(function () {
    $(".createEmployeeBtn").html(`<i class="bi bi-plus-circle"></i> Add`);
    $(".createEmployeeBtn").attr("data-bs-target", "#createEmployeeModal");
    $(".createEmployeeBtn").attr("data-bs-toggle", "modal");

    
    
    
    $("#submitCreateEmployee").click(() => {
        createEmployee();
    });


});

