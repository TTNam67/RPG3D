using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int _sceneIndexToLoad = -1;
        [SerializeField] Transform _spawnPoint;
        [SerializeField] DestinationIdentifier _destinationIdentifier;
        [SerializeField] float _fadeOutTime = 1.2f;
        [SerializeField] float _fadeInTime = 1.6f;
        [SerializeField] float _fadeWaitTime = 0.5f;
        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (_sceneIndexToLoad < 0) 
            {
                Debug.LogError("The sceneIndexToLoad is invalid");
                yield break;
            }
 
            DontDestroyOnLoad(this.gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(_fadeOutTime);
            yield return SceneManager.LoadSceneAsync(_sceneIndexToLoad); // Start load the new scene 

            Portal otherPortal = GetOtherPortal(); // The portal where we want to teleport to
            UpdatePlayer(otherPortal);


            // We do the setup: move the player,.. Then we waiting for small amount of time for the camera to stabilize 
            yield return new WaitForSeconds(_fadeWaitTime);
            yield return fader.FadeIn(_fadeInTime);

            Destroy(this.gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");

            /* Sometimes, the player is not updating correctly, maybe because you have multiple terrains in a scene.
            The reason for this is actually because we are updating the player's transform directly. But there is also a navMeshAgent 
            that is simultaneously trying update the position, and these two can sometimes conflict and your character can end up in completely 
            the wrong position.

            There are 2 ways to solve this problem:
            + Disable the player's NavMeshAgent before updating and enable it later
            */

            // 1st Solution
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal._spawnPoint.position;
            player.transform.rotation = otherPortal._spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

            //2nd Solution
            // player.GetComponent<NavMeshAgent>().Warp(otherPortal._spawnPoint.position);
            // player.transform.rotation = otherPortal._spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                // if we found the current portal, do nothing
                if (portal == this) continue; 
                if (portal._destinationIdentifier != this._destinationIdentifier) continue;

                return portal; // The portal we tending to teleport to
            }

            return null; 
        }
    }
}






