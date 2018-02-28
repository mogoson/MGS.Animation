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
        protected float timer;

        /// <summary>
        /// Delta to calculate tangent.
        /// </summary>
        protected const float Delta = 0.05f;
        #endregion

        #region Protected Method
        protected virtual void Update()
        {
            timer += speed * Time.deltaTime;
            TowTransformBaseOnPath(timer);
        }

        /// <summary>
        /// Tow transform base on path.
        /// </summary>
        /// <param name="time">Time of path curve.</param>
        protected void TowTransformBaseOnPath(float time)
        {
            var timePos = path.GetPoint(time);
            var deltaPos = path.GetPoint(time + Delta);

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

            //Update position and look at secant.
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
        /// Align transform to path (Only call this method in editor script).
        /// </summary>
        public void AlignToPath()
        {
            TowTransformBaseOnPath(0);
        }
#endif
        #endregion
    }
}