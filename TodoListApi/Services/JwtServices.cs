using TodoListApi.Context;

namespace TodoListApi.Services
{
    public class JwtServices
    {
        private readonly TodoListContext _context;
        private readonly IConfiguration _configuration;
        public JwtServices(TodoListContext context, IConfiguration configuration) =>
        (_context, _configuration) = (context, configuration);

    }
}
