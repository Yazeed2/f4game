using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public PlayerController pp;
     Rigidbody2D rb;
     public float speedx;
     public float speedy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // give the bullet physics to move
    }

    // Update is called once per frame
    void Update()
    {
       rb.velocity = new Vector2(speedx,speedy);
       transform.Rotate(new Vector3(0,0,pp.transform.rotation.z));
    }
}
