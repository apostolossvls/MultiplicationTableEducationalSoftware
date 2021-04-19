using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultItem : MonoBehaviour
{
    private int x,y;
    public Animator animator;

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

    public void Show(){
        animator.SetTrigger("Popup");
    }

    public void DestroyItem(){
        animator.SetTrigger("Destroy");
        Destroy(gameObject, 0.5f);
    }

    public void SetCoordinates(int coX, int coY, float posX, float posY){
        x = coX;
        y = coY;

        transform.position = new Vector3(posX, transform.position.y, posY);
    }
}
