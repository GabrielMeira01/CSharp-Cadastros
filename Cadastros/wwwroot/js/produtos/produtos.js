$(document).ready(function () {
    obterProdutos()
    obterEnderecoProduto()
})

function obterProdutos() {
    $.ajax({
        method: "GET",
        async: true,
        url: "/Produto/ObterProduto",
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
