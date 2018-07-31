/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoAnimation.cs
 *  Description  :  Define mono animation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.UAnimation
{
    /// <summary>
    /// Mono animation.
    /// </summary>
    public abstract class MonoAnimation : MonoBehaviour, IAnimation
    {
        #region Field and Property
        /// <summary>
        /// Speed of animation.
        /// </summary>
        [SerializeField]
        protected float speed = 5;

        /// <summary>
        /// Loop mode of animation.
        /// </summary>
        [SerializeField]
        protected LoopMode loop = LoopMode.Once;

        /// <summary>
        /// Speed of animation.
        /// </summary>
        public virtual float Speed
        {
            set { speed = value; }
            get { return speed; }
        }

        /// <summary>
        /// Loop mode of animation.
        /// </summary>
        public virtual LoopMode LoopMode
        {
            set { loop = value; }
            get { return loop; }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Play animation.
        /// </summary>
        public virtual void Play()
        {
            enabled = true;
        }

        /// <summary>
        /// Pause animation.
        /// </summary>
        public virtual void Pause()
        {
            enabled = false;
        }

        /// <summary>
        /// Stop animation.
        /// </summary>
        public virtual void Stop()
        {
            enabled = false;
            Rewind();
        }

        /// <summary>
        /// Rewind animation.
        /// </summary>
        public abstract void Rewind();
        #endregion
    }
}