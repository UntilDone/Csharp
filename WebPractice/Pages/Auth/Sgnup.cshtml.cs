using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPractice.Data;
using WebPractice.Models;
using Microsoft.AspNetCore.Identity;

namespace WebPractice.Pages.Auth
{
    public class SignupModel : PageModel
    {
        private readonly AppDbContext _db;

        public SignupModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string? Name { get; set; }
        
        [BindProperty]
        public string? Email { get; set; }
        
        [BindProperty]
        public string? Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 비밀번호 해시
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Name = Name,
                Email = Email
            };
            user.PasswordHash = hasher.HashPassword(user, Password);

            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToPage("/Auth/Login");
        }
    }
}
