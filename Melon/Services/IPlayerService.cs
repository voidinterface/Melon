using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melon.Services
{
    public interface IPlayerService
    {
        public void playSong(string path);
        public void stop();
    }
}
