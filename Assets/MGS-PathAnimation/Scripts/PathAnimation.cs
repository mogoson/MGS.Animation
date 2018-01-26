/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  PathAnimation.cs
 *  Description  :  Play animation base on path.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/5/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.PathAnimation
{
    public enum KeepUpMode
    {
        WorldUp = 0,
        TransformUp = 1,
        ReferenceForward = 2,
        ReferenceForwardAsNormal = 3
    }

    [AddComponentMenu("Developer/PathAnimation/PathAnimation")]
    public class PathAnimation : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Path of animation.
        /// </summary>
        public Path path;

        /// <summary>
        /// Speed of animation.
        /// </summary>
        public float speed = 5;

        /// <summary>
        /// Wrapmode of animation.
        /// </summary>
        [SerializeField]
        protected WrapMode wrapMode = WrapMode.Default;

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
        /// Delta to calculate secant.
        /// </summary>
        protected const float delta = 0.1f;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            path.Wrapmode = wrapMode;
        }

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
            var timePos = path.GetPointOnCurve(time);
            var deltaPos = path.GetPointOnCurve(time + delta);

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
                        var secant = (deltaPos - timePos).normalized;
                        worldUp = Vector3.Cross(secant, reference.forward);
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
            timer = 0;
            enabled = false;
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