var token = localStorage.getItem("token")

function carregarPagina(){
    if (!token) {
        window.location.href= baseUrl + "/administrador.html";
    }
}

function clickAdmin(){
    window.location.href= baseUrl + "/administrador.html"
}

function clickProgramas(){
    
    window.location.href = baseUrl + "/listaProgramas.html";
}

function home(){
    
    window.location.href = baseUrl + "/index.html"
}

function clickSalvar() 
{
    if (!txtNome.value || !txtTema.value || !txtSite.value || !txtDescricao.value) {
        alert("É obrigatório preencher os campos com com * .");
        return;
    }

    else {
        var data = JSON.stringify({
            "Nome": txtNome.value,
            "Tema": txtTema.value,
            "Site": txtSite.value,
            "Descricao": txtDescricao.value
        });

        let url = baseApiUrl + "/Programa/CadastrarPrograma"
        let xhttp = new XMLHttpRequest();
        xhttp.open("POST", url, false);
        xhttp.setRequestHeader("Content-Type", "application/json");
        xhttp.setRequestHeader("Authorization", "Bearer " + token);
        xhttp.send(data);//A execução do script pára aqui até a requisição retornar do servidor

        alert("Programa cadastrado com sucesso.")
        
        window.location.href = "file:///home/carol/Visual%20Studio/SeriesFavoritas/SeriesFavoritas2/home.html";
    }
}