window.onload = function (e) {

    var btnCadastroProduto = document.getElementById("btnCadastroProduto");
    var txtCodigoProd = document.getElementById("txtCodigoProd");
    var txtNomeProd = document.getElementById("txtNomeProd");
    var txtPrecoProd = document.getElementById("txtPrecoProd");
    var txtQtdEstoqueProd = document.getElementById("txtQtdEstoqueProd");
    
    txtCodigoProd.focus();

    btnCadastroProduto.onclick = function (e) {

        e.preventDefault();

        var codigo = txtCodigoProd.value;
        var nome = txtNomeProd.value;
        var preco = txtPrecoProd.value;
        var quantidade = txtQtdEstoqueProd.value;

        if (codigo == "") {
            mensagemErro("Código obrigatório!");
        }
        else if (nome == "") {
            mensagemErro("Nome obrigatório!");
        }
        else if (preco == "") {
            mensagemErro("Preço obrigatório!");
        }
        else if (quantidade == "") {
            mensagemErro("Quantidade obrigatório!");
        }
        else {
            cadastrarProduto(codigo, nome, preco, quantidade);
        }
    };

    function mensagemErro(mensagem) {
        var spnErro = document.getElementById("spnErro");
        spnErro.innerText = mensagem;
        spnErro.style.display = "block";
        setTimeout(function () {
            spnErro.style.display = "none";
        }, 5000);
    }

    function cadastrarProduto(codigo, nome, preco, quantidade) {

        var data = JSON.stringify({
            "codigo": codigo,
            "nome": nome,
            "preco": preco,
            "quantidade": quantidade
        });

        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener("readystatechange", function () {
            if (this.readyState === 4) {

                var produtoResult = JSON.parse(this.responseText);

                if (produtoResult.sucesso)
                {
                    localStorage.setItem("codigo", produtoResult.codigo);
                    window.location.href = "home.html";
                }
                else {
                   mensagemErro(produtoResult.mensagem);
                }
           }
        });

        xhr.open("POST", "https://localhost:44305/api/produto/cadastro");
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);

    }
}