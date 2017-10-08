/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *  FileName: PathAnimationEditor.cs
 *  Author: Mogoson   Version: 0.1.0   Date: 7/9/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      PathAnimationEditor         Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     7/9/2017       0.1.0        Create this file.
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

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("AlignToPath"))
            {
                script.AlignToPath();

#if UNITY_5_3_OR_NEWER
                EditorSceneManager.MarkAllScenesDirty();
#else
                EditorApplication.MarkSceneDirty();
#endif
            }
        }
        #endregion
    }
}