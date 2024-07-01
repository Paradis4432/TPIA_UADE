using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        private static readonly List<Action> PowerUpListeners = new();
        
        public bool EnemiesFrozen { get; private set; }

        public static GameManager Instance { get; private set; }

        private void Awake() {
            EnemiesFrozen = false;
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void UsePowerUp() {
            StartCoroutine(FreezeEnemiesForSeconds(3));
            foreach (Action action in PowerUpListeners) {
                action();
            }
        }

        public static void RegisterListenerForPowerUp(Action action) {
            PowerUpListeners.Add(action);
        }

        private IEnumerator FreezeEnemiesForSeconds(float seconds) {
            EnemiesFrozen = true;
            yield return new WaitForSeconds(seconds);
            EnemiesFrozen = false;
        }
    }
}