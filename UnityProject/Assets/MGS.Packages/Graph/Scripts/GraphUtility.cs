/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GraphUtility.cs
 *  Description  :  Utility for unity Graph.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  10/04/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using UnityEngine;

namespace MGS.Graph
{
    /// <summary>
    /// Utility for unity Graph.
    /// </summary>
    public sealed class GraphUtility
    {
        /// <summary>
        /// Load gif from file.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="progress"></param>
        /// <param name="finished"></param>
        /// <returns></returns>
        public static IEnumerator LoadGifFromFile(string file,
            Action<float, Texture2D> progress, Action<List<Texture2D>> finished = null)
        {
            var frames = ImageUtility.GetFrames(file);
            var index = 0;
            var textures = new List<Texture2D>();
            foreach (var frame in frames)
            {
                var buffer = ImageUtility.GetBuffer(frame, ImageFormat.Png);
                var texture = new Texture2D(frame.Width, frame.Height);
                texture.LoadImage(buffer);
                textures.Add(texture);

                index++;
                if (progress != null)
                {
                    progress.Invoke((float)index / frames.Length, texture);
                }
                yield return null;
            }
            if (finished != null)
            {
                finished.Invoke(textures);
            }
        }
    }
}