using Microsoft.EntityFrameworkCore;

namespace BookAuthors.Infrastructure
{
    //The "Applier" Class
    //This is an extension method that takes your QueryOptions and applies them to an actual database query:

    // Without these helpers:
    //var query = db.Books.Include(b => b.Author).Where(b => b.Price > 10).OrderBy(b => b.Title);

    // With these helpers:
    //    var options = new QueryOptions<Book>();
    //    options.AddInclude(b => b.Author);
    //    options.Where = b => b.Price > 10;
    //    options.OrderBy = b => b.Title;

    //    var query = db.Books.ApplyOptions(options);


    //Why Use This Pattern?
    //1.	Reusability — Pass the same options object to different methods
    //2.    Flexibility — Only set the options you need; others are ignored
    //3.	Cleaner code — Keeps query logic organized in one place
    //This is a common pattern in repositories or data access layers to avoid repeating query logic throughout your Razor Pages.
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyOptions<T>(this IQueryable<T> query, QueryOptions<T> options) where T : class
        {
            if (options.HasIncludes)
            {
                foreach (var include in options.Includes)
                {
                    query = query.Include(include);
                }
            }

            if (options.HasWhere)
            {
                query = query.Where(options.Where!);
            }

            if (options.HasOrderBy)
            {
                if (options.OrderBy != null)
                {
                    query = query.OrderBy(options.OrderBy);
                }
                else if (options.OrderByDescending != null)
                {
                    query = query.OrderByDescending(options.OrderByDescending);
                }
            }

            return query;
        }
    }
}
