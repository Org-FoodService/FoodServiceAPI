using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace FoodServiceApi.Tests.Utility
{
    [ExcludeFromCodeCoverage]
    public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);

        public IQueryable<T> CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<T>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return CompileExpression<T>(expression)!;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return CompileExpression<TResult>(expression);
        }

        private TResult CompileExpression<TResult>(Expression expression)
        {
            var visitor = new TestAsyncQueryProvider<T>(this);
            return visitor.Execute<TResult>(expression);
        }
    }
}
