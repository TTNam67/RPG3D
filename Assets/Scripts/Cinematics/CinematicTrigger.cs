using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private PlayableDirector _playableDirector;
        bool _alreadyTriggered = false;

        private void OnEnable()
        {
            _playableDirector.Stop();
        }

        private void Start() 
        {
            _playableDirector = GetComponent<PlayableDirector>();
            if (_playableDirector == null)    
                Debug.LogError("CinematicTrigger.cs: PlayableDirector is not found");
        }
        private void Awake() 
        {

        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag == "Player" && _alreadyTriggered == false)
            {
                _playableDirector.Play();
                _alreadyTriggered = true;
            }    
        }
    }
}
