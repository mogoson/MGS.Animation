/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePath.cs
 *  Description  :  Define path base on curve.
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
    /// Path base on curve.
    /// </summary>
    public abstract class CurvePath : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Max time of path curve.
        /// </summary>
        public abstract float MaxTime { get; }
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            Rebuild();
        }

        protected virtual void Awake()
        {
            Rebuild();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild path.
        /// </summary>
        public abstract void Rebuild();

        /// <summary>
        /// Get point on path curve at time.
        /// </summary>
        /// <param name="time">Time of curve.</param>
        /// <returns>The point on path curve at time.</returns>
        public abstract Vector3 GetPoint(float time);
        #endregion
    }
}