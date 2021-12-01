/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UVAnimation.cs
 *  Description  :  Define UV offset animation.
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
    /// Animation base on UV offset.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class UVAnimation : MonoAnimation
    {
        /// <summary>
        /// Speed coefficient of move uv map.
        /// </summary>
        public Vector2 coefficient = Vector2.one;

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
        }

        /// <summary>
        /// Component update.
        /// </summary>
        protected virtual void Update()
        {
            mRenderer.material.mainTextureOffset += speed * coefficient * Time.deltaTime;
        }

        /// <summary>
        /// Rewind animation.
        /// </summary>
        public override void Rewind(float progress = 0)
        {
            progress = Mathf.Clamp01(progress);
            mRenderer.material.mainTextureOffset = Vector2.one * progress;
        }

        /// <summary>
        /// Set main texture of animation.
        /// </summary>
        /// <param name="texture">Animation main texture.</param>
        public virtual void SetTexture(Texture texture)
        {
            mRenderer.material.mainTexture = texture;
        }
    }
}