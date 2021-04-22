using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MultTextDisplay : MonoBehaviour
{
    [SerializeField] MultDisplay multDisplay;
    [SerializeField] TMP_InputField xTextInput , yTextInput , multTextInput;
    [SerializeField] bool xUnlocked, yUnlocked, multUnlocked;
    [SerializeField] int xLimit = 20, yLimit = 20;

    void Start()
    {
        Show();
    }

    private void Show(){
        gameObject.SetActive(true);
        xTextInput.interactable = xUnlocked;
        yTextInput.interactable = yUnlocked;
        multTextInput.interactable = multUnlocked;
    }

    public void SetXYValue(){
        int x = -1;
        int y = -1;
        try
        {
            x = System.Convert.ToInt32(xTextInput.text);
            y = System.Convert.ToInt32(yTextInput.text);
            //x = int.Parse(xTextInput.text); 
            //y = int.Parse(yTextInput.text);
            if (x > xLimit) {
                x = xLimit;
                xTextInput.text = x.ToString();
            }   
            if (y > yLimit) {
                y = yLimit;
                yTextInput.text = y.ToString();
            }   
        }
        catch (System.FormatException)
        {
            print("Parse exception!");
            //throw;
        }
        if (x>=0 && y >= 0) multDisplay.SetXY(x, y);
    }
}
