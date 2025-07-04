namespace TodoListApi.Models.Jwt
{
    public class LoginResponseModel
    {
        public string? UserName { get; set; }
        public string? AccesToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
