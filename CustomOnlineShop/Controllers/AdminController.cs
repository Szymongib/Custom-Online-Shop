using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using ApplicationInfrastructure.Identity;
using CustomOnlineShop.Helpers;
using CustomOnlineShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomOnlineShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        UserManager<AppUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IProductRepository _productRepository;
        IProductCategoryRepository _productCategoryRepository;
        IShippingInfoRepository _shippingInfoRepository;

        #region Constructor

        public AdminController(
            UserManager<AppUser> userManager, 
            IProductRepository productRepository, 
            RoleManager<IdentityRole> roleManager,
            IProductCategoryRepository productCategoryRepository,
            IShippingInfoRepository shippingInfoRepository)
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _roleManager = roleManager;
            _productCategoryRepository = productCategoryRepository;
            _shippingInfoRepository = shippingInfoRepository;
        }

        #endregion


        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Users()
        {
            return View(_userManager.Users);
        }

        public ViewResult Products()
        {
            List<Product> products = _productRepository.ListAll().ToList();
            products.ForEach(x => x.Category = _productCategoryRepository.GetById(x.CategoryId));
            IEnumerable<ProductDisplayViewModel> model = products.Select(x => x.MapToDisplayViewModel());

            return View(model);
        }

        public ViewResult Roles()
        {
            IEnumerable<RoleViewModel> roles = _roleManager.Roles.Select(x => x.MapToViewModel());
            return View(roles);
        }

        [HttpGet]
        public ViewResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name));

                if(result == IdentityResult.Success)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return RedirectToAction("Roles");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _productRepository.Delete(_productRepository.GetById(id));
            return RedirectToAction("Products");
        }

        [HttpGet]
        public ViewResult AddProduct()
        {
            CreateProductViewModel model = new CreateProductViewModel();
            model.Categories = _productCategoryRepository.ListAll().Select(x => x.MapToViewModel());

            return View("EditProduct", model);
        }

        [HttpGet]
        public ViewResult EditProduct(int id)
        {
            CreateProductViewModel model = _productRepository.GetById(id).MapToCreateViewModel();
            model.Categories = _productCategoryRepository.ListAll().Select(x => x.MapToViewModel());

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Price = model.Price.Replace('.', ',');
                decimal price;
                if(decimal.TryParse(model.Price, out price))
                {
                    SaveProduct(model, price);
                    
                    return RedirectToAction("Products");
                }
                else
                {
                    ModelState.AddModelError("Invalid Price", "Price is invalid.");
                }
            }

            model.Categories = _productCategoryRepository.ListAll().Select(x => x.MapToViewModel());

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error Deleting", "Can not delete user");
            }
            _shippingInfoRepository.DeleteAllByUserId(id);

            return RedirectToAction("Users");
        }





        private Product SaveProduct(CreateProductViewModel model, decimal price)
        {
            Product product = _productRepository.GetById(model.Id) ?? new Product();

            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.Price = price;
            product.FullDescription = model.FullDescription;
            product.ShortDescription = model.ShortDescription;
            product.ImgPath = model.ImgPath != null ? Constants.ProductImagesPath + model.ImgPath : null;

            //If new product
            if (model.Id <= 0)
            {
                _productRepository.Add(product);
            }
            else
            {
                _productRepository.Update(product);
            }

            return product;
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
