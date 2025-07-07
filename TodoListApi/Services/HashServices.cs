using BCrypt.Net;

namespace TodoListApi.Services
{
    public class HashServices
    {
        public string HashPassword(string password)=>
            BCrypt.Net.BCrypt.HashPassword(password);
        public bool verifyPassword(string password, string hashedPassword)=>
            BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}