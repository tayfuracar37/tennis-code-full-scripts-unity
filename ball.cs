using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour
{
    Vector3 initialposition;
    public string hitter;

    int playerScore;
    int botScore;

      public Text playerscoretext;
    public  Text botscoretext;
    


    public bool playing = true;

    void Start()
    {
        initialposition = transform.position;
        playerScore=0;
        botScore = 0;
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialposition;
            GameObject.Find("player").GetComponent<player>().resetpositions();


            if (playing)
            {
                if (hitter == "player")
                {
                    botScore++;
                }
                else if (hitter == "bot")
                {
                    playerScore++;
                }
                playing = false;
            }
            
        }

        else if (collision.transform.CompareTag("out"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialposition;
            GameObject.Find("player").GetComponent<player>().resetpositions();


            if (playing)
            {
                if (hitter == "player")
                {
                    botScore++;
                }
                else if (hitter == "bot")
                {
                    playerScore++;
                }
                playing = false;
               // Uptadescore();
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("out")&&playing)
        {

            if (hitter == "player")
            {
                playerScore++;
            }
            else if (hitter == "bot")
            {
                botScore++;
            }
            playing = false;
        }
       // Uptadescore();
    }

    //void Uptadescore()
    //{
        //playerscoretext.text = "Player :" + playerScore;
        //botscoretext.text = "Bot :" + botScore;
    //}
}
