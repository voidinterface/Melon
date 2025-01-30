using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Data;
using Melon.ViewModels;

namespace Melon.Factories
{
    public class LibraryFactory(Func<LibraryViewModel> factory)
    {
        public LibraryViewModel GetLibraryViewModel(Action<LibraryViewModel> initializer)
        {
            LibraryViewModel instance = factory();
            initializer(instance);
            return instance;
        }
    }
}
