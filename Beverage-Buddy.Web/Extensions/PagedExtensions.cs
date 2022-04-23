using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beverage_Buddy.Web.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Beverage_Buddy.Web.Extensions
{
    public static class PagedExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T> {CurrentPage = page, PageSize = pageSize, RowCount = query.Count()};

            var pageCount = (double) result.RowCount / pageSize;
            result.PageCount = (int) Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
