using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    public PlayerController pp;
    public GameObject bullet; //bullet which wil be shoting from player
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            shoting();
        }
    }

    public void shoting () {
        Instantiate(bullet,transform.position,Quaternion.Euler(transform.rotation.x,transform.rotation.y,0));
    }
}
