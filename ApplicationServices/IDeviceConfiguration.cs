using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServices
{
    public interface IDeviceConfiguration
    {
        Dictionary<string, object> Devices { get; set; }
    }
}
