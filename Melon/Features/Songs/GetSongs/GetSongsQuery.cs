using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using MediatR;

namespace Melon.Features.Songs.GetSongs
{
    public class GetSongsQuery : IRequest<List<SongViewModel>>
    {
    }
}
