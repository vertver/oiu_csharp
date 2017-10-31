using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChildWindowsDemo.SupportClass
{
    class SimpleCommand : ICommand
    {
        private Action _action;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var handler = _action;
            if (handler != null)
            {
                handler();
            }
        }

        public SimpleCommand(Action action)
        {
            _action = action;
        }
    }
}
