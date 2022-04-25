function listaProgramas() {

    const url = baseApiUrl + "/Programa/ListarProgramas";

    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", url, false);
    xhttp.send();//A execução do script para aqui até a requisição retornar do servidor

    const lista = JSON.parse(xhttp.responseText);

    lista.forEach(element => {
        var programa = document.createElement('div');//criar um elemento HTML
        programa.innerHTML = "<h2>" + element.nome + "<br>" + element.tema + "<br>" + element.descricao + "<br>" + element.site + "</h2>" 
        document.getElementById('programas').appendChild(programa);//o elemento recem criado "serie" como filho, é inserido ao elemento pai(series) no html
    });
}

function clickAdmin() {
    window.location.href = baseUrl + "/administrador.html"
}

function clickProgramas() {

    window.location.href = baseUrl + "/listaProgramas.html";
}

function home() {

    window.location.href = baseUrl + "/index.html"
}