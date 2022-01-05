/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAnimation.cs
 *  Description  :  Define animation interface.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Animations
{
    /// <summary>
    /// Interface of animation.
    /// </summary>
    public interface IAnimation
    {
        /// <summary>
        /// Speed of animation.
        /// </summary>
        float Speed { set; get; }

        /// <summary>
        /// Loop mode of animation.
        /// </summary>
        LoopMode LoopMode { set; get; }

        /// <summary>
        /// Animation is playing?
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Play animation.
        /// </summary>
        void Play();

        /// <summary>
        /// Pause animation.
        /// </summary>
        void Pause();

        /// <summary>
        /// Rewind animation.
        /// </summary>
        /// <param name="progress">Progress of animation in the range[0~1]</param>
        void Rewind(float progress = 0);

        /// <summary>
        /// Stop animation.
        /// </summary>
        void Stop();
    }

    /// <summary>
    /// Loop mode of animation.
    /// </summary>
    public enum LoopMode
    {
        /// <summary>
        /// Animation just play once.
        /// </summary>
        Once = 0,

        /// <summary>
        /// Animation loop play.
        /// </summary>
        Loop = 1,

        /// <summary>
        /// Animation play like ping pong.
        /// </summary>
        PingPong = 2,
    }
}