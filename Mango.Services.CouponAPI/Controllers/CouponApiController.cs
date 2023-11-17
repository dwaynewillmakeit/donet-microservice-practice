﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public CouponApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public object Get()
        {

            try
            {

                IEnumerable<Coupon> coupons = _db.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);

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
        public object Get(int id)
        {

            try
            {

                Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }




        [HttpGet]
        [Route("{code}")]
        public object GetByCode(string code)
        {

            try
            {

                Coupon coupon = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }

        [HttpPost]
        public object Post([FromBody] CouponDto couponDto)
        {

            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }

        [HttpPut]
        public object Put([FromBody] CouponDto couponDto)
        {

            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(coupon);

            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }

            return _responseDto;
        }
        [HttpDelete]
        public object Delete(int id)
        {

            try
            {
                Coupon coupon = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(coupon);
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
