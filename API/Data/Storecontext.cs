using System;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Storecontext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Basket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
        .HasData(
            new IdentityRole { Id ="7Nx/stZVkU6STLL/F2AjGQ==", Name ="Member", NormalizedName ="MEMBER"},
            new IdentityRole { Id ="rPXWZ5lA80WD9XGZ9oyQHQ==", Name ="Admin", NormalizedName ="ADMIN"}
        );
    }
}