using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly TodosAPIDbContext dbContext;

        public TodosController(TodosAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await dbContext.Todos.ToListAsync());
            // return View();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        {
            var contact = await dbContext.Todos.FindAsync(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest addContactRequest)
        {
            var todo = new Todo()
            {
                Id = Guid.NewGuid(),
                Title = addContactRequest.Title,
                Description = addContactRequest.Description,
                isComplete = false
            };

            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();
            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, UpdateTodoRequest updateContactRequest)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo != null)
            {
                todo.Title = updateContactRequest.Title;
                todo.Description = updateContactRequest.Description;
                todo.isComplete = updateContactRequest.isComplete;

                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);
            if (todo != null)
            {
                dbContext.Remove(todo);
                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
