using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when enabled, user has the ability to update the mupliplication matrix using his mouse
public class MultClickGrid : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    void Update()
    {
        //on left mouse down
        if (Input.GetMouseButton(0)){
            MouseSetXY();
        }
    }

    //get mouse position to world position and call to update the matrix
    public void MouseSetXY(){
        Vector3 clickPos = -Vector3.one;
        //shoot a raycast with distance limit 200 units to hit anything
        //with given layermask ("Ground"). If hit then get world position
        //and call MouseSetXY() on MultDisplay singleton
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 200f, layerMask)){
            clickPos = hit.point;
        }
        MultDisplay.instance.MouseSetXY(clickPos);
    }
}
