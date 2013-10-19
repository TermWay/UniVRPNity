using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class AnimationAction : BaseAction<Animation>
    {
        public UnityEngine.Animation animation;
      //  public AnimationState animationState;
        public float speed = 10F;

        public override void Update()
        {
           
            AnimationState animationState = animation[animation.clip.name];
            Debug.Log("Name" + animation.clip.name +
                "[" + animationState.time + "/" + animationState.length +
                " << " + actions[(int)Animation.Rewind] + ", " +
                actions[(int)Animation.Forward] + " >> ");
            float step = animationState.length;
            if (actions[(int)Animation.Rewind])
                animationState.time = Math.Min(
                    animationState.time + speed / animationState.length * Time.deltaTime,
                    animationState.length);
            if (actions[(int)Animation.Forward])
                animationState.time = Math.Max(
                    animationState.time - speed / animationState.length * Time.deltaTime,
                    0);
            if (actions[(int)Animation.Forward])
                animation.Play();

        }
    }

    /// <summary>
    /// All animation action possible.
    /// </summary>
    public enum Animation
    {
        Play,
        Pause,
        Rewind,
        Forward
    }

}
