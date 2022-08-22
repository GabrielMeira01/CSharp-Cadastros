$(document).ready(function () {
    obterPedidoss()
    obterEnderecoPedidos()
})

function obterPedidoss() {
    $.ajax({
        method: "GET",
        async: true,
        url: "/Pedidos/ObterPedidos",
        data: null,
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            $(".loading").html(data);
        },
        error: function () {
            alert("erro")
        },
    })
}
