using System;
using Microsoft.EntityFrameworkCore;

namespace API.RequestHelpers;

public class PagedList<T> : List<T>
{
   public PagedList(List<T> items, int count , int PageNumber, int PageSize)
   {
      Metadata = new PaginationMetadata
      {
         TotalCount = count,
         PageSize = PageSize,
         CurrentPage = PageNumber,
         TotalPages = (int)Math.Ceiling(count/(double)PageSize)
      };
      AddRange(items);
   }
   public PaginationMetadata Metadata { get; set; }

   public static async Task<PagedList<T>>ToPagedList(IQueryable<T> query,
    int PageNumber, int PageSize)
     
     {
          var count = await query.CountAsync();
          var items = await query.Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();
          return new PagedList<T>(items, count,PageNumber,PageSize);

     }
}
