/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BezierPath.cs
 *  Description  :  Define path base on cubic bezier curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Developer.MathExtension.Curve;
using UnityEngine;

namespace Developer.PathAnimation
{
    [AddComponentMenu("Developer/PathAnimation/BezierPath")]
    public class BezierPath : CurvePath
    {
        #region Field and Property
        /// <summary>
        /// Anchor points of path curve.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected CubicBezierAnchor anchor = new CubicBezierAnchor(Vector3.one,
            new Vector3(3, 1, 3), new Vector3(1, 1, 2), new Vector3(3, 1, 2));

        /// <summary>
        /// Start point of path curve.
        /// </summary>
        public Vector3 StartPoint
        {
            set { anchor.start = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.start); }
        }

        /// <summary>
        /// End point of path curve.
        /// </summary>
        public Vector3 EndPoint
        {
            set { anchor.end = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.end); }
        }

        /// <summary>
        /// Start tangent point of path curve.
        /// </summary>
        public Vector3 StartTangentPoint
        {
            set { anchor.startTangent = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.startTangent); }
        }

        /// <summary>
        /// End tangent point of path curve.
        /// </summary>
        public Vector3 EndTangentPoint
        {
            set { anchor.endTangent = transform.InverseTransformPoint(value); }
            get { return transform.TransformPoint(anchor.endTangent); }
        }
        #endregion

        #region Public Method
        public override void Rebuild() { }

        /// <summary>
        /// Get point on path curve at time.
        /// </summary>
        /// <param name="time">Time of curve.</param>
        /// <returns>The point on path curve at time.</returns>
        public override Vector3 GetPoint(float time)
        {
            return transform.TransformPoint(CubicBezierCurve.GetPoint(anchor, time));
        }

        /// <summary>
        /// Get max time of path curve.
        /// </summary>
        /// <returns>Max time of path curve.</returns>
        public override float GetMaxTime()
        {
            return 1.0f;
        }
        #endregion
    }
}