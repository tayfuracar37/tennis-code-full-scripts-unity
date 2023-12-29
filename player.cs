using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniversalMobileController;

public class player : MonoBehaviour
{
    //topa vurduðumda nereye gitmesini söyleyen bir hedef belirledim
    public Transform aimtarget;

    //player hýzý deðiþken
    public float speed=7.5f;
    public Transform ball;


    // vuruþ gücü için deðiþken
    float force = 14f;

    //topa vurup vurmadýðýmý kontrol etmek için bir boolaen oluþturdurdum
    bool hitting;

    Animator anim;

    Vector3 aimtargetinitialpos;

    ShotManager shotmanager;
    Shot currentshot;

    [SerializeField] Transform serveRight;
    [SerializeField] Transform serveLeft;
    bool servedRight = true;

    // joystick kontrolü için deðiþken
    public FloatingJoyStick joyStick;
    public FloatingJoyStick joyStick1;
    

    //sürekli basýlý tutma f ye
    bool isPressed=true;


  



    void Start()
    {
        anim = GetComponent<Animator>();
        aimtargetinitialpos = aimtarget.position;
        shotmanager = GetComponent<ShotManager>();
        currentshot = shotmanager.topspin;
    }

   
    void Update()
    {
        


        //klavyeden hareket için tuþ girdilerini aldým(A-S-D-W)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        

        
        

        //f ye basýldýðýnda vurup vurmadýðýný göre vuruþ durumunu güncelleme
        if (isPressed)
        {
            hitting = true;
            currentshot = shotmanager.topspin;
        }
        else if (isPressed) 
        {
            hitting = false;
        }
        //
        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true;
            currentshot = shotmanager.flat;
        }
        else if (Input.GetKeyUp(KeyCode.E)) 
        {
            hitting = false;
        }

        //
         if (Input.GetKeyDown(KeyCode.R))
         {
            hitting = true;
            currentshot = shotmanager.flatserve;
            GetComponent<BoxCollider>().enabled = false;
            anim.Play("serve");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            hitting = true;
            currentshot = shotmanager.kickserve;
            GetComponent<BoxCollider>().enabled = false;
            anim.Play("serve");
        }



        if (Input.GetKeyUp(KeyCode.R)|| Input.GetKeyUp(KeyCode.T)) 
        {
            hitting = false;
            GetComponent<BoxCollider>().enabled = true;
            ball.transform.position = transform.position + new Vector3(0.2f, 1.5f, 0);
            Vector3 dir = aimtarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * currentshot.hitforce + new Vector3(0, currentshot.upforce, 0);
            ball.GetComponent<ball>().hitter = "player";
            ball.GetComponent<ball>().playing = true;
        }
         
          
        


        //sorgulamdan geçip true deðerini aldýysa hedefe atmak için yeni vector
        // mathf fonksiyonu ile sýnýrlandýrdým
        // mathf fonkdiyonu yerinde horizontal deðiþkeni vardý DÝKKAT!!!!
        if (hitting)
        {

            aimtarget.Translate(new Vector3(Mathf.Clamp(joyStick1.GetHorizontalValue(),-6.61f,5.59f), 0, 0) * speed*2 * Time.deltaTime);
            Vector3 balldir = ball.position - transform.position;
        }
        


        // eðer tuþlara basýlmama olayý sýfýrdan farklýysa yani basýlýyorsa hareket et
        
        // ÝLERÝDE SORUN ÇIKARTABÝLÝR DÝKKAT DÝKKAT DÝKKAT
        //if((h!=0||v!=0) && !hitting)
        {
            transform.Translate(new Vector3(joyStick.GetHorizontalValue()*Time.deltaTime*speed, 0, joyStick.GetVerticalValue())*Time.deltaTime*speed);
        }

        

    }
    

    // top player ile temasa geçerse topa kuvvet uygular ve top karþýya geçer
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 dir = aimtarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized*currentshot.hitforce+new Vector3(0,currentshot.upforce,0);

            Vector3 balldir = ball.position - transform.position;

            //DÝKKAT HATA ÇIKARSA MUHTEMEL BLOK
            if (balldir.x > 0)

                anim.Play("forehand");

            else
                anim.Play("backhand");

        }
        ball.GetComponent<ball>().hitter = "player";
        aimtarget.position = aimtargetinitialpos;
    }


    /*public void Reset()
    {
        if (servedRight)
            transform.position = serveRight.position;
        else
            transform.position = serveRight.position;

        servedRight = !servedRight;

    }*/

    public void resetpositions()
    {
        transform.position = new Vector3(0.079813f, 1.1967f, 207.4548f);
    }
}
