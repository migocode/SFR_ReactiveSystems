using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SFR_ReactiveSystems.TransactionService
{
    public static class AsyncExtensions
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static CancellationTokenAwaiter GetAwaiter(this CancellationToken cancellationToken) =>
            new CancellationTokenAwaiter(cancellationToken);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public struct CancellationTokenAwaiter : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly CancellationToken cancellationToken;
            public bool IsCompleted => cancellationToken.IsCancellationRequested;

            public CancellationTokenAwaiter(CancellationToken cancellationToken)
            {
                this.cancellationToken = cancellationToken;
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public object GetResult()
            {
                if (IsCompleted)
                {
                    throw new OperationCanceledException();
                }
                else
                {
                    throw new InvalidOperationException("The cancellation token has not yet been cancelled.");
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            public void OnCompleted(Action continuation) =>
                cancellationToken.Register(continuation);

            [EditorBrowsable(EditorBrowsableState.Never)]
            public void UnsafeOnCompleted(Action continuation) =>
                cancellationToken.Register(continuation);
        }
    }
}
