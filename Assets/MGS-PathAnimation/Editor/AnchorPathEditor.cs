/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AnchorPathEditor.cs
 *  DeTargetion  :  Editor for AnchorPath.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.PathAnimation
{
    [CustomEditor(typeof(AnchorPath), true)]
    public class AnchorPathEditor : CurvePathEditor
    {
        #region Field and Property
        protected new AnchorPath Target { get { return target as AnchorPath; } }
        protected const float AnchorSize = 0.125f;
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (Application.isPlaying)
                return;

            if (Target.Anchors.Count == 0)
            {
                var handleSize = HandleUtility.GetHandleSize(Target.transform.position);
                Target.Anchors.Insert(0, Vector3.one * handleSize * 0.5f);
            }
            else
            {
                for (int i = 0; i < Target.Anchors.Count; i++)
                {
                    var anchorPos = Target.transform.TransformPoint(Target.Anchors[i]);
                    var handleSize = HandleUtility.GetHandleSize(anchorPos);
                    var constSize = handleSize * AnchorSize;

                    if (Event.current.alt)
                    {
                        Handles.color = Color.green;
                        if (Handles.Button(anchorPos, Quaternion.identity, constSize, constSize, SphereCap))
                        {
                            var anchorOffset = Vector3.forward * handleSize;
                            if (i > 0)
                                anchorOffset = (Target.Anchors[i] - Target.Anchors[i - 1]).normalized * handleSize;

                            Undo.RecordObject(Target, "Insert Anchor");
                            Target.Anchors.Insert(i + 1, Target.Anchors[i] + anchorOffset);
                            Target.Rebuild();
                            MarkSceneDirty();
                        }
                    }
                    else if (Event.current.shift)
                    {
                        Handles.color = Color.red;
                        if (Handles.Button(anchorPos, Quaternion.identity, constSize, constSize, SphereCap))
                        {
                            Undo.RecordObject(Target, "Remove Anchor");
                            Target.Anchors.RemoveAt(i);
                            Target.Rebuild();
                            MarkSceneDirty();
                        }
                    }
                    else
                    {
                        Handles.color = Blue;
                        EditorGUI.BeginChangeCheck();
                        var position = Handles.FreeMoveHandle(anchorPos, Quaternion.identity, constSize, MoveSnap, SphereCap);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(Target, "Change Anchor Position");
                            Target.Anchors[i] = Target.transform.InverseTransformPoint(position);
                            Target.Rebuild();
                            MarkSceneDirty();
                        }
                    }
                }
            }
        }
        #endregion
    }
}