/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePathEditor.cs
 *  DeTargetion  :  Editor for CurvePath.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using Developer.EditorExtension;
using UnityEditor;
using UnityEngine;

namespace Developer.PathAnimation
{
    [CustomEditor(typeof(CurvePath), true)]
    [CanEditMultipleObjects]
    public class CurvePathEditor : GenericEditor
    {
        #region Field and Property
        protected CurvePath Target { get { return target as CurvePath; } }
        protected const float Delta = 0.05f;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (!Application.isPlaying)
            {
                Target.Rebuild();
                Undo.undoRedoPerformed = () => { Target.Rebuild(); };
            }
        }

        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            for (float t = 0; t < Target.GetMaxTime(); t += Delta)
            {
                Handles.DrawLine(Target.GetPoint(t), Target.GetPoint(t + Delta));
            }
        }

        protected virtual void OnDisable()
        {
            Undo.undoRedoPerformed = null;
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                if (!Application.isPlaying)
                    Target.Rebuild();
            }
        }
        #endregion
    }
}