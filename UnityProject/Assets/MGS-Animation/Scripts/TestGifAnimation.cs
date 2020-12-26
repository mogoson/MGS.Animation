/*************************************************************************
 *  Copyright © 2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestGifAnimation.cs
 *  Description  :  Test play gif animation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/20/2019
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Graph;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.UAnimation
{
    [AddComponentMenu("MGS/Animation/TestGifAnimation")]
    [RequireComponent(typeof(RIFramesAnimation))]
    public class TestGifAnimation : MonoBehaviour
    {
        #region Field and Property
        public Button playBtn;
        public Scrollbar progressBar;

        private RIFramesAnimation fAnimation;
        private string gifFile;
        #endregion

        #region Private Method
        private void Awake()
        {
            fAnimation = GetComponent<RIFramesAnimation>();
            playBtn.onClick.AddListener(OnPlayBtnClick);

            gifFile = string.Format("{0}/Gif/Running.gif", Application.streamingAssetsPath);
        }

        private void OnPlayBtnClick()
        {
            playBtn.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(true);

            GraphUtility.LoadGifFromFileAsycn(gifFile, OnGifLoading, OnGifLoaded);
        }

        private void OnGifLoading(float progress, Texture2D frame)
        {
            progressBar.size = progress;
        }

        private void OnGifLoaded(List<Texture2D> frames)
        {
            progressBar.gameObject.SetActive(false);

            if (frames == null)
            {
                return;
            }

            var newFrames = frames.ConvertAll(item => { return item as Texture; });
            fAnimation.SetFrames(newFrames);
            fAnimation.Play();
        }
        #endregion
    }
}