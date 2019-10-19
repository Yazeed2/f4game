using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public float xspeed = 3f;//the speed in the x axis 
    public float yspeed = 0f;//the speed on the y axis 
    public float xslow = 1;// to slow down the x axis 
    public float yrotate = 0; //to let the object rotate to its original place :O
    public float enter = 0;
    public float go = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);





        if (up) {
            transform.Translate(0, xspeed * Time.deltaTime * xslow, yspeed * Time.deltaTime / 2);
            if (right)
            {
                transform.Rotate(0, 0, -2);
            }
            if (left)
            {
                transform.Rotate(0, 0, 2);
            }
        }
        if (down)
        {
            transform.Translate(0, xspeed * Time.deltaTime * xslow * -1 , yspeed * Time.deltaTime / 2);
        }
        if (right)
        {
            transform.Rotate(0, 0, -2);
        }
        if (left)
        {
            transform.Rotate(0, 0, 2);
        }
    }
}
