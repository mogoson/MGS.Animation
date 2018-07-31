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

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurvePath
{
    /// <summary>
    /// Path base on cubic bezier curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePath/BezierPath")]
    public class BezierPath : MonoCurvePath
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

        /// <summary>
        /// Curve for path.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of path.
        /// </summary>
        protected CubicBezierCurve curve = new CubicBezierCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild path.
        /// </summary>
        public override void Rebuild()
        {
            curve.anchor = anchor;
        }
        #endregion
    }
}