/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GifAnimationDemo.cs
 *  Description  :  Test play gif animation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/20/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Graph;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Animations.Demo
{
    [RequireComponent(typeof(RawImageAnimation))]
    public class GifAnimationDemo : MonoBehaviour
    {
        public Button playBtn;
        public Scrollbar progressBar;
        private RawImageAnimation imgAnim;
        private string gifFile;

        private void Awake()
        {
            imgAnim = GetComponent<RawImageAnimation>();
            playBtn.onClick.AddListener(OnPlayBtnClick);
            gifFile = string.Format("{0}/MGS.Packages/Animation/Demo/Textures/Running.gif", Application.dataPath);
        }

        private void OnPlayBtnClick()
        {
            playBtn.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(true);
            StartCoroutine(GraphUtility.LoadGifFromFile(gifFile, OnGifLoading, OnGifLoaded));
        }

        private void OnGifLoading(float progress, Texture2D tex)
        {
            progressBar.size = progress;
        }

        private void OnGifLoaded(List<Texture2D> texs)
        {
            progressBar.gameObject.SetActive(false);

            var frames = texs.ConvertAll(item => { return item as Texture; });
            imgAnim.SetFrames(frames);
            imgAnim.Play();
        }
    }
}