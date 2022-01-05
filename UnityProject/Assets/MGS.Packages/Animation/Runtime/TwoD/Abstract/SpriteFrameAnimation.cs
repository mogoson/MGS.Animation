/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SpriteFrameAnimation.cs
 *  Description  :  Animation base on sprite frames.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/20/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Animations
{
    /// <summary>
    /// Animation base on sprite frames.
    /// </summary>
    public abstract class SpriteFrameAnimation : FrameAnimation
    {
        /// <summary>
        /// Sprite frames of animation.
        /// </summary>
        [SerializeField]
        protected List<Sprite> frames = new List<Sprite>();

        /// <summary>
        /// Frames count.
        /// </summary>
        public override int FramesCount { get { return frames.Count; } }

        /// <summary>
        /// Set frames texture of animation.
        /// </summary>
        /// <param name="frames">Animation frames.</param>
        public virtual void SetFrames(IEnumerable<Sprite> frames)
        {
            if (frames == null)
            {
                return;
            }

            this.frames.Clear();
            this.frames.AddRange(frames);
        }
    }
}