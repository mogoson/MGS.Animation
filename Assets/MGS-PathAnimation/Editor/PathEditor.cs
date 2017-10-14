/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  PathEditor.cs
 *  Description  :  Editor for path.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/5/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace Developer.PathAnimation
{
    [CustomEditor(typeof(Path), true)]
    [CanEditMultipleObjects]
    public class PathEditor : Editor
    {
        #region Property and Field
        protected Path script { get { return target as Path; } }

        protected Color blue = new Color(0, 1, 1, 1);
        protected float nodeSize = 0.1f;
        protected float buttonOffset = 0.5f;

#if UNITY_5_5_OR_NEWER
        protected Handles.CapFunction SphereCap = Handles.SphereHandleCap;
#else
        protected Handles.DrawCapFunction SphereCap = Handles.SphereCap;
#endif
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
                DrawSphereCap(anchorPos, Quaternion.identity, handleSize * nodeSize);

                EditorGUI.BeginChangeCheck();
                var position = Handles.PositionHandle(anchorPos, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    script.anchors[i] = script.transform.InverseTransformPoint(position);
                    script.CreateCurve();
                    MarkSceneDirty();
                }

                var buttonSize = handleSize * nodeSize * 2;
                Handles.color = Color.green;
                if (Handles.Button(anchorPos + new Vector3(0, 0, handleSize * buttonOffset), Quaternion.identity, buttonSize, buttonSize, SphereCap))
                {
                    var anchorOffset = new Vector3(0, 0, handleSize);
                    if (i > 0)
                        anchorOffset = (script.anchors[i] - script.anchors[i - 1]).normalized * handleSize;
                    script.anchors.Insert(i + 1, script.anchors[i] + anchorOffset);
                    script.CreateCurve();
                    MarkSceneDirty();
                }

                Handles.color = Color.red;
                if (Handles.Button(anchorPos + new Vector3(handleSize * buttonOffset, 0, 0), Quaternion.identity, buttonSize, buttonSize, SphereCap))
                {
                    script.anchors.RemoveAt(i);
                    script.CreateCurve();
                    MarkSceneDirty();
                }
            }

            if (script.anchors.Count == 0)
            {
                var handleSize = HandleUtility.GetHandleSize(script.transform.position);
                var buttonSize = handleSize * nodeSize * 2;

                Handles.color = Color.green;
                if (Handles.Button(script.transform.position + new Vector3(0, 0, handleSize * buttonOffset), Quaternion.identity, buttonSize, buttonSize, SphereCap))
                {
                    script.anchors.Insert(0, new Vector3(0, 0, handleSize));
                    script.CreateCurve();
                    MarkSceneDirty();
                }
            }
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            Handles.SphereHandleCap(0, position, rotation, size, EventType.Ignore);
#else
            Handles.SphereCap(0, position, rotation, size);
#endif
        }

        protected void MarkSceneDirty()
        {
#if UNITY_5_3_OR_NEWER
            EditorSceneManager.MarkAllScenesDirty();
#else
            EditorApplication.MarkSceneDirty();
#endif
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
                MarkSceneDirty();
            }
        }
        #endregion
    }
}