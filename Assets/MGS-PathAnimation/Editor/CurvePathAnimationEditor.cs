/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePathAnimationEditor.cs
 *  DeTargetion  :  Editor for CurvePathAnimation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.PathAnimation
{
    [CustomEditor(typeof(CurvePathAnimation), true)]
    [CanEditMultipleObjects]
    public class CurvePathAnimationEditor : GenericEditor
    {
        #region Field and Property
        protected CurvePathAnimation Target { get { return target as CurvePathAnimation; } }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Align To Path"))
            {
                Target.TowTransformOnPath(0);
                MarkSceneDirty();
            }
        }
        #endregion
    }
}