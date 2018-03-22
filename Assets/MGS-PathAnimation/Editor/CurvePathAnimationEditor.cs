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

using Mogoson.EditorExtension;
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
        protected SerializedProperty reference;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            reference = serializedObject.FindProperty("reference");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Target.keepUpMode == KeepUpMode.ReferenceForward || Target.keepUpMode == KeepUpMode.ReferenceForwardAsNormal)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(reference);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Align To Path"))
            {
                Target.AlignToPathInEditor();
                MarkSceneDirty();
            }
        }
        #endregion
    }
}