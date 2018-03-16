/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePathAnimation.cs
 *  Description  :  Define animation base on curve path.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.PathAnimation
{
    /// <summary>
    /// Loop mode of animation.
    /// </summary>
    public enum LoopMode
    {
        Once = 0,
        Loop = 1,
        PingPong = 2,
    }

    /// <summary>
    /// Keep up mode of animation base on curve path.
    /// </summary>
    public enum KeepUpMode
    {
        WorldUp = 0,
        TransformUp = 1,
        ReferenceForward = 2,
        ReferenceForwardAsNormal = 3
    }

    [AddComponentMenu("Developer/PathAnimation/CurvePathAnimation")]
    public class CurvePathAnimation : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Path of animation.
        /// </summary>
        public CurvePath path;

        /// <summary>
        /// Speed of animation.
        /// </summary>
        public float speed = 5;

        /// <summary>
        /// Loop mode of animation.
        /// </summary>
        public LoopMode loopMode = LoopMode.Once;

        /// <summary>
        /// Keep up mode on play animation.
        /// </summary>
        public KeepUpMode keepUpMode = KeepUpMode.WorldUp;

        /// <summary>
        /// Keep up reference transform.
        /// </summary>
        [HideInInspector]
        public Transform reference;

        /// <summary>
        /// Timer of animation.
        /// </summary>
        protected float timer = 0;

        /// <summary>
        /// Delta to calculate tangent.
        /// </summary>
        protected const float Delta = 0.05f;

        /// <summary>
        /// Direction of timer.
        /// </summary>
        protected int TimerDirection { get { return timer < 0 ? -1 : 1; } }

        /// <summary>
        /// Direction of speed.
        /// </summary>
        protected int SpeedDirection { get { return speed < 0 ? -1 : 1; } }
        #endregion

        #region Protected Method
        protected virtual void Update()
        {
            timer += speed * Time.deltaTime;
            if (timer < 0 || timer > path.MaxTime)
            {
                switch (loopMode)
                {
                    case LoopMode.Once:
                        Stop();
                        return;

                    case LoopMode.Loop:
                        timer -= path.MaxTime * TimerDirection;
                        break;

                    case LoopMode.PingPong:
                        speed = -speed;
                        timer = Mathf.Clamp(timer, 0, path.MaxTime);
                        break;
                }
            }
            TowGameObjectOnPath(timer);
        }

        /// <summary>
        /// Tow gameobject base on path.
        /// </summary>
        /// <param name="time">Time of path curve.</param>
        protected void TowGameObjectOnPath(float time)
        {
            var timePos = path.GetPoint(time);
            var deltaPos = path.GetPoint(time + Delta * SpeedDirection);

            var worldUp = Vector3.up;
            switch (keepUpMode)
            {
                case KeepUpMode.TransformUp:
                    worldUp = transform.up;
                    break;

                case KeepUpMode.ReferenceForward:
                    if (reference)
                        worldUp = reference.forward;
                    break;

                case KeepUpMode.ReferenceForwardAsNormal:
                    if (reference)
                    {
                        var tangent = (deltaPos - timePos).normalized;
                        worldUp = Vector3.Cross(tangent, reference.forward);
                    }
                    break;
            }

            //Update position and look at tangent.
            transform.position = timePos;
            transform.LookAt(deltaPos, worldUp);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Play animation.
        /// </summary>
        public void Play()
        {
            enabled = true;
        }

        /// <summary>
        /// Pause animation.
        /// </summary>
        public void Pause()
        {
            enabled = false;
        }

        /// <summary>
        /// Stop animation.
        /// </summary>
        public void Stop()
        {
            enabled = false;
            timer = 0;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Align gameobject to path (Only call this method in editor script).
        /// </summary>
        public void AlignToPathInEditor()
        {
            TowGameObjectOnPath(0);
        }
#endif
        #endregion
    }
}