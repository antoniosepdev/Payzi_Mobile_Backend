using LinqKit;
using Mapster;
using System.Data.Entity;

namespace Payzi.Abstraction.PartialOverload
{
    public static class Convert
    {
        public static async Task<List<T>> ToList<T>(this IQueryable<object> query)
        {
            var queryList = await query.AsExpandable().ToListAsync();

            List<T> list = queryList.Adapt<List<T>>();

            return list;
        }

        public static T SingleOrDefault<T>(this object item)
        {
            T result = item.Adapt<T>();

            return result;
        }
    }
}
