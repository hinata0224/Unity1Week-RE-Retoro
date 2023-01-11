using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Tetris_Block;

namespace Tetris_UI
{
    public class ResultControllerPresenter : MonoBehaviour
    {
        [SerializeField, Header("�Q�ƃX�N���v�g")]
        private BlockInstance block;

        [SerializeField]
        private ResultController controller;

        void Start()
        {
            block.GetGameOver()
                .Subscribe(x => 
                {
                    Debug.Log("ssss");
                    controller.GameOver(); 
                })
                .AddTo(block);
        }
    }
}