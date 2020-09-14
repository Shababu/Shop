using System;
using Shop.Infrastructure.Commands.Base;

namespace Shop.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        public LambdaCommand(Action<object> Ececute, Func<object, bool> CanExecute = null)
        {
            if(_Execute != null)
            {
                _Execute = Execute;
            }
            else
            {
                throw new ArgumentNullException(nameof(Execute));
            }
            _CanExecute = CanExecute;
        }
        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;
        public override void Execute(object parameter) => _Execute?.Invoke(parameter);
    }
}
