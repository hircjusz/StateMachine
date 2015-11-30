using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServices
{
    public interface IViewStateConfiguration
    {

        Dictionary<string, object> ViewStates { get; set; }
        string[] ViewStateList { get; set; }
        string DefaultViewState { get; set; }
    }
}
