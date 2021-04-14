using UnityEngine;
using System.Collections;
using System;

public class RoamingPlanet : MonoBehaviour {

    float minY = -1.25f;
    float speed;
    float currentTime;
    float timeSinceLastHit;
    float currentX;
    float currentY;
    private ParticleSystem particles;
    AudioSource clip;

	// Use this for initialization
	void Start () {
        speed = UnityEngine.Random.Range(1, 4 + (GameManagement.level/2));
        GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1.1f)) * speed;
        particles = GetComponent<ParticleSystem>();
        clip = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        currentY = GetComponent<Rigidbody2D>().velocity.y;
        currentX = GetComponent<Rigidbody2D>().velocity.x;
        
        //Did we hit the bottom of the play area? Rebound the planet.  Time check makes sure it only happens once in two seconds.
        if (GetComponent<Rigidbody2D>().transform.position.y < minY && ((currentTime - timeSinceLastHit) > 0.2))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(currentX, -currentY);
            timeSinceLastHit = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        particles.Emit(UnityEngine.Random.Range(5, (int)Math.Ceiling(col.relativeVelocity.magnitude)));
        if (col.gameObject.tag == "RoamingPlanet")
        {
            clip.Play();
        }
    }
}
