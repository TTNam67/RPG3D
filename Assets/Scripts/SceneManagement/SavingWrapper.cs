using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string _defaultSavedFile = "save";
        SavingSystem _savingSystem;

        private void Start() 
        {
            _savingSystem = GetComponent<SavingSystem>();
            if (_savingSystem == null)
                Debug.LogWarning("SavingWrapper.cs: saving system is not found");    
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        private void Save()
        {
            _savingSystem.Save(_defaultSavedFile);
        }

        private void Load()
        {
            _savingSystem.Load(_defaultSavedFile);
        }
    }
}
