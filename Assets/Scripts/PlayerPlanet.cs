using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PlayerPlanet : MonoBehaviour
{
    bool hasHitObject = false;
    bool moveAllowed = false;
    bool hasShot = false;
    bool clicked = false;
    int bonusCounter = 0;
    float topOfSquare = -1.73f;
    float Xspeed = .1f;
    float Yspeed = .11f;
    Collider2D touchedCollider;
    Collision2D collisionTracker;
    List<GameObject> planetsLeft;
    public GameObject newPlayerPlanet;
    Sprite mySprite;
    Touch touch;
    
    Vector2 touchPosition;
    Vector2 currentVelocity;
    Collider2D col;
    ParticleSystem particles;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider2D>();
        mySprite = GetComponent<SpriteRenderer>().sprite;
        particles = GetComponent<ParticleSystem>();
        planetsLeft = GameObject.FindGameObjectsWithTag("RoamingPlanet").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            //Touch code.
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (touch.phase == TouchPhase.Began)
            {
                touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (col == touchedCollider && touchPosition.y < topOfSquare)
                {
                    moveAllowed = true;
                }
            }
            if (touch.phase == TouchPhase.Moved && hasShot == false && moveAllowed == true)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
                if (transform.position.y > topOfSquare)
                {
                    //Do the fling.
                    hasShot = true;
                    currentVelocity = GetComponent<Rigidbody2D>().velocity;
                    //Establish minimum & maximum speed for shot.  Prevents shooting complete muffins.
                    if (currentVelocity.x < .1)
                    {
                        currentVelocity.x = .1f;
                    } 
                    if (currentVelocity.x > 1f)
                    {
                        currentVelocity.x = 1;
                    }
                    if (currentVelocity.y < .1)
                    {
                        currentVelocity.y = .1f;
                    }
                    if(currentVelocity.y > 1f)
                    {
                        currentVelocity.y = 1;
                    }
                    GetComponent<Rigidbody2D>().velocity = new Vector2(touchDeltaPosition.x * Xspeed, touchDeltaPosition.y * Yspeed);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
            }
        }

        if(transform.position.y < -7)
        {
            Destroy(GameObject.FindGameObjectWithTag("PlayerPlanet"));
            GameManagement.Score(bonusCounter * planetsLeft.Count());
            if (GameManagement.lives <= 0)
            {
                GameManagement.totalLevels += GameManagement.level;
                SceneManager.LoadScene("GameOver");
            }
            GameManagement.lives--;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        collisionTracker = col;
        //We've hit a planet, is it a match?
        if (mySprite == col.gameObject.GetComponent<SpriteRenderer>().sprite && hasHitObject == true)
        {
            
            Destroy(col.gameObject);
            Destroy(gameObject);
            planetsLeft.Remove(collisionTracker.gameObject);
            GameManagement.Score((GameObject.FindGameObjectsWithTag("RoamingPlanet").Length + 1) * bonusCounter);
            
        } else
        {
            particles.Emit(Random.Range(5, (int)System.Math.Ceiling(col.relativeVelocity.magnitude)));
            hasHitObject = true;
            if (col.gameObject.tag == "RoamingPlanet")
            {
                bonusCounter *= 2;
            }
            else
            {
                bonusCounter++;
                if (col.gameObject.GetComponent<SpriteRenderer>().sprite == gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                    bonusCounter++;
                    bonusCounter *= 4;
                }
            }
        }
    }
}
