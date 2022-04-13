using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  void OnCollisionEnter(Collision other)
  {
    Debug.Log($"{this.name} **collided with** {other.gameObject.name}");
    StartCrashSequence();
  }

  void OnTriggerEnter(Collider other)
  {
    Debug.Log($"{this.name} **triggered by** {other.gameObject.name}");
    StartCrashSequence();
  }

  void StartCrashSequence()
  {
    GetComponent<PlayerControls>().enabled = false;
    Invoke("ReloadLevel", 1f);
  }

  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
}
