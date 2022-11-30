/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LoadGifDemo.cs
 *  Description  :  
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  11/30/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace MGS.Graph.Demo
{
    public class LoadGifDemo : MonoBehaviour
    {
        int index = 0;

        void Start()
        {
            var gifFile = string.Format("{0}/MGS.Packages/Graph/Demo/Textures/Running.gif", Application.dataPath);
            StartCoroutine(GraphUtility.LoadGifFromFile(gifFile, OnLoadProgress));
        }

        void OnLoadProgress(float progress, Texture2D texture)
        {
            transform.GetChild(index).GetComponent<RawImage>().texture = texture;
            index++;
        }
    }
}