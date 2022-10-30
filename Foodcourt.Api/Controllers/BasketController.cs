﻿using System.Net.Mime;
using Foodcourt.BusinessLogic.Services.Basket;
using Foodcourt.Data.Api.Request;
using Foodcourt.Data.Api.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Foodcourt.Api.Controllers
{
    [Authorize(Roles = "user")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("v1.0/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<IdentityUser> _userManager;
        
        public BasketController(IBasketService basketService, UserManager<IdentityUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBasket()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return BadRequest("User does not have ID");
            
            var response = await _basketService.GetBasket(userId);
            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<ActionResult> CleanBasket()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return BadRequest("User does not have ID");
            
            await _basketService.CleanBasket(userId);
            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult> AddProduct([FromBody] AddProductRequest addAddProductRequest)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return BadRequest("User does not have ID");
            
            var response = await _basketService.AddProduct(userId, addAddProductRequest);
            return Created("basket", response);
        }
        
        [HttpPatch("{productId:long}")]
        public async Task<ActionResult> PatchBasketProduct([FromBody] PatchProductRequest patchProductRequest, long productId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return BadRequest("User does not have ID");
            
            await _basketService.PatchProduct(userId, productId, patchProductRequest);
            return Ok();
        }
        
        [HttpDelete("{productId:long}")]
        public async Task<ActionResult> DeleteBasketProduct([FromQuery] PatchProductRequest patchProductRequest, long productId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return BadRequest("User does not have ID");
            
            await _basketService.DeleteProduct(userId, productId);
            return Ok();
        }
    }
}