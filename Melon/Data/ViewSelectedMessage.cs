﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Melon.ViewModels;

namespace Melon.Data
{
    public class ViewSelectedMessage(ViewModelBase value) : ValueChangedMessage<ViewModelBase>(value) { }
}
