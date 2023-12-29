using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scne_changer : MonoBehaviour
{
   
    public void scenechange(int sahne_id)
    {
        SceneManager.LoadScene(sahne_id);
    }
}
