using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TIADateiViewer
{
    class DelegateCommand : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        //der erste Contruktor wird aufgerufen
        public DelegateCommand(Action<object> execute) : this(null, execute) { }
       
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChange() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => execute?.Invoke(parameter);
    }
}
