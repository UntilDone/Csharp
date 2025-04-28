namespace WebPractice.Models
{
    public class User
    {
        public int? Id { get; set; } // 기본키
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; } // 해싱된 비밀번호 저장
    }
}
