using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delay = 1f;

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;

        GetComponent<Rigidbody>().useGravity = true;
        Invoke("ReloadLevel", delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
