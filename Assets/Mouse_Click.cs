using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mouse_Click : MonoBehaviour
{

    public Sprite[] spriteList = new Sprite[21];
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
            int x = (int)Input.mousePosition.x;
            int y = (int)Input.mousePosition.y;
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

            if (objects[xPos, yPos] != null)
            {
                Destroy(objects[xPos, yPos]);
            }

            //36.? x len, 36 y len
            

            //create the object
            GameObject obj = new GameObject();
            obj.AddComponent<SpriteRenderer>();
            obj.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToDo];

            var rect = GUIRectWithObject(obj);
            obj.GetComponent<Transform>().localScale = new Vector3(36/rect.width , 36/rect.height , 1);

            Vector3 point = new Vector3();
            point = Camera.main.ScreenToWorldPoint(new Vector3((xPos*36)+200, yPos*36, Camera.main.nearClipPlane));
            Debug.Log(point.x+", "+point.y);
            obj.transform.position = new Vector3(point.x, point.y, 0);
            objects[xPos, yPos] = obj;


            

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

    public static Rect GUIRectWithObject(GameObject go)
    {
        Vector3 cen = go.GetComponent<Renderer>().bounds.center;
        Vector3 ext = go.GetComponent<Renderer>().bounds.extents;
        Vector2[] extentPoints = new Vector2[8]
         {
               WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z-ext.z)),
               WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z-ext.z)),
               WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y-ext.y, cen.z+ext.z)),
               WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y-ext.y, cen.z+ext.z)),
               WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z-ext.z)),
               WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z-ext.z)),
               WorldToGUIPoint(new Vector3(cen.x-ext.x, cen.y+ext.y, cen.z+ext.z)),
               WorldToGUIPoint(new Vector3(cen.x+ext.x, cen.y+ext.y, cen.z+ext.z))
         };
        Vector2 min = extentPoints[0];
        Vector2 max = extentPoints[0];
        foreach (Vector2 v in extentPoints)
        {
            min = Vector2.Min(min, v);
            max = Vector2.Max(max, v);
        }
        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
    }

    public static Vector2 WorldToGUIPoint(Vector3 world)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(world);
        screenPoint.y = (float)Screen.height - screenPoint.y;
        return screenPoint;
    }

    public void changeSprite()
    {
        var drop = GameObject.Find("TileChoose").GetComponent<Dropdown>();
        //Debug.Log(drop.value);
        spriteToDo = drop.value;
    }
}
