using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] ParticleSystem explosion;

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
        explosion.Play();

        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
