using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;

namespace BackEngin.Tests.Helpers
{
    public static class MockHelper
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        public static Mock<IConfiguration> MockConfiguration()
        {
            var mockConfig = new Mock<IConfiguration>();

            // Create a mock section for "JwtSettings"
            var mockJwtSection = new Mock<IConfigurationSection>();
            mockJwtSection.Setup(x => x["Issuer"]).Returns("TestIssuer");
            mockJwtSection.Setup(x => x["Audience"]).Returns("TestAudience");
            mockJwtSection.Setup(x => x["SecretKey"]).Returns("ThisIsASufficientlyLongSecretKey12345");

            // Setup GetSection to return the mocked JwtSettings section
            mockConfig.Setup(x => x.GetSection("JwtSettings")).Returns(mockJwtSection.Object);

            return mockConfig;
        }

    }

    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current => _inner.Current;

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
    }

    public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression) => _inner.CreateQuery(expression);

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
            new TestAsyncEnumerable<TElement>(expression);

        public object Execute(Expression expression) => _inner.Execute(expression);

        public TResult Execute<TResult>(Expression expression) =>
            _inner.Execute<TResult>(expression);

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            // Execute the expression synchronously
            var executionResult = Execute(expression);

            // Check if TResult is a Task
            if (typeof(TResult).IsGenericType && typeof(TResult).GetGenericTypeDefinition() == typeof(Task<>))
            {
                // Get the result type of the Task (e.g., Task<UserDTO> -> UserDTO)
                var resultType = typeof(TResult).GetGenericArguments()[0];

                // Create a Task of the appropriate type
                var taskResult = typeof(Task).GetMethod(nameof(Task.FromResult))
                    .MakeGenericMethod(resultType)
                    .Invoke(null, new[] { executionResult });

                return (TResult)taskResult; // Cast the Task<object> to Task<TResult>
            }

            // If TResult is not a Task, directly return the result
            return (TResult)executionResult;
        }

    }

    public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public TestAsyncEnumerable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
            new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}
