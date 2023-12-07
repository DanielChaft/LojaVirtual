window.onload = function (e) {

    var btnCadastro = document.getElementById("btnCadastro");
    var txtNome = document.getElementById("txtNome");
    var txtSobrenome = document.getElementById("txtSobrenome");
    var txtEmail = document.getElementById("txtEmail");
    var txtTelefone = document.getElementById("txtTelefone");
    var slcGenero = document.getElementById("slcGenero");
    var txtSenha = document.getElementById("txtSenha");

    txtNome.focus();

    btnCadastro.onclick = function (e) {

        e.preventDefault();

        var nome = txtNome.value;
        var sobrenome = txtSobrenome.value;
        var email = txtEmail.value;
        var telefone = txtTelefone.value;
        var genero = slcGenero.value;
        var senha = txtSenha.value;

        if (nome == "") {
            mensagemErro("Nome obrigatório!");
        }
        else if (sobrenome == "") {
            mensagemErro("Sobrenome obrigatório!");
        }
        else if (email == "") {
            mensagemErro("E-mail obrigatório!");
        }
        else if (telefone == "") {
            mensagemErro("Telefone obrigatório!");
        }
        else if (senha == "") {
            mensagemErro("Senha obrigatório!");
        }
        else {
            cadastrarDados(nome, sobrenome, email, telefone, genero, senha);
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

    function cadastrarDados(nome, sobrenome, email, telefone, genero, senha) {

        var data = JSON.stringify({
            "nome": nome,
            "sobrenome": sobrenome,
            "email": email,
            "telefone": telefone,
            "genero": genero,
            "senha": senha
        });

        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener("readystatechange", function () {
            if (this.readyState === 4) {

                var loginResult = JSON.parse(this.responseText);

                if (loginResult.sucesso)
                {
                    localStorage.setItem("usuarioGuid", loginResult.usuarioGuid);
                    window.location.href = "home.html";
                }
                else {
                    mensagemErro(loginResult.mensagem);
                }
            }
        });

        xhr.open("POST", "https://localhost:44305/api/usuario/cadastro");
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);

    }
}