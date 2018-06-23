/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorPath.cs
 *  Description  :  Define curve path base on anchors.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.CurvePath
{
    /// <summary>
    /// Curve path base on anchors.
    /// </summary>
    [AddComponentMenu("Mogoson/CurvePath/AnchorPath")]
    public class AnchorPath : MonoCurvePath
    {
        #region Field and Property
        /// <summary>
        /// Path curve is close?
        /// </summary>
        public bool close = false;

        /// <summary>
        /// Anchors of path curve.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected List<Vector3> anchors = new List<Vector3>() { Vector3.one,
            new Vector3(1, 1, 2), new Vector3(3, 1, 2), new Vector3(3, 1, 3)};

        /// <summary>
        /// Count of path curve anchors.
        /// </summary>
        public int AnchorsCount { get { return anchors.Count; } }

        /// <summary>
        /// WrapMode of path curve.
        /// </summary>
        public WrapMode WrapMode
        {
            set { curve.PreWrapMode = curve.PostWrapMode = value; }
            get { return curve.PreWrapMode; }
        }

        /// <summary>
        /// Max time of path curve.
        /// </summary>
        public override float MaxTime
        {
            get
            {
                if (curve.Length > 0)
                    return curve[curve.Length - 1].time;
                else
                    return 0;
            }
        }

        /// <summary>
        /// VectorAnimationCurve of path.
        /// </summary>
        protected VectorAnimationCurve curve = new VectorAnimationCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild path.
        /// </summary>
        public override void Rebuild()
        {
            curve = VectorAnimationCurve.FromAnchors(anchors.ToArray(), close);
        }

        /// <summary>
        /// Get point on path curve at time.
        /// </summary>
        /// <param name="time">Time of curve.</param>
        /// <returns>The point on path curve at time.</returns>
        public override Vector3 GetPointAt(float time)
        {
            return transform.TransformPoint(curve.GetPointAt(time));
        }

        /// <summary>
        /// Add anchor item.
        /// </summary>
        /// <param name="item">Anchor item.</param>
        public void AddAnchor(Vector3 item)
        {
            anchors.Add(transform.InverseTransformPoint(item));
        }

        /// <summary>
        /// Insert Anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="item">Anchor item.</param>
        public void InsertAnchor(int index, Vector3 item)
        {
            anchors.Insert(index, transform.InverseTransformPoint(item));
        }

        /// <summary>
        /// Set the anchor item at index.
        /// </summary>
        /// <param name="index">Index of anchor.</param>
        /// <param name="item">Anchor item.</param>
        public void SetAnchorAt(int index, Vector3 item)
        {
            anchors[index] = transform.InverseTransformPoint(item);
        }

        /// <summary>
        /// Get the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        /// <returns>Anchor item.</returns>
        public Vector3 GetAnchorAt(int index)
        {
            return transform.TransformPoint(anchors[index]);
        }

        /// <summary>
        /// Remove the anchor item.
        /// </summary>
        /// <param name="item">Anchor item.</param>
        public void RemoveAnchor(Vector3 item)
        {
            anchors.Remove(item);
        }

        /// <summary>
        /// Remove the anchor item at index.
        /// </summary>
        /// <param name="index">Anchor index.</param>
        public void RemoveAnchorAt(int index)
        {
            anchors.RemoveAt(index);
        }

        /// <summary>
        /// Clear all anchor items.
        /// </summary>
        public void ClearAnchors()
        {
            anchors.Clear();
        }
        #endregion
    }
}