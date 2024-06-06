/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RawImageAnimation.cs
 *  Description  :  Define sequence frames animation base on RawImage.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace MGS.Animations
{
    /// <summary>
    /// Sequence frames animation base on RawImage.
    /// </summary>
    [RequireComponent(typeof(RawImage))]
    public class RawImageAnimation : TextureFrameAnimation
    {
        /// <summary>
        /// Renderer of animation.
        /// </summary>
        protected RawImage rawImage;

        /// <summary>
        /// Awake animation.
        /// </summary>
        protected virtual void Awake()
        {
            rawImage = GetComponent<RawImage>();
        }

        /// <summary>
        /// Set current frame to renderer.
        /// </summary>
        /// <param name="index">Index of frame.</param>
        protected override void SetFrame(int index)
        {
            rawImage.texture = frames[index];
        }
    }
}