/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UVFrameAnimationEditor.cs
 *  Description  :  Editor for UVFrameAnimation component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Animations.Editors
{
    [CustomEditor(typeof(UVFrameAnimation), true)]
    public class UVFrameAnimationEditor : Editor
    {
        protected UVFrameAnimation Target { get { return target as UVFrameAnimation; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawAnimInspector();
        }

        protected virtual void DrawAnimInspector()
        {
            EditorGUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("Apply To UV Map"))
            {
                ApplyArgsToUVMap();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        protected void ApplyArgsToUVMap()
        {
            var mat = Target.GetComponent<Renderer>().sharedMaterial;
            mat.mainTextureOffset = Vector2.zero;
            mat.mainTextureScale = new Vector2(1.0f / Target.Column, 1.0f / Target.Row);
        }
    }
}