window.onload = function (e) {
    var btnRecuperar = document.getElementById("btnRecuperar");
    var txtEmail = document.getElementById("txtEmail");
    txtEmail.focus();
        
    btnRecuperar.onclick = function (e) {
        e.preventDefault();

        var email = txtEmail.value;
        
        if (email == "") {
            mensagemErro("O campo E-mail é obrigatório!");
        }
        else {
            recuperarSenha(email);
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

    function recuperarSenha(email) {

        var data = JSON.stringify({
            "email": email
        });

        var xhr = new XMLHttpRequest();
        xhr.withCredentials = true;

        xhr.addEventListener("readystatechange", function () {
            if (this.readyState === 4) {

                var loginResult = JSON.parse(this.responseText);
                if (loginResult.sucesso) {
                    alert("Recuperação de senha enviada para o e-mail cadastrado!");
                }
                else {
                    mensagemErro(loginResult.mensagem);
                }
            }
        });

        xhr.open("POST", "https://localhost:44305/api/usuario/esqueceuSenha");
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);
    }

}