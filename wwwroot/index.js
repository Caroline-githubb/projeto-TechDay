function clickProgramas(){
    
    window.location.href = baseUrl + "/listaProgramas.html";
}

function clickCadastrar(){
    
    if (!txtNome.value || !txtEmail.value) {
        alert("É obrigatório preencher os dois campos.");
        
    }

    else {
        var data = JSON.stringify({
            "Nome": txtNome.value,
            "Email": txtEmail.value,            
        });

        let url = baseApiUrl + "/Inscricao/InscricaoEmail"
        let xhttp = new XMLHttpRequest();
        xhttp.open("POST", url, false);
        xhttp.setRequestHeader("Content-Type", "application/json");
        xhttp.send(data);//A execução do script pára aqui até a requisição retornar do servidor

        if (xhttp.status == 500){
            var teste = xhttp.responseText;
            localStorage.setItem("Teste", teste)
            alert("Cadastro já existe, digite outro e-mail")
        }

        else{
            alert("Cadastrado com sucesso.")
        }
       
        
    }
}

function clickAdmin(){
    window.location.href= baseUrl + "/administrador.html"
}



function home(){
    
    window.location.href = baseUrl + "/index.html"
}