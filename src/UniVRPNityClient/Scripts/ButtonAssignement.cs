using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UniVRPNity
{
    public class ButtonAssignement<Key, Action, FieldClass>
         where Key : struct, IConvertible
         where Action : struct, IConvertible

    {
        protected Dictionary<Key, FieldInfo> assignements = new Dictionary<Key, FieldInfo>();

        public FieldInfo Field(Action action)
        {
            return Field(action.ToString());
        }

        public FieldInfo Field(string actionName)
        {
            System.Type type = typeof(FieldClass);
            return type.GetField(actionName, BindingFlags.Public | BindingFlags.Instance);
        }

        public Action this[Key key]
        {
            get
            {
                return (Action)Enum.Parse(typeof(Action), this.assignements[key].Name); ;
            }
            set
            {
                this.assignements[key] = Field(value);
            }
        }        

        public void update(Action action, object owner, object newValue)
        {
            Field(action).SetValue(owner, newValue);
        }

        public void Assign(Key assignement, Action move)
        {
            assignements[assignement] = Field(move);
        }
    }
}
