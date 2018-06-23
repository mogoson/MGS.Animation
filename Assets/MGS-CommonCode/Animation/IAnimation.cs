/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAnimation.cs
 *  Description  :  Define animation interface.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace Mogoson.AnimationExtension
{
    /// <summary>
    /// Loop mode of animation.
    /// </summary>
    public enum LoopMode
    {
        Once = 0,
        Loop = 1,
        PingPong = 2,
    }

    /// <summary>
    /// Interface of animation.
    /// </summary>
    public interface IAnimation
    {
        #region Property
        /// <summary>
        /// Speed of animation.
        /// </summary>
        float Speed { set; get; }

        /// <summary>
        /// Loop mode of animation.
        /// </summary>
        LoopMode LoopMode { set; get; }
        #endregion

        #region Method
        /// <summary>
        /// Play animation.
        /// </summary>
        void Play();

        /// <summary>
        /// Pause animation.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stop animation.
        /// </summary>
        void Stop();

        /// <summary>
        /// Rewind animation.
        /// </summary>
        void Rewind();
        #endregion
    }
}