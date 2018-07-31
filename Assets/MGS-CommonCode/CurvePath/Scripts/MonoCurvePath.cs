/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurvePath.cs
 *  Description  :  Define path base on curve.
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
    /// Path base on curve.
    /// </summary>
    public abstract class MonoCurvePath : MonoBehaviour, ICurvePath
    {
        #region Field and Property
        /// <summary>
        /// Length of path curve.
        /// </summary>
        public virtual float Length { get { return Curve.Length; } }

        /// <summary>
        /// Max key of path curve.
        /// </summary>
        public virtual float MaxKey { get { return Curve.MaxKey; } }

        /// <summary>
        /// Curve for path.
        /// </summary>
        protected abstract ICurve Curve { get; }
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
        /// Get point on path curve at key.
        /// </summary>
        /// <param name="key">Key of curve.</param>
        /// <returns>The point on path curve at key.</returns>
        public virtual Vector3 GetPointAt(float key)
        {
            return transform.TransformPoint(Curve.GetPointAt(key));
        }
        #endregion
    }
}