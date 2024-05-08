using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 14;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public int pointValue;
    public ParticleSystem explosionParticle;
    
    
    // Start is called before the first frame update
    void Start()
    {
       targetRb = GetComponent<Rigidbody>();
       
       gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

       targetRb.AddForce(RandomForce(), ForceMode.Impulse);
       targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

       transform.position = RandomSpawnPos();
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Nuke"))
            {
                gameManager.GameOver();
            }
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good"))
        {
            gameManager.GameOver();
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }



}
