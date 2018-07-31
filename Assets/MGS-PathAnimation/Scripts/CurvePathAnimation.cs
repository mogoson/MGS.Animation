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

using Mogoson.CurvePath;
using UnityEngine;

namespace Mogoson.UAnimation
{
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

    /// <summary>
    /// Animation base on curve path.
    /// </summary>
    [AddComponentMenu("Mogoson/UAnimation/CurvePathAnimation")]
    public class CurvePathAnimation : MonoAnimation
    {
        #region Field and Property
        /// <summary>
        /// Path of animation.
        /// </summary>
        public MonoCurvePath path;

        /// <summary>
        /// Keep up mode on play animation.
        /// </summary>
        public KeepUpMode keepUp = KeepUpMode.WorldUp;

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
            if (timer < 0 || timer > path.MaxKey)
            {
                switch (loop)
                {
                    case LoopMode.Once:
                        Stop();
                        return;

                    case LoopMode.Loop:
                        timer -= path.MaxKey * TimerDirection;
                        break;

                    case LoopMode.PingPong:
                        speed = -speed;
                        timer = Mathf.Clamp(timer, 0, path.MaxKey);
                        break;
                }
            }
            TowTransformOnPath(timer);
        }

        /// <summary>
        /// Tow this transform base on path.
        /// </summary>
        /// <param name="time">Time of path curve.</param>
        protected void TowTransformOnPath(float time)
        {
            var timePos = path.GetPointAt(time);
            var deltaPos = path.GetPointAt(time + Delta * SpeedDirection);

            var worldUp = Vector3.up;
            switch (keepUp)
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
        /// Rewind animation.
        /// </summary>
        public override void Rewind()
        {
            timer = 0;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Align gameobject to path (Only call this method in editor script).
        /// </summary>
        public void AlignToPathInEditor()
        {
            TowTransformOnPath(0);
        }
#endif
        #endregion
    }
}