using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MDT.Utility
{
    public class WCFServiceFactory
    {
        public static T GetInstance<T>() where T : ICommunicationObject, new()
        {
            return new T();
        }
    }
}
