window.onload = function (e) {

    var btnCadastroCliente = document.getElementById("btnCadastroCliente");
    var NomeCli = document.getElementById("NomeCliente");
    var CPF = document.getElementById("CPF");
    var Email = document.getElementById("email");
    var CEP = document.getElementById("cep");
    var Rua = document.getElementById("rua");
    var Numero = document.getElementById("numero");
    var Bairro = document.getElementById("bairro");
    var Cidade = document.getElementById("cidade");
    var Estado = document.getElementById("uf");
    
    NomeCli.focus();

    btnCadastroCliente.onclick = function (e) {

        e.preventDefault();

        var nome = NomeCli.value;
        var cpf = CPF.value;
        var email = Email.value;
        var cep = CEP.value;
        var rua = Rua.value;
        var numero = Numero.value;
        var bairro = Bairro.value;
        var cidade = Cidade.value;
        var uf = Estado.value;

        if (nome == "") {
            mensagemErro("Nome é obrigatório!");
        }
        else if (cpf == "") {
            mensagemErro("CPF é obrigatório!");
        }
        else if (email == "") {
            mensagemErro("E-mail é obrigatório!");
        }
        else if (cep == "") {
            mensagemErro("CEP é obrigatório!");
        }
        else if (rua == "") {
            mensagemErro("Rua é obrigatório!");
        }
        else if (numero == "") {
            mensagemErro("Número é obrigatório!");
        }
        else if (bairro == "") {
            mensagemErro("Bairro é obrigatório!");
        }
        else if (cidade == "") {
            mensagemErro("Cidade é obrigatório!");
        }
        else if (uf == "") {
            mensagemErro("Estado é obrigatório!");
        }
        else {
            cadastrarCliente(nome, cpf, email, cep, rua, numero, bairro, cidade, uf);
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

    function cadastrarCliente(nome, cpf, email, cep, rua, numero, bairro, cidade, uf) {

        var data = JSON.stringify({
            "nome": nome,
            "CPF": cpf,
            "email": email,
            "CEP": cep,
            "rua": rua,
            "numero": numero,
            "bairro": bairro,
            "cidade": cidade,
            "uf": uf,
        });

        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener("readystatechange", function () {
            if (this.readyState === 4) {

                var clienteResult = JSON.parse(this.responseText);

                if (clienteResult.sucesso)
                {
                    localStorage.setItem("codigo", clienteResult.codigo);
                    window.location.href = "home.html";
                }
                else {
                   mensagemErro(clienteResult.mensagem);
                }
           }
        });

        xhr.open("POST", "https://localhost:44305/api/cliente/cadastro");
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);

    }
}