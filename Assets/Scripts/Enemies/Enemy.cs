using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 1;
    [SerializeField] int scorePerDeath = 5;
    [SerializeField] int healthPoints = 5;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindGameObjectsWithTag("Spawn_At_Runtime")[0];
        addRigidBody();
    }

    void addRigidBody()
    {
        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (healthPoints < 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        GameObject fx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;

        scoreBoard.IncreaseScore(scorePerHit);

        healthPoints--;
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;

        scoreBoard.IncreaseScore(scorePerDeath);

        Destroy(gameObject);
    }
}
