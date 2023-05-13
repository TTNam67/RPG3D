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
            yield return SceneManager.LoadSceneAsync(_sceneIndexToLoad); 

            Portal otherPortal = GetOtherPortal(); // The portal where we want to teleport to
            UpdatePlayer(otherPortal);

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






