function abrirModal(id){
    var modal = document.getElementById(id);
    modal.classList.add('abrir');
    
    modal.addEventListener('click', (evento) => {
        if(evento.target.id == id){
            modal.classList.remove('abrir');
        }
    })
    
}

function fecharModal(id){
    var modal = document.getElementById(id);
    modal.classList.remove('abrir');
}

function abrirFecharModalLoading(isAbrir, id){
    var modal = document.getElementById(id);
    if(isAbrir){
        modal.classList.add('abrir');
    }else{
        modal.classList.remove('abrir');
    }
}