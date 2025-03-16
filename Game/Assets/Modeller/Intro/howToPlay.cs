using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class howToPlay : MonoBehaviour
{
    public GameObject panel;
    private bool panelYes = false;


    public void onButtonClick()
    {
        if (!panelYes)
        {
          panel.SetActive(true);
          panelYes = true;
        }
        else
        {
          panel.SetActive(false);
          panelYes = false;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
