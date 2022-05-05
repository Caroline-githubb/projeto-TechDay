function clickAdmin(){
    window.location.href= baseUrl + "/administrador.html"
}

function clickProgramas(){
    
    window.location.href = baseUrl + "/listaProgramas.html";
}

function home(){
    
    window.location.href = baseUrl + "/index.html"
}

function clickEntrar() 
{
    if (!txtEmail.value) 
    {
        alert("e-mail inválido");
    }

    var url = baseApiUrl + "/Usuario/VerificarUsuario?email=" + encodeURIComponent(txtEmail.value) + "&senha=" + encodeURIComponent(txtSenha.value);//Sua URL

    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", url, false);
    xhttp.send();//A execução do script pára aqui até a requisição retornar do servidor

    if (xhttp.status == 200){
        var token = xhttp.responseText;
        localStorage.setItem("token", token);
        window.location.href = baseUrl + "/cadastroPrograma.html";
    }
    else{
        alert("E-mail ou senha incorreta!")
    }
    console.log(xhttp.responseText);
}