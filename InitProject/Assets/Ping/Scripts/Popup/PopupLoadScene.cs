using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace Ping
{
    public class PopupLoadScene : MonoBehaviour
    {
        AsyncOperation operation;
        public Slider slider;
        public Text txtSlider;
        public void Init(string nextScene)
        {
            gameObject.SetActive(true);
            operation = SceneManager.LoadSceneAsync(nextScene);
        }

        void Update()
        {
            if (operation != null)
            {
                slider.value = operation.progress;
                int _value = (int)(operation.progress * 100);
                txtSlider.text = "Đang tải " + _value + "%";
                if (operation.isDone)
                {
                    operation = null;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}