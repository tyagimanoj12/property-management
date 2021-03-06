using System;
using System.Collections.Generic;
using System.Text;

namespace MyProperty.Services.Interfaces
{
    public interface ISmsService
    {
        void SendTextMessage(string mobileNumbers, string messageText);
    }
}
