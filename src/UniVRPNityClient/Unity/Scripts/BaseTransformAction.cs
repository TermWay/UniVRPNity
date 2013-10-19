using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    /// <summary>
    /// Define transform action.
    /// </summary>
    /// <typeparam name="Action">Type of action. Translate, Rotate, Scale.</typeparam>
    public abstract class BaseTransformAction : BaseAction
    {
        public Transform Transform;
        public UnityEngine.Vector3 sensibility = new UnityEngine.Vector3(5F, 5F, 5F);
    }

    /// <summary>
    /// All movement action possible. Need to be call as public attribute.
    /// </summary>
    public enum Translate
    {
        Forward,    // z++
        Backward,   // z--
        Left,       // x--
        Right,      // x++
        Up,         // y++
        Bottom      // y--
    }

    public enum Scale
    {
        Elongate,   // z++
        Shorten,    // z--
        Raise,      // y++
        Flatten,    // y--
        Enlarge,    // x++ 
        Contract    // x-- 
    }

    public enum Rotate
    {
        YawLeft,    // y--
        YawRight,   // y++
        PitchUp,    // x--
        PitchBottom,// x++
        RollLeft,   // z++
        RollRight   // z--
    }

}
