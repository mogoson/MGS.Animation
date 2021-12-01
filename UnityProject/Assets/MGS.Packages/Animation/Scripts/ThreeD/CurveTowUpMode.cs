/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurveTowUpMode.cs
 *  Description  :  Define up mode of animation base on curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Animations
{
    /// <summary>
    /// Up mode for tow on curve.
    /// </summary>
    public enum CurveTowUpMode
    {
        /// <summary>
        /// Use world up.
        /// </summary>
        WorldUp = 0,

        /// <summary>
        /// Use transform up.
        /// </summary>
        TransformUp = 1,

        /// <summary>
        /// Use reference up.
        /// </summary>
        ReferenceUp = 2,

        /// <summary>
        /// Use the cross of tangent and reference up.
        /// </summary>
        ReferenceUpAsNormal = 3
    }
}