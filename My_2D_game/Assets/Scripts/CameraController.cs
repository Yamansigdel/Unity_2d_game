using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player; 
    
   private void Update()
    {
        //It maintains the main camera connected with player position
        transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);
    }
}
