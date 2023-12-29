using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bot : MonoBehaviour
{
    float speed=30;
    Animator anim;
    public Transform ball;
    public Transform aimtargetbot;

    public Transform[] targets;
    ShotManager shotManager;

    
    float force = 13f;

    
    

    Vector3 targetpositions;
    void Start()
    {
        targetpositions = transform.position;
        anim = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
    }

   
    void Update()
    {
        Move();
    }

    Vector3 picktarget()
    {
        int randomvalue = Random.Range(0, targets.Length);
        return targets[randomvalue].position;

    }
    Shot pickshot()
    {
        int randomvalue = Random.Range(0, 2);
        if (randomvalue == 0)
            return shotManager.topspin;
        else 
            return shotManager.flat;

    }

    void Move()
    {
        targetpositions.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetpositions, speed * Time.deltaTime);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Shot currentshot = pickshot();
            Vector3 dir = picktarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentshot.hitforce + new Vector3(0, currentshot.upforce, 0);

            Vector3 balldir = ball.position - transform.position;

            //DÝKKAT HATA ÇIKARSA MUHTEMEL BLOK
            if (balldir.x > 0)
            {
                anim.Play("forehand");
            }
            else
            {
                anim.Play("backhand");
            }
                

            ball.GetComponent<ball>().hitter = "bot";


        }
    }
}
