using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public bool thisIsActiveLevel;
    private bool isActivated;
    public GameObject checkPoint;
    public PolygonCollider2D camBounds;
    public float camOrthoGraphicSize;

}
