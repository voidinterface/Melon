using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Data;
using Melon.ViewModels;

namespace Melon.Factories
{
    public class PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
    {
        public PageViewModel GetPageViewModel(ApplicationPageNames pageName)
        {
            return factory(pageName);
        }
    }
}
