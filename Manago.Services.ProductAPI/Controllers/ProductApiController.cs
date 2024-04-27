using AutoMapper;
using Mango.Services.ProductApi;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public ProductApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public object Get()
        {

            try
            {

                IEnumerable<Product> products = _db.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public object Get(int id)
        {

            try
            {

                Product Product = _db.Products.First(u => u.ProductId == id);
                _responseDto.Result = _mapper.Map<ProductDto>(Product);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }





        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public object Post([FromBody] ProductDto ProductDto)
        {

            try
            {
                Product Product = _mapper.Map<Product>(ProductDto);
                _db.Products.Add(Product);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<ProductDto>(Product);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public object Put([FromBody] ProductDto ProductDto)
        {

            try
            {
                Product Product = _mapper.Map<Product>(ProductDto);
                _db.Products.Update(Product);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<ProductDto>(Product);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public object Delete(int id)
        {

            try
            {
                Product Product = _db.Products.First(u => u.ProductId == id);
                _db.Products.Remove(Product);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }
    }
}
