using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector _playableDirector;
        private GameObject _player;
        private void Start() 
        {
            _playableDirector = GetComponent<PlayableDirector>();
            if (_playableDirector == null)
                Debug.LogError("CinematicControlRemover.cs: PlayableDirector is not found");

            _player = GameObject.FindWithTag("Player");
            if (_player == null)
                Debug.LogError("CinematicController.cs: player is not found");

            // We cant control the player while the cut scene is playing
            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;
        }
        
        void DisableControl(PlayableDirector pd)
        {

            // When we hit the CutScene trigger, the current action will be disabled
            _player.GetComponent<ActionScheduler>().CancelCurrentAction();
            _player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }
    }
}




