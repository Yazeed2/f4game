using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInit : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;
    public int bulletNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(bulletNum>=0){
        Instantiate(bullet,bulletPos.position,Quaternion.Euler(0,0,0));
    }
    }
}
