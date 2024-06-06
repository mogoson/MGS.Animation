/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UVFrameAnimation.cs
 *  Description  :  Define sequence frames animation base on UV offset.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Animations
{
    /// <summary>
    /// Sequence frames animation base on UV offset.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class UVFrameAnimation : FrameAnimation
    {
        /// <summary>
        /// Row of frames.
        /// </summary>
        [SerializeField]
        protected int row = 2;

        /// <summary>
        /// Column of frames.
        /// </summary>
        [SerializeField]
        protected int column = 3;

        /// <summary>
        /// Row of frames.
        /// </summary>
        public int Row { get { return row; } }

        /// <summary>
        /// Column of frames.
        /// </summary>
        public int Column { get { return column; } }

        /// <summary>
        /// Count of image frames.
        /// </summary>
        public override int FramesCount { get { return framesCount; } }

        /// <summary>
        /// Count of image frames.
        /// </summary>
        protected int framesCount;

        /// <summary>
        /// Width of a frame.
        /// </summary>
        protected float frameWidth;

        /// <summary>
        /// Height of a frame.
        /// </summary>
        protected float frameHeight;

        /// <summary>
        /// Renderer of animation.
        /// </summary>
        protected Renderer mRenderer;

        /// <summary>
        /// Awake animation.
        /// </summary>
        protected virtual void Awake()
        {
            mRenderer = GetComponent<Renderer>();
            SetFrames(mRenderer.material.mainTexture, row, column);
        }

        /// <summary>
        /// Set current frame to renderer.
        /// </summary>
        /// <param name="index">Index of frame.</param>
        protected override void SetFrame(int index)
        {
            mRenderer.material.mainTextureOffset = new Vector2(index % column * frameWidth, index / column * frameHeight);
        }

        /// <summary>
        /// Set frames of animation.
        /// </summary>
        /// <param name="frames">Frames texture.</param>
        /// <param name="row">Row of frames.</param>
        /// <param name="column">Column of frames.</param>
        public virtual void SetFrames(Texture frames, int row, int column)
        {
            this.row = row;
            this.column = column;

            framesCount = row * column;
            frameWidth = 1.0f / column;
            frameHeight = 1.0f / row;

            mRenderer.material.mainTexture = frames;
            mRenderer.material.mainTextureOffset = Vector2.zero;
            mRenderer.material.mainTextureScale = new Vector2(frameWidth, frameHeight);
        }
    }
}