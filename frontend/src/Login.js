window.onload = function (e) {
    var btnLogin = document.getElementById("btnLogin");
    var txtEmail = document.getElementById("txtEmail");
    var txtPassword = document.getElementById("txtPassword");
    txtEmail.focus();

    btnLogin.onclick = function (e) {

        e.preventDefault();

        var email = txtEmail.value;
        var senha = txtPassword.value;

        if (email == "") {
            mensagemErro("O campo E-mail é obrigatório!");
        }
        else if (senha == "") {
            mensagemErro("O campo Senha é obrigatório");
        }
        else {
            realizarLogin(email, senha);
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

    function realizarLogin(email, senha) {

        var login = {
            "email": email,
            "senha": senha
        };
        var data = JSON.stringify(login);

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
                else
                {
                    mensagemErro(loginResult.mensagem);
                }
            }
        });

        xhr.open("POST", "https://localhost:44305/api/usuario/login");
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);
    }
}