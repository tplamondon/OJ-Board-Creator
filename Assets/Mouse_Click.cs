using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mouse_Click : MonoBehaviour
{

    public Sprite[] list = new Sprite[21];
    public int spriteToDo = (int)TileEnum.HOME;


    //36.? x len, 36 y len
    readonly int xMax = 47;
    readonly int yMax = 30;
    int[,] tiles = null;
    GameObject[,] objects = null;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new int[xMax, yMax];
        //initialise empty
        for(int x=0; x<xMax; x++)
        {
            for(int y=0; y<yMax; y++)
            {
                tiles[x, y] = -1;
            }
        }

        objects = new GameObject[xMax, yMax];
        //initialise empty
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                objects[x, y] = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Pressed primary button.");
            if (Input.mousePosition.x > 200 || tiles == null)
            {
                //Debug.Log(Input.mousePosition.ToString());
            }
            int xPos = (int)Input.mousePosition.x;
            if (xPos < 200)
            {
                return;
            }
            xPos -= 200;
            xPos /= 36;
            int yPos = ((int)Input.mousePosition.y) / 36;

            if (xPos >= xMax || yPos >= yMax)
            {
                Debug.Log("Too far, would leave array");
                return;
            }
            tiles[xPos, yPos] = spriteToDo;
            

            

        }

        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Pressed primary button.");
            if (Input.mousePosition.x > 200 || tiles == null)
            {
                //Debug.Log(Input.mousePosition.ToString());
            }
            int xPos = (int)Input.mousePosition.x;
            if (xPos < 200)
            {
                return;
            }
            xPos -= 200;
            xPos /= 36;
            int yPos = ((int)Input.mousePosition.y) / 36;

            if (xPos >= xMax || yPos >= yMax)
            {
                Debug.Log("Too far, would leave array");
                return;
            }
            // remove the sprite
            tiles[xPos, yPos] = -1;
            if(objects[xPos,yPos] != null)
            {
                Destroy(objects[xPos, yPos]);
            }
        }
            
    }


    public void changeSprite()
    {
        var drop = GameObject.Find("TileChoose").GetComponent<Dropdown>();
        //Debug.Log(drop.value);
        spriteToDo = drop.value;
    }
}
