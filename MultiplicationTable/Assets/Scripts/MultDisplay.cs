using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultDisplay : MonoBehaviour
{
    public static MultDisplay instance; //singleton
    public GameObject itemSample;
    public Transform itemStartT, itemEndT;
    public float xSpace = 2, ySpace = 2;
    private MultItem[,] items;
    public int x=0, y=0, mult=0;

    [Header("Inspector Input")]
    public int inputx;
    public int inputy;
    private int oldX, oldY;
    private Camera cam;

    void Awake(){
        //singleton
        if (instance != null && instance != this){
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    void Start()
    {
        oldX = inputx;
        oldY = inputy;
        //create matrix
        items = new MultItem[x,y];
        cam = Camera.main;
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
        //subtract starting point of matrix
        pos.x -= itemStartT.position.x;
        pos.z -= itemStartT.position.z;

        //if mouse position is giving negative values, set matrix to empty
        if (pos.x < 0 || pos.z < 0){
            SetXY(0,0);
            return;
        }

        //get dimentions of matrix
        //value <- biggest integer of (mouse position / space between items)
        int mouseX = Mathf.CeilToInt((pos.x) / xSpace);
        int mouseY = Mathf.CeilToInt((pos.z) / ySpace);

        //if position x from mouse is bigger than end point, override to end x
        if (pos.x > itemEndT.position.x){
            mouseX = Mathf.CeilToInt((itemEndT.position.x) / xSpace);
        }
        //if position z from mouse is bigger than end point, override to end z
        if (pos.z > itemEndT.position.z){
            mouseY = Mathf.CeilToInt((itemEndT.position.z) / ySpace);
        }

        //call mtrix creation/update
        SetXY(mouseX,mouseY);
    }

    //update matrix
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
