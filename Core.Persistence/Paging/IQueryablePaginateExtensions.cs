using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Paging;

public static class IQueryablePaginateExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        List<T> items = await  source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);
        
        Paginate<T> List = new()
        {
            Index = index,
            Size = size,
            Count = count,
            Pages = (int)Math.Ceiling((double)count / size),
            Items = items
        };

        return List;
    }

    public static Paginate<T> ToPaginate<T>(
        this IQueryable<T> source,
        int index,
        int size)
    {
        int count =  source.Count();
        var items =  source.Skip(index * size).Take(size).ToList();

        Paginate<T> List = new()
        {
            Index = index,
            Size = size,
            Count = count,
            Pages = (int)Math.Ceiling((double)count / size),
            Items = items
        };

        return List;
    }
}
