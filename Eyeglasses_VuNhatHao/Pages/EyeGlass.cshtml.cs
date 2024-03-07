using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Entites;

namespace Eyeglasses_VuNhatHao.Pages
{
    public class EyeGlassModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public List<Eyeglass> eyeglasses = new List<Eyeglass>();
        [BindProperty]
        public int pageIndex { get; set; } = 1;
        public int totalPage { get; set; } = 0;
        [BindProperty]
        public int Price { get; set; }
        [BindProperty]
        public string EyeglassesDescription { get; set; }
        [BindProperty]
        public int IdToDelete { get; set; }
        [BindProperty]
        public int IdToUpdate { get; set; }

        public EyeGlassModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            eyeglasses = _unitOfWork.EyeGlassRepository.Get(includeProperties: "LensType", pageIndex: pageIndex, pageSize: 4).ToList();
            totalPage = (int)Math.Ceiling((double)_unitOfWork.EyeGlassRepository.Count() / 4);
        }
        public IActionResult OnPostGetList()
        {
            eyeglasses = _unitOfWork.EyeGlassRepository.Get(includeProperties: "LensType", pageIndex: pageIndex, pageSize: 4).ToList();
            totalPage = (int)Math.Ceiling((double)_unitOfWork.EyeGlassRepository.Count() / 4);
            return Page();
        }

        public IActionResult OnPostSearch()
        {
            eyeglasses = _unitOfWork.EyeGlassRepository.Get(x =>
            (Price == null || x.Price == Price)
            || (string.IsNullOrEmpty(EyeglassesDescription) || x.EyeglassesDescription.Contains(EyeglassesDescription)),
            includeProperties: "LensType").ToList();
            return Page();
        }

        public void OnPostDelete()
        {
            var eyeGlassToDelete = _unitOfWork.EyeGlassRepository.GetByID(IdToDelete);
            if (eyeGlassToDelete != null)
            {
                _unitOfWork.EyeGlassRepository.Delete(eyeGlassToDelete);

                _unitOfWork.Save();
            }
            OnGet();
        }

        public IActionResult OnPostCreate()
        {
            return Redirect("/CreateEyeGlass");
        }
        public IActionResult OnPostUpdate()
        {
            HttpContext.Session.SetString("IdToUpdate", IdToUpdate.ToString());
            return Redirect("/UpdateEyeGlass");
        }


    }
}
