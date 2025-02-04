using UnityEngine;

public class ButtonPlay : MonoBehaviour
{
    public GameObject button;
    public GameObject text;
    public DimensionManager dimension;

    public void Update()
    {
        if(dimension.IsInWhiteDimension == false)
        {
            button.SetActive(true);
            text.SetActive(false);
        }
        else
        {
            button.SetActive(false);
            text.SetActive(true);
        }
    }


}