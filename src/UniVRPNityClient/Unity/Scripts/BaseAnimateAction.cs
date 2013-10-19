using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public abstract class BaseAnimateAction : BaseAction
    {
        public bool Play, Stop, Pause, Rewind, Forward;

        public UnityEngine.Animation Animation;
        public AnimationState animationState;
        public float defaultSpeed = 1F;
        public float sensibilitySpeed = 1F;
        public int maxSpeed = 10;
    }

    /// <summary>
    /// Manipulate animation speed.
    /// </summary>
    [Serializable]
    public class AnimateSpeedAction : BaseAnimateAction
    {
        public void Update()
        {
            animationState = Animation[Animation.clip.name];
            if (Play)
                Animation.Play();

            if (Stop)
                Animation.Stop();

            if (Pause)
                animationState.speed = 0;

            if (Rewind)
                animationState.speed -= sensibilitySpeed * Time.deltaTime;

            if (Forward)
                animationState.speed += sensibilitySpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Manipulate animation time.
    /// </summary>
    [Serializable]
    public class AnimateTimeAction : BaseAnimateAction
    {
        public void Update()
        {
            AnimationState animationState = Animation[Animation.clip.name];
            if (Play)
            {
                Animation.Play();
                animationState.speed = defaultSpeed;
            }

            if (Stop)
                Animation.Stop();

            if (Pause)
                animationState.speed = 0;

            float step = sensibilitySpeed * animationState.length * Time.deltaTime;
            if (Rewind)
                animationState.time = Mathf.Clamp(animationState.time - step, 0, animationState.length);

            if (Forward)
                animationState.time = Mathf.Clamp(animationState.time + step, 0, animationState.length);
        }
    }

    /// <summary>
    /// All animation action possible.
    /// </summary>
    public enum Animate
    {
        Play,
        Stop,
        Pause,
        Rewind,
        Forward
    }

}
