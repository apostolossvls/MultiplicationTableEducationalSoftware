using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultDisplay : MonoBehaviour
{
    public GameObject itemSample;
    public Transform itemStartT;
    public float xSpace = 2, ySpace = 2;
    private MultItem[,] items;
    public int x=0, y=0, mult=0;
    public int inputx, inputy;
    private int oldX, oldY;

    void Start()
    {
        oldX = inputx;
        oldY = inputy;
        //create matrix
        items = new MultItem[x,y];
    }

    void Update()
    {
        //inspector input handler
        if (oldX != inputx || oldY != inputy) {
            SetXY(inputx,inputy);
        }
        oldX = inputx;
        oldY = inputy;
    }

    public void SetXY(int newX, int newY){

        int newMult = newX*newY;

        //create new matrix, add already existing items from the old matrix
        //that can be included into the new matrix and delete others
        MultItem[,] newItems = new MultItem[newX, newY];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                //if index is inside the new bounds
                if (i<newX && j< newY){
                    //print("x,y,newX,newY,i,j: "+x+" "+y+" "+newX+" "+newY+" "+i+" "+j);
                    newItems[i,j] = items[i,j];
                }
                //else call item delete
                else{
                    //print("Destroy: "+i.ToString()+" x"+j.ToString());
                    //Destroy(items[i,j].gameObject);
                    items[i,j].DestroyItem();
                }
            }
        }

        //create items where needed
        for (int i = 0; i < newX; i++)
        {
            for (int j = 0; j < newY; j++)
            {
                //if index is inside the old bounds, dont create new item
                if (i<x && j<y){continue;}
                //create new item and add it to matrix
                newItems[i,j] = CreateItem(i,j);
            }
        }
        
        //after calculations, old values aren't needed and now can be changed
        x = newX;
        y = newY;
        //set matrix to the new matrix
        items = newItems;
    }

    //create / clone new item (and gamobject) and setup
    private MultItem CreateItem(int coX, int coY){
        GameObject g = GameObject.Instantiate(itemSample, itemStartT.position, itemStartT.rotation);
        g.SetActive(true);
        g.transform.SetParent(transform);
        MultItem item = g.GetComponent<MultItem>();
        item.SetCoordinates(coX, coY, xSpace, ySpace);
        return item;
    }
}
