using UnityEngine;

public class IntormationPanels : MonoBehaviour
{
    [SerializeField] private GameObject panelInfo;

    void Start()
    {
        if (panelInfo != null) {  panelInfo.SetActive(false); }
          
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (panelInfo != null) 
            { 
                panelInfo.SetActive(true); 
            }
           
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (panelInfo != null) 
            { 
                panelInfo.SetActive(false); 
            }
           
        }
    }
}



