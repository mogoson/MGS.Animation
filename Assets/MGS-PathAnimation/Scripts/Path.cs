/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  Path.cs
 *  Description  :  Define path curve base on anchors.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/5/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using Developer.AnimationCurveExtension;
using UnityEngine;

namespace Developer.PathAnimation
{
    [AddComponentMenu("Developer/PathAnimation/Path")]
    public class Path : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Close state of path curve.
        /// </summary>
        [HideInInspector]
        public bool isClose = false;

        /// <summary>
        /// Anchors of path curve.
        /// </summary>
        [HideInInspector]
        public List<Vector3> anchors = new List<Vector3>();

        /// <summary>
        /// WrapMode of path curve.
        /// </summary>
        public WrapMode Wrapmode
        {
            set { curve.PreWrapMode = curve.PostWrapMode = value; }
            get { return curve.PreWrapMode; }
        }

        /// <summary>
        /// Max time of path curve.
        /// </summary>
        public float MaxTime
        {
            get
            {
                var maxTime = 0f;
                if (curve.Length > 0)
                    maxTime = curve[curve.Length - 1].time;
                return maxTime;
            }
        }

        /// <summary>
        /// VectorAnimationCurve of path.
        /// </summary>
        protected VectorAnimationCurve curve = new VectorAnimationCurve();

#if UNITY_EDITOR
        /// <summary>
        /// Blue color (Only use it in editor script).
        /// </summary>
        protected readonly Color blue = new Color(0, 1, 1, 1);

        /// <summary>
        /// Delta time of path curve (Only use it in editor script).
        /// </summary>
        protected const float delta = 0.05f;
#endif
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            CreateCurve();
        }

        protected virtual void Awake()
        {
            CreateCurve();
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = blue;
            for (float time = 0; time < MaxTime; time += delta)
            {
                Gizmos.DrawLine(GetPointOnCurve(time), GetPointOnCurve(Mathf.Clamp(time + delta, 0, MaxTime)));
            }
        }
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// Create path curve base on anchors.
        /// </summary>
        public virtual void CreateCurve()
        {
            //New curve.
            curve = new VectorAnimationCurve();

            //No anchor.
            if (anchors.Count == 0)
                return;

            //Add frame keys to curve.
            var time = 0f;
            for (int i = 0; i < anchors.Count - 1; i++)
            {
                curve.AddKey(time, anchors[i]);
                time += Vector3.Distance(anchors[i], anchors[i + 1]);
            }

            //Add last key.
            curve.AddKey(time, anchors[anchors.Count - 1]);

            if (isClose)
            {
                //Add close key(the first key).
                time += Vector3.Distance(anchors[anchors.Count - 1], anchors[0]);
                curve.AddKey(time, anchors[0]);
            }

            //Smooth curve keys out tangent.
            curve.SmoothTangents(0);
        }

        /// <summary>
        /// Get point on path curve at time.
        /// </summary>
        /// <param name="time">Time of path curve.</param>
        /// <returns>The point on path curve at time.</returns>
        public virtual Vector3 GetPointOnCurve(float time)
        {
            return transform.TransformPoint(curve.Evaluate(time));
        }
        #endregion
    }
}