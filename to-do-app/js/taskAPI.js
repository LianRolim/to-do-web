//carrega ao iniciar a pagina os dados.
$(document).ready(function(){
    abrirFecharModalLoading(true, 'modal-loading'); //abre loading
    $.ajax({ url: "https://localhost:5001/api/v1/tasks",
             type: "GET"
           })
           .done(function( retorno ) {
                retorno.forEach(function(obj){
                    switch (obj.statusTask) {
                       case "ABERTA":
                         manipulaTasksToDo(obj);
                         break;
                       case "ENCERRADA":
                         manipulaTasksDone(obj);
                         break;
                       default:
                        break;
                    }
                abrirFecharModalLoading(false, 'modal-loading'); // fecha loading
                });
            abrirFecharModalLoading(false, 'modal-loading'); // fecha loading
           });
});

//se usuario pressionar enter no input de busca, chama operacao para fazer like
$(function(){
    $("#top-input").keyup(function (e) {
        if(!$(this).val()){
           findAllTasks();
        }
        if (e.which == 13) {
            abrirFecharModalLoading(true, 'modal-loading'); //abre loading
            var nameTask = document.querySelector('#top-input').value;            
            $.ajax({ url: "https://localhost:5001/api/v1/tasks/name/"+nameTask.toLowerCase(),
             type: "GET"
               })
               .done(function( retorno ) {
                if(retorno.length > 0){
                    var listToDo = document.querySelector('#list-task-to-do');
                    var listDone = document.querySelector('#list-task-done');
                    listToDo.innerHTML = "";
                    listDone.innerHTML = "";
                    retorno.forEach(function(obj){
                        switch (obj.statusTask) {
                          case "ABERTA":
                            manipulaTasksToDo(obj);
                            break;
                          case "ENCERRADA":
                            manipulaTasksDone(obj);
                            break;
                          default:
                            break;
                        }
                    abrirFecharModalLoading(false, 'modal-loading'); // fecha loading
                    });
                }else{
                    var listToDo = document.querySelector('#list-task-to-do');
                    var listDone = document.querySelector('#list-task-done');
                    listToDo.innerHTML = "";
                    listDone.innerHTML = "";
                    abrirFecharModalLoading(false, 'modal-loading'); // fecha loading
                }
                
            });
        }
     });
});

function findAllTasks(){
    abrirFecharModalLoading(true, 'modal-loading');
    $.ajax({ url: "https://localhost:5001/api/v1/tasks",
             type: "GET"
           })
           .done(function( retorno ) {
                retorno.forEach(function(obj){
                    switch (obj.statusTask) {
                       case "ABERTA":
                         manipulaTasksToDo(obj);
                         break;
                       case "ENCERRADA":
                         manipulaTasksDone(obj);
                         break;
                       default:
                        break;
                    }
                abrirFecharModalLoading(false, 'modal-loading');
                });
           });
}

//manipula o dom da sessao de tasks-to-do
function manipulaTasksToDo(valor) { 
    var tasksToDo = document.querySelector('#list-task-to-do');
    var article = '';
    var article = article + '<article onclick="loadTaskDtlById(\'' + valor.id + '\')" class="detalhes-task-ativa"> ';
    var article = article + '   <h3 class="task-name">' + valor.nomeTask + '</h3> ';
    var article = article + '      <p class="task-detalhe-ativa-done">' + valor.descricaoTask + '</p> ';
    var article = article + '</article>';
    
    tasksToDo.innerHTML += article;
    
}

//manipula o dom da sessao de tasks-done
function manipulaTasksDone(valor) { 
    var tasksDone = document.querySelector('#list-task-done');
    var article = '';
    var article = article + '<article class="detalhes-task-ativa"> ';
    var article = article + '   <h3 class="task-name"><strike>' + valor.nomeTask + '</strike></h3> ';
    var article = article + '      <p class="task-detalhe-ativa-done"><strike>' + valor.descricaoTask + '</strike></p> ';
    var article = article + '</article>';
    
    tasksDone.innerHTML += article;
    
}

// realiza a busca por um id especifico
function loadTaskDtlById(id){
    abrirFecharModalLoading(true, 'modal-loading');
    $.ajax({ url: "https://localhost:5001/api/v1/tasks/"+id,
             type: "GET"
           })
           .done(function( retorno ) {
               
                var taskDetail = document.querySelector('#carrega-detail');
                var article = '';
                var article = article + '<article class="edit-task-ativa"> ';
                var article = article + '   <h3 class="task-name-edit">'+ retorno.nomeTask +'</h3> ';
                var article = article + '      <p class="task-detalhe-edit">'+ retorno.descricaoTask +'</p> ';
                var article = article + '      <a class="btn-delete-task" href="#" onclick="deleteTaskById(\'' + retorno.id + '\')">Delete task</a>';
                var article = article + '      <a class="btn-delete-task" href="#" onclick="markDoneTask(\'' + retorno.id + '\')">Done</a>';
                var article = article + '</article>';
                taskDetail.innerHTML = article;
                
                abrirFecharModalLoading(false, 'modal-loading');
           });
}

//realiza o delete da task fisicamente
function deleteTaskById(id){
    abrirFecharModalLoading(true, 'modal-loading');
    $.ajax({ url: "https://localhost:5001/api/v1/tasks/"+id,
             type: "DELETE"
           })
           .done(function( retorno ) {
            abrirFecharModalLoading(false, 'modal-loading');
            location.reload();
    });
}

//marca a task como done na base
function markDoneTask(id){
    abrirFecharModalLoading(true, 'modal-loading');
    $.ajax({ url: "https://localhost:5001/api/v1/tasks/done/"+id,
             type: "PUT"
           })
           .done(function( retorno ) {
            abrirFecharModalLoading(false, 'modal-loading');
            location.reload();
    });
}

// post para insercao de uma nova task na base
function addNewTask(nomeTask, descricaoTask){
    
    var insercao = validaInsercao(nomeTask, descricaoTask);
    
    if(insercao){
        abrirFecharModalLoading(true, 'modal-loading');
        $.ajax({
        url: 'https://localhost:5001/api/v1/tasks',
        dataType: 'JSON',
        type: 'POST',
        contentType: 'APPLICATION/JSON',
        data: JSON.stringify( { "nomeTask": nomeTask, "descricaoTask": descricaoTask } ),
        processData: false,
        success: function( data, textStatus, jQxhr ){
            abrirFecharModalLoading(true, 'modal-loading');
            location.reload();
        },
        error: function( jqXhr, textStatus, errorThrown ){
            console.log( errorThrown );
            abrirFecharModalLoading(true, 'modal-loading');
            location.reload();
        }
        });
    }    
}

// nem nome e nem descricao da task podem ser empty or null, sao obrigatorios..
function validaInsercao(nome, desc){
    retorno = true;
    
    if(!nome){
        retorno = false;
        alert('Você deve informar um nome');
    }else if(!desc){
        retorno = false;
        alert('Você deve informar uma descrição');
    }
    
    return retorno;
}