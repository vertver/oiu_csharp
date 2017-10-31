using ChildWindowsDemo.SupportClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChildWindowsDemo.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public ICommand CreateChildCommand { get; set; }

        public MainViewModel()
        {
            CreateChildCommand = new SimpleCommand(CreateChild);
        }

        private void CreateChild()
        {
            var child = new DemoViewModel()
            {
                Title = "Дочернее окно",
                Date = DateTime.Now
            };
            Show(child);
        }
    }
}
