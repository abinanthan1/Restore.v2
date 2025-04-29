using System;
using API.DTOs;
using API.Entities;

namespace API.Extensions;

public static class BasketExtensions
{
    public static BasketDto ToDto(this Basket basket)
    {
        return new BasketDto
        {
            BasketId = basket.BasketId,
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
}
