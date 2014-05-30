using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MDT.ManageCenter.ServiceContract
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IShortMessageService
    {
        [OperationContract]
        void Send(string toAddress, string message);
    }
}
