/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: Path.cs
 *  Author: Mogoson   Version: 1.0   Date: 7/5/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.             Path                 Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     7/5/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Animation
{
    using AnimationCurve;
    using System.Collections.Generic;
    using UnityEngine;

    [AddComponentMenu("Developer/Animation/Path")]
    public class Path : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Close state of path curve.
        /// </summary>
        [HideInInspector]
        public bool isClose = false;

        /// <summary>
        /// Anchors of path curve.
        /// </summary>
        [HideInInspector]
        public List<Vector3> anchors = new List<Vector3>();

        /// <summary>
        /// WrapMode of path curve.
        /// </summary>
        public WrapMode wrapmode
        {
            set { curve.preWrapMode = curve.postWrapMode = value; }
            get { return curve.preWrapMode; }
        }

        /// <summary>
        /// Max time of path curve.
        /// </summary>
        public float maxTime
        {
            get { return curve[curve.length - 1].time; }
        }

        /// <summary>
        /// VectorAnimationCurve of path.
        /// </summary>
        public VectorAnimationCurve curve;
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            curve = null;
        }

        protected virtual void Awake()
        {
            CreateCurve();
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (curve == null)
            {
                if (anchors.Count == 0)
                    return;
                else
                    CreateCurve();
            }

            Gizmos.color = new Color(0, 1, 1, 1);
            for (float time = 0; time < maxTime; time += 0.05f)
            {
                Gizmos.DrawLine(GetPathPoint(time), GetPathPoint(Mathf.Clamp(time + 0.05f, 0, maxTime)));
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Create path curve base on anchors.
        /// </summary>
        public virtual void CreateCurve()
        {
            if(anchors.Count == 0)
            {
                curve = null;
                return;
            }

            curve = new VectorAnimationCurve();
            curve.preWrapMode = curve.postWrapMode = wrapmode;

            //Add frame keys to curve.
            float time = 0;
            for (int i = 0; i < anchors.Count - 1; i++)
            {
                curve.AddKey(time, anchors[i]);
                time += Vector3.Distance(anchors[i], anchors[i + 1]);
            }

            //Add last key.
            curve.AddKey(time, anchors[anchors.Count - 1]);

            if (isClose)
            {
                //Add close key[the first key]
                time += Vector3.Distance(anchors[anchors.Count - 1], anchors[0]);
                curve.AddKey(time, anchors[0]);
            }

            //Smooth curve keys out tangent.
            curve.SmoothTangents(0);
        }

        /// <summary>
        /// Get point on path curve at time.
        /// </summary>
        /// <param name="time">Time in curve.</param>
        /// <returns>The point on path curve at time</returns>
        public virtual Vector3 GetPathPoint(float time)
        {
            return transform.TransformPoint(curve.Evaluate(time));
        }
        #endregion
    }//class_end
}//namespace_end