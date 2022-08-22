$(document).ready(function () {
    obterClientes()
    obterEnderecoCliente()
})

function obterClientes() {
    $.ajax({
        method: "GET",
        async: true,
        url: "/Cliente/ObterCliente",
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

function limpaformulariocep() {
    $(".logradouro-cliente").val("");
    $(".bairro-cliente").val("");
    $(".cidade-cliente").val("");
    $(".uf-cliente").val("");
    $(".cod-ibge-cliente").val("");
}

function obterEnderecoCliente() {
    $(".cep-cliente").blur(function () {

        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {

            var validacep = /^[0-9]{8}$/;

            if (validacep.test(cep)) {
                $(".logradouro-cliente").val("...");
                $(".bairro-cliente").val("...");
                $(".cidade-cliente").val("...");
                $(".uf-cliente").val("...");
                $(".cod-ibge-cliente").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        $(".logradouro-cliente").val(dados.logradouro);
                        $(".bairro-cliente").val(dados.bairro);
                        $(".cidade-cliente").val(dados.localidade);
                        $(".uf-cliente").val(dados.uf);
                        $(".cod-ibge-cliente").val(dados.ibge);
                    }
                    else {
                        limpaformulariocep();
                        alert("CEP não encontrado.");
                    }
                });
            }
            else {
                limpaformulariocep();
                alert("Formato de CEP inválido.");
            }
        }
        else {
            limpaformulariocep();
        }
    });
}
