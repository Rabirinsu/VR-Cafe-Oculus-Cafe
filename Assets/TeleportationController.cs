using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class TeleportationController : MonoBehaviour
{
    public RayInteractor rayinteractor;
     [SerializeField] private Transform playerTransform;
    private Vector3 TeleportPoint;
    private void Awake()
    {
      
    }

    public void TeleportPlayer()
    {
        playerTransform.position = rayinteractor.hitpoint;
        Debug.Log("TELEPORTED");
    }

   
}
