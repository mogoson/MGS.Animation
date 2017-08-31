/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: PathEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 7/5/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          PathEditor              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     7/5/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Animation
{
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    [CustomEditor(typeof(Path), true)]
    [CanEditMultipleObjects]
    public class PathEditor : Editor
    {
        #region Property and Field
        protected Path script { get { return target as Path; } }

        protected Color blue = new Color(0, 1, 1, 1);
        protected float nodeSize = 0.1f;
        protected float buttonOffset = 0.5f;
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            if (Application.isPlaying)
                return;

            for (int i = 0; i < script.anchors.Count; i++)
            {
                var anchorPos = script.transform.TransformPoint(script.anchors[i]);
                var handleSize = HandleUtility.GetHandleSize(anchorPos);

                Handles.color = blue;
                Handles.SphereCap(0, anchorPos, Quaternion.identity, handleSize * nodeSize);
                EditorGUI.BeginChangeCheck();
                var position = Handles.PositionHandle(anchorPos, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    script.anchors[i] = script.transform.InverseTransformPoint(position);
                    script.CreateCurve();
                    EditorSceneManager.MarkAllScenesDirty();
                }

                var buttonSize = handleSize * nodeSize * 2;
                Handles.color = Color.green;
                if (Handles.Button(anchorPos + new Vector3(0, 0, handleSize * buttonOffset), Quaternion.identity, buttonSize, buttonSize, Handles.SphereCap))
                {
                    var anchorOffset = new Vector3(0, 0, handleSize);
                    if (i > 0)
                        anchorOffset = (script.anchors[i] - script.anchors[i - 1]).normalized * handleSize;
                    script.anchors.Insert(i + 1, script.anchors[i] + anchorOffset);
                    script.CreateCurve();
                    EditorSceneManager.MarkAllScenesDirty();
                }

                Handles.color = Color.red;
                if (Handles.Button(anchorPos + new Vector3(handleSize * buttonOffset, 0, 0), Quaternion.identity, buttonSize, buttonSize, Handles.SphereCap))
                {
                    script.anchors.RemoveAt(i);
                    script.CreateCurve();
                    EditorSceneManager.MarkAllScenesDirty();
                }
            }

            if (script.anchors.Count == 0)
            {
                var handleSize = HandleUtility.GetHandleSize(script.transform.position);
                var buttonSize = handleSize * nodeSize * 2;

                Handles.color = Color.green;
                if (Handles.Button(script.transform.position + new Vector3(0, 0, handleSize * buttonOffset), Quaternion.identity, buttonSize, buttonSize, Handles.SphereCap))
                {
                    script.anchors.Insert(0, new Vector3(0, 0, handleSize));
                    script.CreateCurve();
                    EditorSceneManager.MarkAllScenesDirty();
                }
            }
        }

        protected void ActiveSceneWindow()
        {
            EditorApplication.ExecuteMenuItem("Window/Scene");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Application.isPlaying)
                return;

            EditorGUI.BeginChangeCheck();
            script.isClose = EditorGUILayout.Toggle("Close", script.isClose);
            if (EditorGUI.EndChangeCheck())
            {
                script.CreateCurve();
                ActiveSceneWindow();
                EditorSceneManager.MarkAllScenesDirty();
            }
        }
        #endregion
    }
}