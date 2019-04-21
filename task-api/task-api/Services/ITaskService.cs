using System.Collections.Generic;
using task_api.Entities;
using task_api.Models;

namespace task_api.Services
{
    public interface ITaskService{
        TaskDetail Create(TaskDetail task);
        List<TaskDetail> FindAll();
        TaskDetail FindById(string id);
        void Update(string id, TaskDetail task);
        void Delete(string id);

        List<TaskDetail> FindNameTaskLike(string nameTask);
    }
}
