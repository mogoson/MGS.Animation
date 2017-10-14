/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *  FileName: PathAnimation.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 7/5/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        PathAnimation             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     7/5/2017       0.1.0        Create this file.
 *************************************************************************/

using UnityEngine;

namespace Developer.PathAnimation
{
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
        /// Timer of animation.
        /// </summary>
        protected float timer;

        /// <summary>
        /// Delta to calculate tangent.
        /// </summary>
        protected float delta = 0.1f;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            path.wrapmode = wrapMode;
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
            var pathPoint = path.GetPointOnCurve(time);
            var tangent = (path.GetPointOnCurve(time + delta) - pathPoint).normalized;

            //Update position and look at tangent.
            transform.position = pathPoint;
            transform.LookAt(pathPoint + tangent, Vector3.up);
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
            enabled = true;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Align transform to path (Only call this method in editor script).
        /// </summary>
        public void AlignToPath()
        {
            if (path.curve == null)
                path.CreateCurve();

            TowTransformBaseOnPath(0);
        }
#endif
        #endregion
    }
}