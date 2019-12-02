using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mouse_Click : MonoBehaviour
{

    public Sprite[] spriteList = new Sprite[23];
    public int spriteToDo = (int)TileEnum.HOME;

    public const int LEFT = 0;
    public const int UP = 1;
    public const int DOWN = 2;
    public const int RIGHT = 3;

    readonly int xMax = 30;
    readonly int yMax = 20;
    int[,] tiles = null;
    GameObject[,] objects = null;
    GameObject[,,] arrows = null;
    bool[,,] arrowTiles = null;

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

        arrows = new GameObject[xMax, yMax, 4];
        //initialise empty
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                for(int z = 0; z<4; z++)
                {
                    arrows[x, y, z] = null;
                }
                
            }
        }

        arrowTiles = new bool[xMax, yMax, 4];
        //initialise empty
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    arrowTiles[x, y, z] = false;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        /******************************
         * LEFT CLICKED
         *******************************/
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

            var point = Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane));

            int xLoc = (int)Mathf.RoundToInt(point.x);
            int yLoc = (int)Mathf.RoundToInt(point.y);

            if (xLoc >= xMax / 2 || yLoc >= yMax / 2 || xLoc <= -xMax / 2 || yLoc <= -yMax / 2)
            {
                Debug.Log("Too far, would leave array");
                return;
            }

            //if it's a regular tile
            if (spriteToDo != (int)TileEnum.LEFT && spriteToDo != (int)TileEnum.UP && spriteToDo != (int)TileEnum.DOWN && spriteToDo != (int)TileEnum.RIGHT)
            {
                //set tile as proper tile (for png conversion later)
                tiles[xLoc + xMax / 2, yLoc + yMax / 2] = spriteToDo;

                //destroy old object
                if (objects[xLoc + xMax / 2, yLoc + yMax / 2] != null)
                {
                    Destroy(objects[xLoc + xMax / 2, yLoc + yMax / 2]);
                }

                //create the object
                GameObject obj = new GameObject();
                obj.AddComponent<SpriteRenderer>();
                obj.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToDo];

                float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
                float objHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
                //Debug.Log("Width: "+objWidth);
                obj.GetComponent<Transform>().localScale = new Vector3(1 / objWidth, 1 / objHeight, 1);

                //Debug.Log(pointOld.x+", "+ pointOld.y);
                obj.transform.position = new Vector3(xLoc, yLoc, 0);
                objects[xLoc + xMax / 2, yLoc + yMax / 2] = obj;
                Debug.Log("xLoc = " + xLoc);
                Debug.Log("yLoc = " + yLoc);
            }
            //else, it's an arrow
            else
            {
                //if there's no tile or it's a deck
                if (tiles[xLoc + xMax / 2, yLoc + yMax / 2] == -1 || tiles[xLoc + xMax / 2, yLoc + yMax / 2] == (int)TileEnum.DECK)
                {
                    return;
                }

                if (spriteToDo == (int)TileEnum.LEFT)
                {
                    //if there's a tile beside us
                    if(isValidIndexNotDeck(xLoc + xMax / 2 - 1, yLoc + yMax / 2) == true){
                        if(tiles[xLoc + xMax / 2 - 1, yLoc + yMax / 2] == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    arrowTiles[xLoc + xMax / 2, yLoc + yMax / 2, LEFT] = true;
                    //destroy old object
                    if (arrows[xLoc + xMax / 2, yLoc + yMax / 2, LEFT] != null)
                    {
                        Destroy(arrows[xLoc + xMax / 2, yLoc + yMax / 2, LEFT]);
                    }
                    //create the object
                    GameObject obj = new GameObject();
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToDo];

                    float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
                    float objHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
                    //Debug.Log("Width: "+objWidth);
                    //obj.GetComponent<Transform>().localScale = new Vector3(1 / objWidth, 1 / objHeight, 1);

                    //Debug.Log(pointOld.x+", "+ pointOld.y);
                    obj.transform.position = new Vector3(xLoc - 0.5f, yLoc, -0.1f);
                    arrows[xLoc + xMax / 2, yLoc + yMax / 2, LEFT] = obj;
                }
                if (spriteToDo == (int)TileEnum.UP)
                {
                    //if there's a tile beside us
                    if (isValidIndexNotDeck(xLoc + xMax / 2, yLoc + yMax / 2 + 1) == true)
                    {
                        if (tiles[xLoc + xMax / 2, yLoc + yMax / 2 + 1] == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    arrowTiles[xLoc + xMax / 2, yLoc + yMax / 2, UP] = true;
                    //destroy old object
                    if (arrows[xLoc + xMax / 2, yLoc + yMax / 2, UP] != null)
                    {
                        Destroy(arrows[xLoc + xMax / 2, yLoc + yMax / 2, UP]);
                    }
                    //create the object
                    GameObject obj = new GameObject();
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToDo];

                    float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
                    float objHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
                    //Debug.Log("Width: "+objWidth);
                    //obj.GetComponent<Transform>().localScale = new Vector3(1 / objWidth, 1 / objHeight, 1);

                    //Debug.Log(pointOld.x+", "+ pointOld.y);
                    obj.transform.position = new Vector3(xLoc, yLoc + 0.5f, -0.1f);
                    arrows[xLoc + xMax / 2, yLoc + yMax / 2, UP] = obj;
                }
                if (spriteToDo == (int)TileEnum.DOWN)
                {
                    //if there's a tile beside us
                    if (isValidIndexNotDeck(xLoc + xMax / 2, yLoc + yMax / 2 -1) == true)
                    {
                        if (tiles[xLoc + xMax / 2, yLoc + yMax / 2 -1] == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    arrowTiles[xLoc + xMax / 2, yLoc + yMax / 2, DOWN] = true;
                    //destroy old object
                    if (arrows[xLoc + xMax / 2, yLoc + yMax / 2, DOWN] != null)
                    {
                        Destroy(arrows[xLoc + xMax / 2, yLoc + yMax / 2, DOWN]);
                    }
                    //create the object
                    GameObject obj = new GameObject();
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToDo];

                    float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
                    float objHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
                    //Debug.Log("Width: "+objWidth);
                    //obj.GetComponent<Transform>().localScale = new Vector3(1 / objWidth, 1 / objHeight, 1);

                    //Debug.Log(pointOld.x+", "+ pointOld.y);
                    obj.transform.position = new Vector3(xLoc, yLoc - 0.5f, -0.1f);
                    arrows[xLoc + xMax / 2, yLoc + yMax / 2, DOWN] = obj;
                    Debug.Log("xLoc = " + xLoc);
                    Debug.Log("yLoc = " + yLoc);
                }
                if (spriteToDo == (int)TileEnum.RIGHT)
                {
                    //if there's a tile beside us
                    if (isValidIndexNotDeck(xLoc + xMax / 2 + 1, yLoc + yMax / 2) == true)
                    {
                        if (tiles[xLoc + xMax / 2 + 1, yLoc + yMax / 2] == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    arrowTiles[xLoc + xMax / 2, yLoc + yMax / 2, RIGHT] = true;
                    //destroy old object
                    if (arrows[xLoc + xMax / 2, yLoc + yMax / 2, RIGHT] != null)
                    {
                        Destroy(arrows[xLoc + xMax / 2, yLoc + yMax / 2, RIGHT]);
                    }
                    //create the object
                    GameObject obj = new GameObject();
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = spriteList[spriteToDo];

                    float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
                    float objHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
                    //Debug.Log("Width: "+objWidth);
                    //obj.GetComponent<Transform>().localScale = new Vector3(1 / objWidth, 1 / objHeight, 1);

                    //Debug.Log(pointOld.x+", "+ pointOld.y);
                    obj.transform.position = new Vector3(xLoc + 0.5f, yLoc, -0.1f);
                    arrows[xLoc + xMax / 2, yLoc + yMax / 2, RIGHT] = obj;
                    Debug.Log("xLoc = " + xLoc);
                    Debug.Log("yLoc = " + yLoc);
                }

            }
        }
        /******************************
         * RIGHT CLICKED
         *******************************/
        if (Input.GetMouseButtonDown(1))
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

            var point = Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane));

            int xLoc = (int)Mathf.RoundToInt(point.x);
            int yLoc = (int)Mathf.RoundToInt(point.y);

            if (xLoc >= xMax / 2 || yLoc >= yMax / 2 || xLoc <= -xMax / 2 || yLoc <= -yMax / 2)
            {
                Debug.Log("Too far, would leave array");
                return;
            }

            //destroy objects
            destroyTileAndArrows(xLoc + xMax / 2, yLoc + yMax / 2);

        }

        if(Input.mouseScrollDelta.y > 0)
        {
            scrollIn();
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            scrollOut();
        }
    }

    /******************************
     * SCROLLING
     *******************************/
    public void scrollIn()
    {
        if(Camera.main.GetComponent<Camera>().orthographicSize == 1)
        {
            return;
        }
        Camera.main.GetComponent<Camera>().orthographicSize--;
    }

    public void scrollOut()
    {
        if (Camera.main.GetComponent<Camera>().orthographicSize == 15)
        {
            return;
        }
        Camera.main.GetComponent<Camera>().orthographicSize++;
    }

    public void destroyTileAndArrows(int x, int y)
    {
        //if there's a tile
        if (objects[x, y] != null)
        {
            Destroy(objects[x, y]);
        }
        //set to null
        objects[x, y] = null;
        //set that tile to -1 to mark as not used
        tiles[x, y] = -1;
        for(int z=0; z<4; z++)
        {
            //if there's an arrow
            if(arrows[x,y,z] != null)
            {
                Destroy(arrows[x, y, z]);
            }
            //set to null
            arrows[x, y, z] = null;
            arrowTiles[x, y, z] = false;
        }
    }

    /********************
     *  valid indexes
     *******************/
    public bool isValidIndex(int x, int y)
    {
        if (x < 0 || y < 0 || x >= xMax || y >= yMax)
        {
            return false;
        }
        return true;
    }

    public bool isValidIndexNotDeck(int x, int y)
    {
        if (x < 0 || y < 0 || x >= xMax || y >= yMax)
        {
            return false;
        }
        if(tiles[x, y] != (int)TileEnum.DECK)
        {
            return false;
        }
        return true;
    }


    /********************
     *  changing sprite
     *******************/

    public void changeSprite()
    {
        var drop = GameObject.Find("TileChoose").GetComponent<Dropdown>();
        //Debug.Log(drop.value);
        string name = drop.options[drop.value].text;
        Debug.Log(name);
        switch (name)
        {
            case "Home":
                spriteToDo = (int)TileEnum.HOME;
                break;
            case "Star":
                spriteToDo = (int)TileEnum.STAR;
                break;
            case "Star x2":
                spriteToDo = (int)TileEnum.STAR2;
                break;
            case "Draw":
                spriteToDo = (int)TileEnum.DRAW;
                break;
            case "Draw x2":
                spriteToDo = (int)TileEnum.DRAW2;
                break;
            case "Battle":
                spriteToDo = (int)TileEnum.BATTLE;
                break;
            case "Battle x2":
                spriteToDo = (int)TileEnum.BATTLE2;
                break;
            case "Drop":
                spriteToDo = (int)TileEnum.DROP;
                break;
            case "Drop x2":
                spriteToDo = (int)TileEnum.DROP2;
                break;
            case "Warp":
                spriteToDo = (int)TileEnum.WARP;
                break;
            case "Warp Move":
                spriteToDo = (int)TileEnum.WARPMOVE;
                break;
            case "Warp Move x2":
                spriteToDo = (int)TileEnum.WARPMOVE2;
                break;
            case "Move":
                spriteToDo = (int)TileEnum.MOVE;
                break;
            case "Move x2":
                spriteToDo = (int)TileEnum.MOVE2;
                break;
            case "Blank":
                spriteToDo = (int)TileEnum.BLANK;
                break;
            case "Ice":
                spriteToDo = (int)TileEnum.ICE;
                break;
            case "Heal":
                spriteToDo = (int)TileEnum.HEAL;
                break;
            case "Heal x2":
                spriteToDo = (int)TileEnum.HEAL2;
                break;
            case "Left Arrow":
                spriteToDo = (int)TileEnum.LEFT;
                break;
            case "Up Arrow":
                spriteToDo = (int)TileEnum.UP;
                break;
            case "Down Arrow":
                spriteToDo = (int)TileEnum.DOWN;
                break;
            case "Right Arrow":
                spriteToDo = (int)TileEnum.RIGHT;
                break;
            case "Deck":
                spriteToDo = (int)TileEnum.DECK;
                break;
            default:
                spriteToDo = (int)TileEnum.HOME;
                break;

        }
        //spriteToDo = drop.value;
    }

    /********************
     *  outputting PNG
     *******************/ 

    public void outputPNG()
    {
        //Bitmap bmp = new Bitmap(3 * xMax, 3 * yMax);
        Texture2D texture = new Texture2D(3*xMax, 3*yMax, TextureFormat.RGB24,false);

        //set all tiles first
        for(int x=0; x<xMax; x++)
        {
            for(int y=0; y<yMax; y++)
            {
                setPixelTile(texture, x, y);
            }
        }
        //set all arrows now
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                //setPixelTile(texture, x, y);
                setArrowPixels(texture, x, y);
            }
        }

    }


    public void setLeftArrow(Texture2D texture, int x, int y)
    {
        Color entryCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRY, out entryCol);
        Color exitCol;
        ColorUtility.TryParseHtmlString(OJColour.EXIT, out exitCol);
        Color entryExitCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRYEXIT, out entryExitCol);

        //start with left arrows
        bool leftArrow = arrowTiles[x, y, LEFT];
        bool rightArrowIn = false;
        //get if right arrow points in
        if (isValidIndex(x - 1, y) == true)
        {
            rightArrowIn = arrowTiles[x, y, RIGHT];
        }
        if(leftArrow == false)
        {
            return;
        }

        if(rightArrowIn == false)
        {
            texture.SetPixel(3 * x, 3 * y +1, entryCol);
            texture.SetPixel(3 * x-1, 3 * y +1, exitCol);
        }
        else
        {
            texture.SetPixel(3 * x, 3 * y + 1, entryExitCol);
            texture.SetPixel(3 * x -1, 3 * y + 1, entryExitCol);
        }
    }

    public void setRightArrow(Texture2D texture, int x, int y)
    {
        Color entryCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRY, out entryCol);
        Color exitCol;
        ColorUtility.TryParseHtmlString(OJColour.EXIT, out exitCol);
        Color entryExitCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRYEXIT, out entryExitCol);

        //start with right arrows
        bool rightArrow = arrowTiles[x, y, RIGHT];
        bool leftArrowIn = false;
        //get if left arrow points in
        if (isValidIndex(x + 1, y) == true)
        {
            leftArrowIn = arrowTiles[x, y, LEFT];
        }
        if (rightArrow == false)
        {
            return;
        }

        if (leftArrowIn == false)
        {
            texture.SetPixel(3 * x +2, 3 * y + 1, entryCol);
            texture.SetPixel(3 * x +3, 3 * y + 1, exitCol);
        }
        else
        {
            texture.SetPixel(3 * x + 2, 3 * y + 1, entryCol);
            texture.SetPixel(3 * x + 3, 3 * y + 1, exitCol);
        }
    }

    public void setUpArrow(Texture2D texture, int x, int y)
    {
        Color entryCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRY, out entryCol);
        Color exitCol;
        ColorUtility.TryParseHtmlString(OJColour.EXIT, out exitCol);
        Color entryExitCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRYEXIT, out entryExitCol);

        //start with up arrows
        bool upArrow = arrowTiles[x, y, UP];
        bool downArrowIn = false;
        //get if down arrow points in
        if (isValidIndex(x, y-1) == true)
        {
            downArrowIn = arrowTiles[x, y, DOWN];
        }
        if (upArrow == false)
        {
            return;
        }

        if (downArrowIn == false)
        {
            texture.SetPixel(3 * x + 1, 3 * y + 2, entryCol);
            texture.SetPixel(3 * x + 1, 3 * y + 3, exitCol);
        }
        else
        {
            texture.SetPixel(3 * x + 1, 3 * y + 2, entryCol);
            texture.SetPixel(3 * x + 1, 3 * y + 3, exitCol);
        }
    }

    public void setDownArrow(Texture2D texture, int x, int y)
    {
        Color entryCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRY, out entryCol);
        Color exitCol;
        ColorUtility.TryParseHtmlString(OJColour.EXIT, out exitCol);
        Color entryExitCol;
        ColorUtility.TryParseHtmlString(OJColour.ENTRYEXIT, out entryExitCol);

        //start with down arrows
        bool downArrow = arrowTiles[x, y, DOWN];
        bool upArrowIn = false;
        //get if up arrow points in
        if (isValidIndex(x, y+1) == true)
        {
            upArrowIn = arrowTiles[x, y, UP];
        }
        if (downArrow == false)
        {
            return;
        }

        if (upArrowIn == false)
        {
            texture.SetPixel(3 * x + 1, 3 * y, entryCol);
            texture.SetPixel(3 * x + 1, 3 * y - 1, exitCol);
        }
        else
        {
            texture.SetPixel(3 * x + 1, 3 * y, entryCol);
            texture.SetPixel(3 * x + 1, 3 * y - 1, exitCol);
        }
    }

    public void setArrowPixels(Texture2D texture, int x, int y)
    {
        //start with left
        setLeftArrow(texture, x, y);
        //now right
        setRightArrow(texture, x, y);
        //now up
        setUpArrow(texture, x, y);
        //now down
        setDownArrow(texture, x, y);

    }

    public void setPixelTile(Texture2D texture, int x, int y)
    {
        string spriteToWrite = OJColour.EMPTY;
        int spriteRead = tiles[x, y];

        switch (spriteRead)
        {
            case -1:
                spriteToWrite = OJColour.EMPTY;
                break;
            case (int)TileEnum.HOME:
                spriteToWrite = OJColour.HOME;
                break;
            case (int)TileEnum.STAR:
                spriteToWrite = OJColour.STAR;
                break;
            case (int)TileEnum.STAR2:
                spriteToWrite = OJColour.STAR2;
                break;
            case (int)TileEnum.DRAW:
                spriteToWrite = OJColour.DRAW;
                break;
            case (int)TileEnum.DRAW2:
                spriteToWrite = OJColour.DRAW2;
                break;
            case (int)TileEnum.BATTLE:
                spriteToWrite = OJColour.BATTLE;
                break;
            case (int)TileEnum.BATTLE2:
                spriteToWrite = OJColour.BATTLE2;
                break;
            case (int)TileEnum.DROP:
                spriteToWrite = OJColour.DROP;
                break;
            case (int)TileEnum.DROP2:
                spriteToWrite = OJColour.DROP2;
                break;
            case (int)TileEnum.WARP:
                spriteToWrite = OJColour.WARP;
                break;
            case (int)TileEnum.WARPMOVE:
                spriteToWrite = OJColour.WARPMOVE;
                break;
            case (int)TileEnum.WARPMOVE2:
                spriteToWrite = OJColour.WARPMOVE2;
                break;
            case (int)TileEnum.MOVE:
                spriteToWrite = OJColour.MOVE;
                break;
            case (int)TileEnum.MOVE2:
                spriteToWrite = OJColour.MOVE2;
                break;
            case (int)TileEnum.BLANK:
                spriteToWrite = OJColour.BLANK;
                break;
            case (int)TileEnum.ICE:
                spriteToWrite = OJColour.ICE;
                break;
            case (int)TileEnum.HEAL:
                spriteToWrite = OJColour.HEAL;
                break;
            case (int)TileEnum.HEAL2:
                spriteToWrite = OJColour.HEAL2;
                break;
            case (int)TileEnum.DECK:
                spriteToWrite = OJColour.DECK;
                break;
            default:
                spriteToWrite = OJColour.EMPTY;
                break;
        }
        Color col;
        ColorUtility.TryParseHtmlString(spriteToWrite, out col);
        for (int x1=0; x1 < 3; x1++)
        {
            for(int y1=0; y1<3; y1++)
            {
                
                texture.SetPixel(3*x + x1, 3*y + y1, col);
            }
        }
    }


}
