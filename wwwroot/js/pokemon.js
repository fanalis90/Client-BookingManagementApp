//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon/",
//    //success: (result) => {
//    //    console.log(result);
//    //}
//}).done((result) => {
//    console.log(result)
//    let temp = "";
//    $.each(result.results, (key, val) => {
//        temp += `<tr>
//                    <td>${key + 1}</td>
//                    <td>${val.name}</td>
//                    <td><button type="button" onclick="detail('${val.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
//                </tr >`;
//    });
//    $("#tbodyPoke").html(temp);


//}).fail((error) => {
//    console.log(error);
//})
$("#tbodyPoke").DataTable({
    ajax: {
        url: "https://pokeapi.co/api/v2/pokemon/",
        dataSrc: "results",
        dataType: "JSON"
    },
    columns: [

        { data: "name" },
        {
            data: "url",
            render: function (data, type, row) {
                return `<button type="button" onclick="detail('${row.url}')" class="btn btn-primary" data-toggle="modal" data-target="#modalPoke">Detail</button>`;
            }
        }
    ]

});

function detail(stringUrl) {
    $.ajax({
        url: stringUrl
    }).done((res) => {
        console.log(res);
        $("#gambar-pokemon").attr("src", res.sprites.front_default);
        $(".tinggi").html(`height : <p >${res.height}</p>`);
        $(".berat").html(`weight : <p >${res.weight}</p>`);
        $(".modal-title").html(res.name);
        let move = "";
        $.each(res.moves, (key, val) => {
            move += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.move.name}</td>
                </tr >`;
        });
        $("#PokeMove").html(move);

        let type = "";
        $.each(res.types, (key, val) => {
            type += `<span class="badge bg-${val.type.name}">
                    ${val.type.name}
                </span>`;
        });
        $("#tipe-pokemon").html(type);
        let stat = "";
        $.each(res.stats, (key, val) => {
            stat += `<span>${val.stat.name}</span>
                    <div class="progress" role="progressbar" aria-label="Basic example" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="120">
                        <div class="progress-bar" style="width: ${(val.base_stat / 120) * 100}%">${val.base_stat}</div>
                    </div>`;
        });
        $(".stat-pokemon").html(stat);
    })
}