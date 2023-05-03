using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector _playableDirector;
        private void Start() 
        {
            _playableDirector = GetComponent<PlayableDirector>();
            if (_playableDirector == null)
                Debug.LogError("CinematicControlRemover.cs: PlayableDirector is not found");

            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;
        }
        
        void DisableControl(PlayableDirector pd)
        {
            print("DisableControl");
        }

        void EnableControl(PlayableDirector pd)
        {
            print("EnableControl");
        }
    }
}




