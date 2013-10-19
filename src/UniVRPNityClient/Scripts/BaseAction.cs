using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    /// <summary>
    /// Define action as list of boolean. True if action is on going, false else.
    /// An action enum define the index of the action.
    /// </summary>
    /// <typeparam name="Action"></typeparam>
    public abstract class BaseAction<Action> 
    {
        public List<bool> actions = new List<bool>(new bool[Enum.GetValues(typeof(Action)).Length]);
 
        public abstract void Update();
    }
}
