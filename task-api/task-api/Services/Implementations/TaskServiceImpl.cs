using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_api.Entities;
using task_api.Models;

namespace task_api.Services.Implementations
{
    public class TaskServiceImpl : ITaskService{

        private readonly IMongoCollection<TaskDetail> _taskDetail;
        private readonly ILogger _log;
        private string ABERTA = "ABERTA";
        private string ENCERRADA = "ENCERRADA";
        public TaskServiceImpl(IConfiguration config, ILogger<TaskServiceImpl> logger)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("TaskDB"));
            IMongoDatabase database = client.GetDatabase("trincadb");
            _taskDetail = database.GetCollection<TaskDetail>("TaskDetail");
            _log = logger;
        }

        public TaskDetail Create(TaskDetail task)
        {
            task.StatusTask = ABERTA;
            try
            {
                _taskDetail.InsertOne(task);
            }
            catch (Exception ex)
            {
                _log.LogInformation("Create - Falha ao inserir registro: " + ex);
            }
            return task;
        }

        public List<TaskDetail> findTaskDuplicada(string nomeTask, string descricaoTask) {

            List<TaskDetail> retorno = null;
            try
            {
                retorno = _taskDetail.Find(task => task.NomeTask == nomeTask && 
                                                   task.DescricaoTask == descricaoTask &&
                                                   task.StatusTask == ABERTA).ToList();
            }
            catch (Exception ex)
            {
                _log.LogInformation("findTaskDuplicada - Falha ao executar a busca: " + ex);
            }
            return retorno;

        } 

        public void Delete(string id)
        {
            try
            {
                _taskDetail.DeleteOne(task => task.Id == id);
            }
            catch (Exception ex)
            {
                _log.LogInformation("Delete - Falha ao excluir registro id: " + id + " erro: " + ex);
            }
        }

        public List<TaskDetail> FindAll()
        {
            List<TaskDetail> retorno = null;
            try
            {
                retorno = _taskDetail.Find(task => true).ToList();
            }
            catch (Exception ex) {
                _log.LogInformation("FindAll - Falha ao executar a busca: " + ex);
            }
            return retorno;
        }

        public TaskDetail FindById(string id)
        {
            TaskDetail retorno = null;
            try
            {
                retorno = _taskDetail.Find<TaskDetail>(task => task.Id == id).SingleOrDefault();
            } catch (Exception ex) {
                _log.LogInformation("FindById - Falha ao executar a busca: " + ex);
            }
            return retorno;
        }

        public List<TaskDetail> FindNameTaskLike(string nameTask)
        {
            _log.LogInformation("Nome da task: " + nameTask);
            List<TaskDetail> retorno = null;
            try
            {
                retorno = _taskDetail.Find(task => task.NomeTask.Contains(nameTask)).ToList();
            }
            catch (Exception ex)
            {
                _log.LogInformation("findNameTaskLike - Falha ao executar a busca: " + ex);
            }
            return retorno;
        }

        public void Update(string id, TaskDetail obj)
        {
            obj.StatusTask = ENCERRADA;
            obj.Id = id;
            try
            {
                _taskDetail.ReplaceOne(task => task.Id == id, obj);
            }
            catch (Exception ex)
            {
                _log.LogInformation("Update - Falha ao executar update: " + ex);
            }
        }
    }
}
