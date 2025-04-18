using System;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Storecontext(DbContextOptions options) : DbContext(options)
{
    public DbSet <Product> Products { get; set; }

    public static implicit operator ControllerContext(Storecontext v)
    {
        throw new NotImplementedException();
    }
}
