using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



namespace MyNewAspNetApp.Pages;

public class IndexModel : PageModel
    {
        private readonly TodoDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(TodoDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string NewTodo { get; set; }

        public List<TodoItem> TodoList { get; set; }

        public void OnGet()
        {
            string userId = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            TodoList = _context.TodoItems.Where(t => t.UserId == userId).ToList();
        }

        public async Task<IActionResult> OnPostAddTodoAsync()
        {
            string userId = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        
            var todo = new TodoItem { Title = NewTodo, IsDone = false, UserId = userId };
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostToggleTodoAsync(int id)
        {
            string userId = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var todo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo != null)
            {
                todo.IsDone = !todo.IsDone;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteTodoAsync(int id)
        {
            string userId = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var todo = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (todo != null)
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

    }