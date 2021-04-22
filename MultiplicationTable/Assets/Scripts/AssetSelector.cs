using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSelector : MonoBehaviour
{
    public static AssetSelectorType type;
    [SerializeField] private List<GameObject> high;
    [SerializeField] private List<GameObject> medium;
    [SerializeField] private List<GameObject> low;
    [Header("Inspector Temp")]
    public int listIndex = 0;

    public void SetType(AssetSelectorType newType){
        type = newType;
    }

    public GameObject GetRandom(){
        GameObject returnObject = null;
        List<GameObject> list;

        switch (type)
        {
            case AssetSelectorType.HIGH:
                list = high;
                break;
            case AssetSelectorType.MEDIUM:
                list = medium;
                break;
            case AssetSelectorType.LOW:
                list = low;
                break;
            default:
                return null;
        }
        
        if (list.Count > 0){
            int index = Random.Range(0, list.Count);
            returnObject = list[index];
        }

        return returnObject;
    }


    //set type to high (temporary)
    void Awake(){
        type = AssetSelectorType.HIGH;
    }

    void Update(){
        if (listIndex==0){
            type = AssetSelectorType.HIGH;
        }
        else if (listIndex==1){
            type = AssetSelectorType.MEDIUM;
        }
        else{
            type = AssetSelectorType.LOW;
        }
    }
}

public enum AssetSelectorType{
    HIGH,
    MEDIUM,
    LOW
}
