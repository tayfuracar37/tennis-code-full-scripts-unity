using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shot
{
    public float upforce;
    public float hitforce;
}

public class ShotManager : MonoBehaviour
{
    public Shot flatserve;
    public Shot kickserve;
    public Shot topspin;
    public Shot flat;
  
}
