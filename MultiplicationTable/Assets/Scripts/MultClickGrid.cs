using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when enabled, user has the ability to update the mupliplication matrix using his mouse
public class MultClickGrid : MonoBehaviour
{
    [SerializeField] MultDisplay multDisplay;
    [SerializeField] LayerMask layerMask;
    bool overUI = false;

    void Update()
    {
        //on left mouse down
        if (Input.GetMouseButton(0) && !overUI){
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
        multDisplay.MouseSetXY(clickPos);
    }

    public void PointerEnterUI(){
        overUI = true;
    }

    public void PointerExitUI(){
        overUI = false;
    }
}
