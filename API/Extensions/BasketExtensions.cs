using System;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class BasketExtensions
{
    public static BasketDto ToDto(this Basket basket)
    {
        return new BasketDto
        {
            BasketId = basket.BasketId,
            ClientSecret = basket.ClientSecret,
            PaymentIntentId = basket.PaymentIntentId,
            Items=basket.Items.Select(x=>new BasketItemDto
            {
              productId = x.ProductId,
              Name = x.Product.Name, 
              price = x.Product.Price,
              Brand = x.Product.Brand,
              Type = x.Product.Type,
              PictureUrl = x.Product.pictureUrl,
              Quantity = x.Quantity

            }) .ToList()
        }; 
    } // basket.ToDto
   
   public static async Task<Basket>GetBasketWithItems(this IQueryable<Basket> query,
   string?basketId)
   {
     return await query
        .Include(x => x.Items)
        .ThenInclude(x => x.Product)
        .FirstOrDefaultAsync(x => x.BasketId == basketId)??throw new Exception("Cannot get basket");
   }

}
