using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Melon.Common.Core
{
    public sealed class Interaction<TInput, TOutput> : IDisposable, ICommand
    {
        private Func<TInput, Task<TOutput>>? _handler;

        public Task<TOutput> HandleAsync(TInput input)
        {
            if (_handler == null)
            {
                throw new InvalidOperationException("Handler is not set.");
            }
            return _handler(input);
        }

        public IDisposable RegisterHandler(Func<TInput, Task<TOutput>> handler)
        {
            if (_handler != null)
            {
                throw new InvalidOperationException("Handler is already set.");
            }

            _handler = handler;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            return this;
        }

        public void Dispose()
        {
            _handler = null;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? paramete) => _handler is not null;

        public void Execute(object? parameter) => HandleAsync((TInput?)parameter!);

        public event EventHandler? CanExecuteChanged;
    }
}
