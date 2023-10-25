using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using ToDoDioDesafio.Context;

namespace ToDoDioDesafio.Controllers
{
    [Route("Api/v1/[controller]/[action]")]
    public class TodoController : ControllerBase
    {
        private readonly ToDoDbContext _dbContext;
        public TodoController(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<Todo> Create([FromBody] Todo todo)
        {
            if (todo == null)
            {
                throw new Exception("Precisar passar um todo");
            }

            await _dbContext.Set<Todo>().AddAsync(todo);
            await _dbContext.SaveChangesAsync();
            return todo;
        }

        [HttpGet]
        public  async Task<IEnumerable<Todo>> GetAll()
        {
            var entities =  await _dbContext.Set<Todo>().ToListAsync();
            return entities;
        }

        [HttpGet]
        public async Task<Todo> GetById(int id)
        {
            if (id == null)
            {
                throw new Exception("O id não pode ser nulo");
            }
            return await _dbContext.Set<Todo>().FindAsync(id);
        }
        [HttpGet]
        public async Task<IEnumerable<Todo>> GetByTitulo(string titulo)
        {
            if (titulo == null)
            {
                throw new Exception("O Titulo não pode ser nulo");
            }
            var entities = await _dbContext.Set<Todo>().Where(todo => todo.Titulo == titulo).ToListAsync();
            return entities;
        }
        [HttpGet]
        public async Task<IEnumerable<Todo>> GetByData(DateTime data)
        {
            if (data == null)
            {
                throw new Exception("A data não pode ser nula");
            }
            var dateWithoutTime = data.Date;
            var entities = await _dbContext.Set<Todo>().Where(todo => todo.Data.Date == dateWithoutTime).ToListAsync();
            return entities;
        }

        [HttpGet]
        public async Task<IEnumerable<Todo>> GetByStatus(Status status)
        {
            if (status == null)
            {
                throw new Exception("A data não pode ser nula");
            }
            var entities = await _dbContext.Set<Todo>().Where(todo => todo.Status == status).ToListAsync();
            return entities;
        }

        [HttpPut]
        public async Task<Todo> Update([FromBody]Todo todo)
        {
            _dbContext.Set<Todo>().Update(todo);
            await _dbContext.SaveChangesAsync();
            return todo;
            
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            var entity = await _dbContext.Set<Todo>().FindAsync(id);
            _dbContext.Set<Todo>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

    }
}
