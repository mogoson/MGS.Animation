/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FrameAnimation.cs
 *  Description  :  Animation base on key frames.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Animations
{
    /// <summary>
    /// Animation base on key frames.
    /// </summary>
    public abstract class FrameAnimation : MonoAnimation
    {
        /// <summary>
        /// Event of animation play on last frame.
        /// </summary>
        public event Action OnLastFrameEvent;

        /// <summary>
        /// Index of current frame.
        /// </summary>
        protected float index = 0;

        /// <summary>
        /// Frames count.
        /// </summary>
        public abstract int FramesCount { get; }

        /// <summary>
        /// Update animation.
        /// </summary>
        protected virtual void Update()
        {
            index += speed * Time.deltaTime;
            if (index < 0 || index >= FramesCount)
            {
                switch (loopMode)
                {
                    case LoopMode.Once:
                        enabled = false;
                        index = 0;
                        break;

                    case LoopMode.Loop:
                        index -= FramesCount * (index < 0 ? -1 : 1);
                        break;

                    case LoopMode.PingPong:
                        speed = -speed;
                        index = Mathf.Clamp(index, 1, FramesCount - 1);
                        break;
                }
                InvokeOnLastFrameEvent();
            }
            else
            {
                SetFrame((int)index);
            }
        }

        /// <summary>
        /// Set current frame to renderer.
        /// </summary>
        /// <param name="index">Index of frame.</param>
        protected abstract void SetFrame(int index);

        /// <summary>
        /// 
        /// </summary>
        protected void InvokeOnLastFrameEvent()
        {
            if (OnLastFrameEvent != null)
            {
                OnLastFrameEvent.Invoke();
            }
        }

        /// <summary>
        /// Rewind animation.
        /// </summary>
        /// <param name="progress">Progress of animation in the range[0~1]</param>
        public override void Rewind(float progress = 0)
        {
            progress = Mathf.Clamp01(progress);
            index = progress * (FramesCount - 1);
            SetFrame((int)index);
        }
    }
}