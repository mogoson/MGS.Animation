/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorPathAnimation.cs
 *  Description  :  Define animation base on anchor curve path.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.PathAnimation
{
    [AddComponentMenu("Developer/PathAnimation/AnchorPathAnimation")]
    public class AnchorPathAnimation : CurvePathAnimation
    {
        #region Field and Property
        /// <summary>
        /// Path of animation.
        /// </summary>
        public new AnchorPath path;

        /// <summary>
        /// Wrapmode of animation.
        /// </summary>
        public WrapMode WrapMode
        {
            set { path.Wrapmode = value; }
            get { return path.Wrapmode; }
        }

        /// <summary>
        /// Wrapmode of animation.
        /// </summary>
        [SerializeField]
        protected WrapMode wrapMode = WrapMode.Default;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            path.Wrapmode = wrapMode;
        }
        #endregion
    }
}