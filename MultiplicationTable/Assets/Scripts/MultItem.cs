using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultItem : MonoBehaviour
{
    private int x,y;

    public void SetTarget(){
        print("SetTarget call ("+gameObject.name+")");
    }

    public void SetTargetAndDestroy(){
        SetTarget();
        Destroy(gameObject, 3f);
    } 

    public void CheckCoordinates(int matrixX, int matrixY){
        if (!(x < matrixX && y < matrixY)){
            Destroy(gameObject);
        }
    }

    public void SetCoordinates(int coX, int coY){
        x = coX;
        y = coY;

        transform.position = new Vector3(x*2, transform.position.y, y*2);
    }
}
