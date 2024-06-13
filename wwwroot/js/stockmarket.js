var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Json/GetAll' },
        "columns": [
            { "data": 'date', "width": "10%" },
            { "data": 'trade_Code', "width": "10%" },
            { "data": 'high', "width": "10%" },
            { "data": 'low', "width": "10%" },
            { "data": 'open', "width": "10%" },
            { "data": 'close', "width": "10%" },
            { "data": 'volume', "width": "10%" },

            {
                data: 'id',
                "render": function (data) {
                    return `
                    <div class="w-75 btn-group" role="group">
                        <a href="/Json/Update?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                        <a onClick=Delete('/Json/Delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                    </div>
                    `;

                },
                "width": "30%"
            }
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

