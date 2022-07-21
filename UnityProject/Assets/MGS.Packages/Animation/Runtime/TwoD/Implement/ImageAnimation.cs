/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ImageAnimation.cs
 *  Description  :  Define sequence frames animation base Image.
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
    /// Sequence frames animation base on Image.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ImageAnimation : SpriteFrameAnimation
    {
        /// <summary>
        /// Image of animation.
        /// </summary>
        protected Image image;

        /// <summary>
        /// Awake animation.
        /// </summary>
        protected virtual void Awake()
        {
            image = GetComponent<Image>();
        }

        /// <summary>
        /// Set current frame to renderer.
        /// </summary>
        /// <param name="index">Index of frame.</param>
        protected override void SetFrame(int index)
        {
            image.sprite = frames[index];
        }
    }
}