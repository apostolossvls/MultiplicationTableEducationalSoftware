using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultDisplay : MonoBehaviour
{
    public AssetSelector itemSample;
    public Transform itemStartT, itemEndT, itemLastT;
    public float xSpace = 2, ySpace = 2;
    private MultItem[,] items;
    public int x=0, y=0, mult=0;

    [Header("Inspector Input")]
    public int inputx;
    public int inputy;
    private int oldX, oldY;
    private Camera cam;

    void Start()
    {
        oldX = inputx;
        oldY = inputy;
        //create matrix
        items = new MultItem[x,y];
        cam = Camera.main;

        //temp
        SetXY(0,0);
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

    //get worldpoint, calculate matrix dimensions and call SetXY
    public void MouseSetXY(Vector3 mousePos){  
        Vector3 pos = mousePos;
        //if (dirX >=0) pos.x -= itemStartT.position.x;
        //else pos.x -= itemEndT.position.x;
        //if (dirZ >=0) pos.z -= itemStartT.position.z;
        //else pos.z -= itemEndT.position.z;

        //subtract starting point of matrix
        pos.x -= itemStartT.position.x;
        pos.z -= itemStartT.position.z;

        //if mouse position is giving negative values, set matrix to empty
        //if (pos.x < startPos.x || pos.z < startPos.z){
        //if (pos.x < itemStartT.position.x || pos.z < itemStartT.position.z){
        //    SetXY(0,0);
        //    return;
        //}

        //get dimentions of matrix
        //value <- biggest integer of (mouse position / space between items)
        int mouseX = 0;
        int mouseY = 0;

        //calculate dir of the difference between start-end values
        float dirX = Mathf.Sign(itemEndT.position.x - itemStartT.position.x);
        float dirZ = Mathf.Sign(itemEndT.position.z - itemStartT.position.z);
        //if mouse position is within the right quadrant
        //calculate the items that can fit inside from start point to mouse point
        if (pos.x * dirX > 0){
            mouseX = Mathf.Abs(Mathf.CeilToInt((pos.x) / xSpace));
        }
        //same for y
        if (pos.z * dirZ > 0){
            mouseY = Mathf.Abs(Mathf.CeilToInt((pos.z) / ySpace));
        }

        //mouseX = Mathf.Abs(Mathf.CeilToInt((pos.x) / xSpace));
        //mouseY = Mathf.Abs(Mathf.CeilToInt((pos.z) / ySpace));

        //set a max value as an upper bound from end point
        //calculate the max amount of items that can be clones inside the area of start and end point
        //and clamp between the original and the max value
        mouseX = Mathf.Clamp(mouseX, 0, Mathf.Abs(Mathf.CeilToInt((itemEndT.position.x - itemStartT.position.x) / xSpace)));
        mouseY = Mathf.Clamp(mouseY, 0, Mathf.Abs(Mathf.CeilToInt((itemEndT.position.z - itemStartT.position.z) / ySpace)));


        /*
        //if position x from mouse is bigger than end point, override to end x
        bool endXPositive = Mathf.Sign(itemEndT.position.x - itemStartT.position.x) >= 0;
        bool endZPositive = Mathf.Sign(itemEndT.position.z - itemStartT.position.z) >= 0;
        if ((pos.x > itemEndT.position.x  && endXPositive) || (pos.x < itemEndT.position.x  && !endXPositive)){
            mouseX = Mathf.Abs(Mathf.CeilToInt((itemEndT.position.x) / xSpace));
        }
        //if position z from mouse is bigger than end point, override to end z
        //if (pos.z > itemEndT.position.z){
        //    mouseY = Mathf.Abs(Mathf.CeilToInt((itemEndT.position.z) / ySpace));
        //}
        if ((pos.z > itemEndT.position.z  && endZPositive) || (pos.z < itemEndT.position.z  && !endZPositive)){
            mouseY = Mathf.Abs(Mathf.CeilToInt((itemEndT.position.z) / ySpace));
        }
        */

        

        //call mtrix creation/update
        SetXY(mouseX,mouseY);
    }

    //update matrix
    public void SetXY(int newX, int newY){

        int newMult = newX*newY;

        print(newX+" ,"+newY);

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

        if (items.GetLength(0) > 0 && items.GetLength(1)>0){
            itemLastT.position = items[items.GetLength(0)-1, items.GetLength(1)-1].transform.position;
        }
        else {
            //itemLastT.position = itemStartT.position + new Vector3(xSpace,0,ySpace);
        }
    }

    //create / clone new item (and gamobject) and setup
    private MultItem CreateItem(int coX, int coY){
        //get random item from asset selector
        GameObject sample = itemSample.GetRandom();
        if (sample == null) {return null;}
        //clone
        GameObject g = GameObject.Instantiate(sample, itemStartT.position, itemStartT.rotation);
        g.SetActive(true); //show gameObject
        g.transform.SetParent(transform); //set parent so that it doesnt spam the hierarchy
        MultItem item = g.GetComponent<MultItem>(); //find MultItem component
        //move item to the correct position
        item.SetCoordinates(coX, coY, coX*xSpace+itemStartT.position.x, coY*ySpace+itemStartT.position.z);
        item.Show(); //show - animate
        return item; //return item to add it to the list
    }
}
