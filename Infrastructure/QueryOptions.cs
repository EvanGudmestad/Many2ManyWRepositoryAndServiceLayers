using System.Linq.Expressions;

namespace BookAuthors.Infrastructure
{
    //QueryOptions<T> — The "Settings" Class
    //Think of QueryOptions<T> as a configuration object that holds all your query preferences:
    //Property Purpose Example Use
    //Where Filter which records to return	"Only books published after 2020"
    //OrderBy Sort results ascending	"Sort by title A-Z"
    //OrderByDescending Sort results descending	"Sort by price high-to-low"
    //Includes Load related data (navigation properties)   "Include the Author when loading Books"
    //The Has* properties (HasWhere, HasOrderBy, HasIncludes) are convenience checks to see if each option was set.
    public class QueryOptions<T> where T : class
    {
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDescending { get; set; }
        public Expression<Func<T, bool>>? Where { get; set; }

        private List<Expression<Func<T, object>>> _includes = [];
        public List<Expression<Func<T, object>>> Includes
        {
            get => _includes;
            set => _includes = value;
        }

        public void AddInclude(Expression<Func<T, object>> include)
        {
            _includes.Add(include);
        }

        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null || OrderByDescending != null;
        public bool HasIncludes => _includes.Count > 0;
    }
}
