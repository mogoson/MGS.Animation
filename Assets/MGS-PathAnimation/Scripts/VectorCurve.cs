/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: VectorCurve.cs
 *  Author: Mogoson   Version: 1.0   Date: 6/21/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.     VectorKeyframe(struct)       Ignore.
 *     1.     VectorAnimationCurve         Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     6/21/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.AnimationCurve
{
    using UnityEngine;

    public struct VectorKeyframe
    {
        #region Property and Field
        public float time;
        public Vector3 value;
        #endregion

        #region Public Method
        public VectorKeyframe(float time, Vector3 value)
        {
            this.time = time;
            this.value = value;
        }
        #endregion
    }//struct_end

    public class VectorAnimationCurve
    {
        #region Property and Field
        public VectorKeyframe this[int index]
        {
            get
            {
                return new VectorKeyframe(xCurve[index].time, new Vector3(xCurve[index].value, yCurve[index].value, zCurve[index].value));
            }
        }

        /// <summary>
        /// Keyframe count.
        /// </summary>
        public int length { get { return xCurve.length; } }

        /// <summary>
        /// The behaviour of the animation after the last keyframe.
        /// </summary>
        public WrapMode postWrapMode
        {
            set { xCurve.postWrapMode = yCurve.postWrapMode = zCurve.postWrapMode = value; }
            get { return xCurve.postWrapMode; }
        }

        /// <summary>
        /// The behaviour of the animation before the first keyframe.
        /// </summary>
        public WrapMode preWrapMode
        {
            set { xCurve.preWrapMode = yCurve.preWrapMode = zCurve.preWrapMode = value; }
            get { return xCurve.preWrapMode; }
        }

        protected AnimationCurve xCurve;
        protected AnimationCurve yCurve;
        protected AnimationCurve zCurve;
        #endregion

        #region Public Method
        public VectorAnimationCurve()
        {
            xCurve = new AnimationCurve();
            yCurve = new AnimationCurve();
            zCurve = new AnimationCurve();
        }

        /// <summary>
        /// Add a new key to the curve.
        /// </summary>
        /// <param name="key">The key to add to the curve.</param>
        /// <returns>The index of the added key, or -1 if the key could not be added.</returns>
        public int AddKey(VectorKeyframe key)
        {
            xCurve.AddKey(key.time, key.value.x);
            yCurve.AddKey(key.time, key.value.y);
            return zCurve.AddKey(key.time, key.value.z);
        }

        /// <summary>
        /// Add a new key to the curve.
        /// </summary>
        /// <param name="time">The time at which to add the key (horizontal axis in the curve graph).</param>
        /// <param name="value">The value for the key (vertical axis in the curve graph).</param>
        /// <returns>The index of the added key, or -1 if the key could not be added.</returns>
        public int AddKey(float time, Vector3 value)
        {
            xCurve.AddKey(time, value.x);
            yCurve.AddKey(time, value.y);
            return zCurve.AddKey(time, value.z);
        }

        /// <summary>
        /// Evaluate the curve at time.
        /// </summary>
        /// <param name="time">The time within the curve you want to evaluate (the horizontal axis in the curve graph).</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        public Vector3 Evaluate(float time)
        {
            return new Vector3(xCurve.Evaluate(time), yCurve.Evaluate(time), zCurve.Evaluate(time));
        }

        /// <summary>
        /// Removes a key.
        /// </summary>
        /// <param name="index">The index of the key to remove.</param>
        public void RemoveKey(int index)
        {
            xCurve.RemoveKey(index);
            yCurve.RemoveKey(index);
            zCurve.RemoveKey(index);
        }

        /// <summary>
        /// Smooth the in and out tangents of the keyframe at index.
        /// </summary>
        /// <param name="index">The index of the keyframe to be smoothed.</param>
        /// <param name="weight">The smoothing weight to apply to the keyframe's tangents.</param>
        public void SmoothTangents(int index, float weight)
        {
            xCurve.SmoothTangents(index, weight);
            yCurve.SmoothTangents(index, weight);
            zCurve.SmoothTangents(index, weight);
        }

        /// <summary>
        /// Smooth the in and out tangents of keyframes.
        /// </summary>
        /// <param name="weight">The smoothing weight to apply to the keyframe's tangents.</param>
        public void SmoothTangents(float weight)
        {
            for(int i = 0; i < length; i++)
            {
                SmoothTangents(i, weight);
            }
        }
        #endregion
    }//struct_end
}//namespace_end