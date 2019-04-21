using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using task_api.Entities;
using task_api.Models;
using task_api.Services;

namespace task_api.controllers
{
    //[Route("/api/[controller]")]
    [Route("/api/v1/tasks")]
    [ApiController]
    public class TaskController : ControllerBase{

        private ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult GetAll(){
            return Ok(_service.FindAll());
        }

        // GET: api/tasks/5
        [HttpGet("{id}", Name = "GetTask")]
        public ActionResult GetById(string id){
            return Ok(_service.FindById(id));
        }

        // GET: api/tasks/name/te
        [HttpGet("name/{nameTask}", Name = "GetNameTaskLike")]
        public ActionResult GetNameTaskLike(string nameTask)
        {
            return Ok(_service.FindNameTaskLike(nameTask));
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<TaskDetail> PostTask(TaskDetail task){
            _service.Create(task);
            return CreatedAtRoute("GetTask", new { id = task.Id.ToString() }, task);
        }

        // PUT: api/tasks/5
        [HttpPut("{id:length(24)}")]
        public IActionResult PutTask(string id)
        {
            var obj = _service.FindById(id);

            if (obj == null)
            {
                return NotFound();
            }

            _service.Update(id, obj);

            return NoContent();
        }

        // PUT: api/tasks/5
        [HttpPut("done/{id:length(24)}")]
        public IActionResult PutMarkDoneTask(string id)
        {
            var obj = _service.FindById(id);

            if (obj == null)
            {
                return NotFound();
            }

            _service.Update(id, obj);

            return NoContent();
        }

        // PUT: api/tasks/5
        [HttpPut("{id:length(24)}")]
        public IActionResult PutTask(string id, TaskDetail task){
            var obj = _service.FindById(id);

            if (obj == null)
            {
                return NotFound();
            }

            _service.Update(id, task);

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteTask(string id){

            var obj = _service.FindById(id);

            if (obj == null) {
                return NotFound();
            }

            _service.Delete(id);
            return NoContent();
        }
    }
}
