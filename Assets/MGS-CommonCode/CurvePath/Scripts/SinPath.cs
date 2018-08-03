/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SinPath.cs
 *  Description  :  Define path base on sin curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurvePath
{
    /// <summary>
    /// Path base on sin curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePath/SinPath")]
    public class SinPath : MonoCurvePath
    {
        #region Field and Property
        /// <summary>
        /// Args of sin curve.
        /// </summary>
        public SinArgs args = new SinArgs(1, 1, 0, 0);

        /// <summary>
        /// Max key of sin curve.
        /// </summary>
        public float maxKey = 2 * Mathf.PI;

        /// <summary>
        /// Curve for path.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of path.
        /// </summary>
        protected SinCurve curve = new SinCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild path.
        /// </summary>
        public override void Rebuild()
        {
            curve.args = args;
            curve.MaxKey = maxKey;
            base.Rebuild();
        }
        #endregion
    }
}