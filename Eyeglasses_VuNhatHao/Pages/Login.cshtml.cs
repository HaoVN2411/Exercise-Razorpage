using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace Eyeglasses_VuNhatHao.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public RequestLoginModel RequestLoginModel { get; set; }
        public LoginModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var existLoginAccount = _unitOfWork.StoreAccountRepository.Get
                (x => x.EmailAddress.Equals(RequestLoginModel.EmailAddress) 
                && x.AccountPassword.Equals(RequestLoginModel.Password)
                && (x.Role == 1 || x.Role == 2)).FirstOrDefault();
            if (existLoginAccount != null)
            {
                HttpContext.Session.SetString("Email", existLoginAccount.EmailAddress);
                HttpContext.Session.SetString("Role", existLoginAccount.Role.ToString());
                return RedirectToPage("/EyeGlass");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You do not have permission to do this function!");
                return Page();
            }
        }

    }
}
