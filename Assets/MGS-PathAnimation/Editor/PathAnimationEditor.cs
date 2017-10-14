/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  PathAnimationEditor.cs
 *  Description  :  Editor for PathAnimation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/9/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace Developer.PathAnimation
{
    [CustomEditor(typeof(PathAnimation), true)]
    [CanEditMultipleObjects]
    public class PathAnimationEditor : Editor
    {
        #region Property and Field
        protected PathAnimation script { get { return target as PathAnimation; } }
        #endregion

        #region Protected Method
        protected void MarkSceneDirty()
        {
#if UNITY_5_3_OR_NEWER
            EditorSceneManager.MarkAllScenesDirty();
#else
            EditorApplication.MarkSceneDirty();
#endif
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("AlignToPath"))
            {
                script.AlignToPath();
                MarkSceneDirty();
            }
        }
        #endregion
    }
}