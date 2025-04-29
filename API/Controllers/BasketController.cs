using System;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class BasketController(Storecontext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var basket = await RetrieveBasket();
        if (basket == null) return NoContent();

        return basket.ToDto();
    }
    [HttpPost]
    public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
    {
        var basket = await RetrieveBasket(); //get basket
   
        basket ??= CreateBasket(); //create basket

        var product = await context.Products.FindAsync(productId); //get product

        if (product == null) return BadRequest("Problem adding item to basket");

        basket.AddItem(product, quantity); //add item to basket

        var result = await context.SaveChangesAsync() > 0;

        if (result) return CreatedAtAction(nameof(GetBasket), basket.ToDto());
        //save changes
        return BadRequest("Problem updating basket");
    }



    [HttpDelete]

    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
    {
        var basket = await RetrieveBasket();      //get basket

        if (basket == null) return BadRequest("Unable to retrieve basket");//remove the item
        basket.RemoveItem(productId, quantity);

        var result = await context.SaveChangesAsync() > 0;         //save changes


        if (result) return Ok();

        return BadRequest("Problem updating basket");

    }

    private Basket CreateBasket()
    {

        var baseketId = Guid.NewGuid().ToString();
        var cookiesOptions = new CookieOptions
        {
            IsEssential = true,
            Expires = DateTime.UtcNow.AddDays(30)
        };
        Response.Cookies.Append("basketId", baseketId, cookiesOptions);
        var basket = new Basket { BasketId = baseketId };
        context.Baskets.Add(basket);
        return basket;
    }

    private async Task<Basket?> RetrieveBasket()
    {
        return await context.Baskets
        .Include(x => x.Items)
        .ThenInclude(x => x.Product)
        .FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);
    }
}
