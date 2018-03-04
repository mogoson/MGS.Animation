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
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (Application.isPlaying)
                return;

            if (Target.AnchorsCount == 0)
            {
                var handleSize = HandleUtility.GetHandleSize(Target.transform.position);
                Target.InsertAnchor(0, Vector3.one * handleSize * 0.5f);
            }
            else
            {
                for (int i = 0; i < Target.AnchorsCount; i++)
                {
                    var anchorItem = Target.GetAnchorAt(i);
                    var handleSize = HandleUtility.GetHandleSize(anchorItem);
                    var constSize = handleSize * AnchorSize;

                    if (Event.current.alt)
                    {
                        Handles.color = Color.green;
                        if (Handles.Button(anchorItem, Quaternion.identity, constSize, constSize, SphereCap))
                        {
                            var offset = Vector3.forward * handleSize;
                            if (i > 0)
                                offset = (anchorItem - Target.GetAnchorAt(i - 1)).normalized * handleSize;

                            Undo.RecordObject(Target, "Insert Anchor");
                            Target.InsertAnchor(i + 1, anchorItem + offset);
                            Target.Rebuild();
                            MarkSceneDirty();
                        }
                    }
                    else if (Event.current.shift)
                    {
                        Handles.color = Color.red;
                        if (Handles.Button(anchorItem, Quaternion.identity, constSize, constSize, SphereCap))
                        {
                            Undo.RecordObject(Target, "Remove Anchor");
                            Target.RemoveAnchorAt(i);
                            Target.Rebuild();
                            MarkSceneDirty();
                        }
                    }
                    else
                    {
                        Handles.color = Blue;
                        EditorGUI.BeginChangeCheck();
                        var position = Handles.FreeMoveHandle(anchorItem, Quaternion.identity, constSize, MoveSnap, SphereCap);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(Target, "Change Anchor Position");
                            Target.SetAnchorAt(i, position);
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