using TaskManagement.Data.Entities;
using TaskManagement.Data.Models;
using TaskManagement.Helpers;
using TaskManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TaskManagement.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]

    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<ActionResult<TaskEntity>> AddNewTask([FromBody] CreateTaskRequest model)
        {
            try
            {
                TaskEntity u = await _taskService.AddNew(model);
                return Ok(u);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetAll()
        {
            try
            {
                List<TaskEntity> tasks = await _taskService.GetAll();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> GetById(int id)
        {
            try
            {
                TaskEntity u = await _taskService.GetById(id);
                return Ok(u);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskEntity>> Update(int id, [FromBody] UpdateTaskRequest model)
        {
            try
            {
                TaskEntity u = await _taskService.Update(id, model);
                return Ok(u);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _taskService.Delete(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
