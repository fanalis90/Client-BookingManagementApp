
let dataTableEmployee =  $("#tableEmployee").DataTable({
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
            url: "/Employee/ListJson",
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
                    // Create an HTML button and return it as a string
                    return `<div class="action-employee">
                    <button type="button" class="btn btn-primary editEmployeeBtn" data-id="${meta.row}" data-bs-target="#createEmployeeModal" data-bs-toggle="modal" ><i class="bi bi-pencil-square"></i></button>
                    <button type="button" id="" class="btn btn-danger deleteEmployeeBtn" data-id="${meta.row}" data-bs-target="#deleteEmployeeModal" data-bs-toggle="modal"><i class="bi bi-trash"></i></button> 
                    </div>`;
                    // Return the raw data for sorting and other purposes
                }

            }
        ]

    });
function getData() {
   
        $.ajax({
            url: '/Employee/ListJson',
            method: 'GET',
            dataType: 'json',
         
         
        }).done((res) => {
            console.log(res);
        }).fail((err) => {
            console.log(err);
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

    }).done((response) => {
        $("#createEmployeeModal").modal("hide");
        Swal.fire(
            'created!',
            'Your file has been created.',
            'success'
        )
        dataTableEmployee.ajax.reload();
    }).fail((error) => {
        alert(error);
    });
};

function updateEmployee() {
    let updateEmployeeForm = {};
    updateEmployeeForm.guid = $("#updateEmployeeGuid").val();
    updateEmployeeForm.nik = $("#nik").val();
    updateEmployeeForm.firstName = $("#firstName").val();
    updateEmployeeForm.lastName = $("#lastName").val();
    updateEmployeeForm.birthDate = $("#birthDate").val();
    updateEmployeeForm.gender = parseInt($("#gender").val());
    updateEmployeeForm.hiringDate = $("#hiringDate").val();
    updateEmployeeForm.email = $("#email").val();
    updateEmployeeForm.phoneNumber = $("#phoneNumber").val();

    $.ajax({
        url: "https://localhost:7290/api/Employee",
        type: "PUT",
        data: JSON.stringify(updateEmployeeForm),
        dataType: "json",
        contentType: "application/json;charset=utf-8"

    }).done((result) => {
        Swal.fire(
            'Updated!',
            'Your file has been updated.',
            'success'
        )
        $("#createEmployeeModal").modal("hide");
        dataTableEmployee.ajax.reload();

    }).fail((error) => {
        console.log(error)
        alert(error);
    });
};

let deleteEmployee = () => {

    let deleteEmployeeGuid = $("#deleteEmployeeGuid").val();


    $.ajax({
        url: `https://localhost:7290/api/Employee/${deleteEmployeeGuid}`,
        type: "DELETE",


    }).done((result) => {
        $("#deleteEmployeeModal").modal("hide");
        Swal.fire(
            'Deleted!',
            'Your file has been deleted.',
            'success'
        )
        dataTableEmployee.ajax.reload();

    }).fail((error) => {
        alert(error);
    });
};

$(document).ready(function () {
    dataTableEmployee;
    getData();
    $(".createEmployeeBtn").html(`<i class="bi bi-plus-circle"></i> Add`);
    $(".createEmployeeBtn").attr("data-bs-target", "#createEmployeeModal");
    $(".createEmployeeBtn").attr("data-bs-toggle", "modal");


    $(".createEmployeeBtn").on("click", () => {
        $("#submitCreateEmployee").css("display", "block");
        $("#submitUpdateEmployee").css("display", "none");
        $("#modalTitle").html("Create Employee");
        
    });

    $("#submitCreateEmployee").click(() => {
        createEmployee();
    });

    $("#tableEmployee").on("click", ".editEmployeeBtn", (e) => {
        $("#submitCreateEmployee").css("display", "none");
        $("#submitUpdateEmployee").css("display", "block");
        $("#modalTitle").html("Update Employee");

        const indexRow = $(e.target).closest(".editEmployeeBtn").attr("data-id");
        const currentRow = $("#tableEmployee").DataTable().row(indexRow).data();
        $("#updateEmployeeGuid").val(currentRow.guid);
        $("#nik").val(currentRow.nik);
        $("#firstName").val(currentRow.firstName);
        $("#lastName").val(currentRow.lastName);
        $("#birthDate").val(currentRow.birthDate.split('T')[0]);
        $("#gender").val(currentRow.gender);
        $("#hiringDate").val(currentRow.hiringDate.split('T')[0]);
        $("#email").val(currentRow.email);
        $("#phoneNumber").val(currentRow.phoneNumber);

    })

    $("#submitUpdateEmployee").click(() => {
        updateEmployee();
    });

    $("#tableEmployee").on("click", ".deleteEmployeeBtn", (e) => {

        const indexRow = $(e.target).closest(".deleteEmployeeBtn").attr("data-id");
        const currentRow = $("#tableEmployee").DataTable().row(indexRow).data();
        $("#deleteEmployeeGuid").val(currentRow.guid);

    })

    $("#submitDeleteEmployee").click(() => {
        deleteEmployee();
    });






});

