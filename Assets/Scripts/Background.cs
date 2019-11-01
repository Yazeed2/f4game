using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This component manages a fullscreen vertically downward-scrolling background.
 * 
 * How to use:
 *  Attach at least one Sprite element to the sprites array.
 *  Modify the speed of movement in moveSpeed.
 * 
 * Terminology:
 *  Background object (bgObject): are GameObjects with a SpriteRenderer and a RigidBody2D.
 * 
 * Saud Bakolka
 * November 2019
 */

public class Background : MonoBehaviour
{
    // Background movement speed
    public float moveSpeed = 0.5f;
    // Sprites to be used for the background
    public Sprite[] sprites;

    // A list of background objects to be moved in the background
    private List<GameObject> bg = new List<GameObject>();

    // Dimentions of the main camera
    private float cameraHeight;
    private float cameraWidth;

    // Sets up the fullscreen scrolling background
    void Awake()
    {
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;
        
        // Creates the background out of every sprite
        foreach (Sprite sprite in sprites) this.appendNewBgObject(sprite);

        // Repeats some sprites if the background is not long enough to scroll
        for (int index = 0; !this.bgIsRepeatable(); index++)
        {
            this.appendNewBgObject(bg[index].GetComponent<SpriteRenderer>().sprite);

            if (index == bg.Count) index = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If bg reaches the upper limit of the camera
        if (this.bgUpperLimit() <= cameraHeight / 2)
        {
            // Reorders the first element to be displayed last
            this.addToBackground(this.popBgObject());
        }
    }

    ////////////////////////////////////////////////Helper methods/////////////
    
    // Creates a new bg Object and adds it to the background
    private void appendNewBgObject(Sprite sprite)
    {
        GameObject bgObject = this.createBgObject(sprite);

        this.scaleToCamera(bgObject);

        this.addToBackground(bgObject);
    }

    // Returns true if there are enough sprites to cover a scrolling background
    private bool bgIsRepeatable()
    {
        for (int ob = 0; ob < bg.Count; ob++)
        {
            float height = 0;

            for (int other = 0; other < bg.Count; other++)
                if (other != ob) height += this.getHeight(bg[other]);

            if (height < cameraHeight) return false;
        }

        return true;
    }

    // Creates a background object
    private GameObject createBgObject(Sprite sprite)
    {
        GameObject bgObject = new GameObject();

        // Adds a SpriteRenderer and includes the sprite
        SpriteRenderer render = bgObject.AddComponent<SpriteRenderer>();
        render.sprite = sprite;

        // Adds a RigidBody2D and includes movement data
        Rigidbody2D rb2d = bgObject.AddComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -moveSpeed);
        rb2d.isKinematic = true;

        return bgObject;
    }

    // Adds a bgObject to the bg and places it on top of the stack
    private void addToBackground(GameObject bgObject)
    {
        // Calculates the upper limit position of the object
        float bgObjectPosition = this.bgUpperLimit() + this.getHeight(bgObject) / 2;

        bgObject.transform.position = new Vector3(0, bgObjectPosition, 1f);

        bg.Add(bgObject);

        this.addChild(bgObject);
    }
    
    // Returns the height of the given object
    private float getHeight(GameObject bgObject)
    {
        return bgObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Returns the y position of the last GameObject in the background
    private float bgUpperLimit()
    {
        if (bg.Count == 0) return -cameraHeight/2;
        else return getUpperLimit(bg[bg.Count - 1]);
    }

    // Returns the length of the 
    private float bgLength()
    {
        float result = 0f;

        foreach (GameObject bgObject in bg)
        {
            result += bgObject.GetComponent<SpriteRenderer>().bounds.size.y;
        }

        return result;
    }

    // Returns the y position of the top edge of a GameObject
    private float getUpperLimit(GameObject bgObject)
    {
        float position = bgObject.transform.position.y;

        return position + this.getHeight(bgObject) / 2;
    }

    // Removes and returns the last element in bg (the list of bgObjects)
    private GameObject popBgObject()
    {
        GameObject toRemove = bg[0];
        bg.RemoveAt(0);
        return toRemove;
    }

    // Scales a game object with a sprite renderer to the width of the main camera
    private void scaleToCamera(GameObject bgObject)
    {
        // Calculates the scale of the object
        Vector2 spriteSize = bgObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector2 scale = bgObject.transform.localScale * cameraWidth / spriteSize.x;

        bgObject.transform.localScale = scale;
    }

    // Makes this the parent of a bgObject
    private void addChild(GameObject bgObject)
    {
        bgObject.transform.parent = this.transform;
    }
}
