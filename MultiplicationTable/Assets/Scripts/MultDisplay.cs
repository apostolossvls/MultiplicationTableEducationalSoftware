using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultDisplay : MonoBehaviour
{
    public GameObject itemSample;
    public Transform itemStartT;
    private MultItem[,] items;
    public int x=0, y=0, mult=0;
    public int inputx, inputy, oldX, oldY;

    void Start()
    {
        oldX = inputx;
        oldY = inputy;
        items = new MultItem[x,y];
    }

    void Update()
    {
        if (oldX != inputx || oldY != inputy) {
            SetXY(inputx,inputy);
        }
        oldX = inputx;
        oldY = inputy;
    }

    public void SetXY(int newX, int newY){
        //x = newX;
        //y = newY;
        int newMult = newX*newY;

        MultItem[,] newItems = new MultItem[newX, newY];
        //print("setcall");

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (i<newX && j< newY){
                    //print("x,y,newX,newY,i,j: "+x+" "+y+" "+newX+" "+newY+" "+i+" "+j);
                    newItems[i,j] = items[i,j];
                }
                else{
                    //print("Destroy: "+i.ToString()+" x"+j.ToString());
                    //Destroy(items[i,j].gameObject);
                    items[i,j].DestroyItem();
                }
            }
        }

        for (int i = 0; i < newX; i++)
        {
            for (int j = 0; j < newY; j++)
            {
                if (i<x && j<y){continue;}
                newItems[i,j] = CreateItem(i,j);
            }
        }
        
        x = newX;
        y = newY;
        items = newItems;

        /*
        for (int i = 0; i < Mathf.Min(x, newX); i++)
        {
            bool createRow = false;
            bool deleteRow = false;
            if (newX<=i) {
                deleteRow = true;
            }
            else if (x<=i){
                createRow = true;
            }
            for (int j = 0; j < Mathf.Min(y, newY); j++)
            {
                if (createRow){
                    //create y
                    continue;
                }
                else if (deleteRow) {
                    //delete y
                    continue;
                }
                if (x>i && newX>i){
                //transfer
                }
                else if (y>j) {
                    //delete
                    deleteRow = true;
                }
                else {
                    //add
                    createRow = true;
                }
            }
        }
        */
    }

    private MultItem CreateItem(int coX, int coY){
        GameObject g = GameObject.Instantiate(itemSample, itemStartT.position, itemStartT.rotation);
        g.SetActive(true);
        g.transform.SetParent(transform);
        MultItem item = g.GetComponent<MultItem>();
        item.SetCoordinates(coX, coY);
        return item;
    }
}
