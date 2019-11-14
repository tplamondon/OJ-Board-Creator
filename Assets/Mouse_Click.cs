using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mouse_Click : MonoBehaviour
{

    public Sprite[] list = new Sprite[21];
    public int spriteToDo = (int)TileEnum.HOME;

    int[,] tiles = new int[50, 30];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Pressed primary button.");
            if(Input.mousePosition.x > 200)
            {
                Debug.Log(Input.mousePosition.ToString());
            }
            

        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed secondary button.");
        }
            
    }


    public void changeSprite()
    {
        var drop = GameObject.Find("TileChoose").GetComponent<Dropdown>();
        //Debug.Log(drop.value);
        spriteToDo = drop.value;
    }
}
