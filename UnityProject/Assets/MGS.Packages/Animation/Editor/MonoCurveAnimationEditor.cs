/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveAnimationEditor.cs
 *  DeTargetion  :  Editor for MonoCurveAnimation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace MGS.Animations.Editors
{
    [CustomEditor(typeof(MonoCurveAnimation), true)]
    public class MonoCurveAnimationEditor : Editor
    {
        protected MonoCurveAnimation Target { get { return target as MonoCurveAnimation; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawAnimInspector();
        }

        protected virtual void DrawAnimInspector()
        {
            EditorGUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("Align To Curve"))
            {
                Target.curve.Rebuild();
                Target.Rewind(0);
                MarkSceneDirty();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        protected void MarkSceneDirty()
        {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            EditorSceneManager.MarkAllScenesDirty();
#else
            EditorApplication.MarkSceneDirty();
#endif
        }
    }
}