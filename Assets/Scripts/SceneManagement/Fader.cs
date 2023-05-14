using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup _canvasGroup;

        private void Start() 
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)    
                Debug.LogError("Fader.cs: CanvasGroup is not found!");

        }

        public IEnumerator FadeOut(float time) // time: the time it takes to perform a fadeOut
        {
            while(_canvasGroup.alpha < 1) //alpha is not 1
            {
                _canvasGroup.alpha += ((Time.deltaTime*1)/time); //1: max value of alpha
                yield return null; // basically tell unity, "this coroutine should be run again on the next possible opportunity (next frame)"

            }
        }

        public IEnumerator FadeIn(float time) // time: the time it takes to perform a fadeOut
        {
            while (_canvasGroup.alpha > 0) //alpha is not 1
            {
                _canvasGroup.alpha -= ((Time.deltaTime * 1) / time); //1: max value of alpha
                yield return null; // basically tell unity, "this coroutine should be run again on the next possible opportunity (next frame)"

            }
        }
    }
}
