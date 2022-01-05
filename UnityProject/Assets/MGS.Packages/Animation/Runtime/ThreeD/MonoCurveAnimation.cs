/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveAnimation.cs
 *  Description  :  Define animation base on mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;
using UnityEngine;

namespace MGS.Animations
{
    /// <summary>
    /// Animation base on mono curve.
    /// </summary>
    public class MonoCurveAnimation : MonoAnimation
    {
        /// <summary>
        /// Curve for animation base on.
        /// </summary>
        public MonoCurve curve;

        /// <summary>
        /// Up mode for tow on curve.
        /// </summary>
        public CurveTowUpMode upMode = CurveTowUpMode.WorldUp;

        /// <summary>
        /// Keep up reference transform.
        /// </summary>
        public Transform reference;

        /// <summary>
        /// DELTA for timer to calculate tangent.
        /// </summary>
        protected const float DELTA = 0.05f;

        /// <summary>
        /// Timer of animation.
        /// </summary>
        protected float timer = 0;

        /// <summary>
        /// Direction of timer.
        /// </summary>
        protected int TimerDirection { get { return timer < 0 ? -1 : 1; } }

        /// <summary>
        /// Direction of speed.
        /// </summary>
        protected int SpeedDirection { get { return speed < 0 ? -1 : 1; } }

        /// <summary>
        /// Update animation.
        /// </summary>
        protected virtual void Update()
        {
            timer += speed * Time.deltaTime;
            if (timer < 0 || timer > curve.Length)
            {
                switch (loopMode)
                {
                    case LoopMode.Once:
                        Stop();
                        return;

                    case LoopMode.Loop:
                        timer -= curve.Length * TimerDirection;
                        break;

                    case LoopMode.PingPong:
                        speed = -speed;
                        timer = Mathf.Clamp(timer, 0, curve.Length);
                        break;
                }
            }
            TowOnCurve(timer);
        }

        /// <summary>
        /// Tow transform base on curve.
        /// </summary>
        /// <param name="len">Length of curve.</param>
        protected virtual void TowOnCurve(float len)
        {
            var timePos = curve.Evaluate(len);
            var nextPos = curve.Evaluate(len + SpeedDirection * DELTA);

            var worldUp = Vector3.up;
            switch (upMode)
            {
                case CurveTowUpMode.TransformUp:
                    worldUp = transform.up;
                    break;

                case CurveTowUpMode.ReferenceUp:
                    if (reference)
                    {
                        worldUp = reference.up;
                    }
                    break;

                case CurveTowUpMode.ReferenceUpAsNormal:
                    if (reference)
                    {
                        var tangent = (nextPos - timePos).normalized;
                        worldUp = Vector3.Cross(tangent, reference.up);
                    }
                    break;
            }

            //Update position and look at tangent.
            transform.position = timePos;
            transform.LookAt(nextPos, worldUp);
        }

        /// <summary>
        /// Rewind animation.
        /// </summary>
        /// <param name="progress">Progress of animation in the range[0~1]</param>
        public override void Rewind(float progress = 0)
        {
            progress = Mathf.Clamp01(progress);
            timer = curve.Length * progress;
            TowOnCurve(timer);
        }
    }
}