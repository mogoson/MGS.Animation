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
        protected Path Script { get { return target as Path; } }

        protected readonly Color blue = new Color(0, 1, 1, 1);

        protected const float nodeSize = 0.1f;
        protected const float buttonSize = 0.2f;
        protected const float buttonOffset = 0.35f;

#if UNITY_5_5_OR_NEWER
        protected readonly Handles.CapFunction SphereCap = Handles.SphereHandleCap;
#else
        protected readonly Handles.DrawCapFunction SphereCap = Handles.SphereCap;
#endif
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            if (Application.isPlaying)
                return;

            if (Script.anchors.Count == 0)
            {
                var handleSize = HandleUtility.GetHandleSize(Script.transform.position);
                var constOffset = handleSize * buttonOffset;
                var constSize = handleSize * buttonSize;

                Handles.color = Color.green;
                if (Handles.Button(Script.transform.position + new Vector3(constOffset, constOffset, constOffset), Quaternion.identity, constSize, constSize, SphereCap))
                {
                    Script.anchors.Insert(0, new Vector3(0, 0, handleSize));
                    Script.CreateCurve();
                    MarkSceneDirty();
                }
            }
            else
            {
                for (int i = 0; i < Script.anchors.Count; i++)
                {
                    var anchorPos = Script.transform.TransformPoint(Script.anchors[i]);
                    var handleSize = HandleUtility.GetHandleSize(anchorPos);

                    Handles.color = blue;
                    DrawSphereCap(anchorPos, Quaternion.identity, handleSize * nodeSize);

                    EditorGUI.BeginChangeCheck();
                    var position = Handles.PositionHandle(anchorPos, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Script.anchors[i] = Script.transform.InverseTransformPoint(position);
                        Script.CreateCurve();
                        MarkSceneDirty();
                    }

                    var constOffset = handleSize * buttonOffset;
                    var constSize = handleSize * buttonSize;

                    if (Event.current.control)
                    {
                        Handles.color = Color.green;
                        if (Handles.Button(anchorPos + new Vector3(constOffset, constOffset, constOffset), Quaternion.identity, constSize, constSize, SphereCap))
                        {
                            var anchorOffset = new Vector3(0, 0, handleSize);
                            if (i > 0)
                                anchorOffset = (Script.anchors[i] - Script.anchors[i - 1]).normalized * handleSize;

                            Script.anchors.Insert(i + 1, Script.anchors[i] + anchorOffset);
                            Script.CreateCurve();
                            MarkSceneDirty();
                        }
                    }
                    else if (Event.current.shift)
                    {
                        Handles.color = Color.red;
                        if (Handles.Button(anchorPos + new Vector3(constOffset, constOffset, constOffset), Quaternion.identity, constSize, constSize, SphereCap))
                        {
                            Script.anchors.RemoveAt(i);
                            Script.CreateCurve();
                            MarkSceneDirty();
                        }
                    }
                }
            }
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                Handles.SphereHandleCap(0, position, rotation, size, EventType.Repaint);
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
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Application.isPlaying)
                return;

            EditorGUI.BeginChangeCheck();
            Script.isClose = EditorGUILayout.Toggle("Close", Script.isClose);
            if (EditorGUI.EndChangeCheck())
            {
                Script.CreateCurve();
                SceneView.RepaintAll();
                MarkSceneDirty();
            }
        }
        #endregion
    }
}