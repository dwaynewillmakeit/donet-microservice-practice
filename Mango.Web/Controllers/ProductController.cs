﻿using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {


                ResponseDto? response = await _productService.CreateProductAsync(productDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created";

                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }

            return View(productDto);
        }
        
        public async Task<IActionResult> ProductDelete(int productId)
        {

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(productDto);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

        public async Task<IActionResult> ProductEdit(int productId) {

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response == null) {
            
                return NotFound();
            }

            ProductDto? productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(productDto);
        }

		[HttpPost]
		public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {

            ResponseDto? response = await _productService.UpdateProductAsync(productDto);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));

            }
            else
            {
                TempData["error"] = response?.Message;
            }

			return View(productDto);

		}

        
		[HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {

            ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted";

                return RedirectToAction("ProductIndex");
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }
    }
}
