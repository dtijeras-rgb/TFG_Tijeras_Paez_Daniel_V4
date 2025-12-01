using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public Sprite heartFull, heartEmpty;

    public Image heart1, heart2, heart3, pacifierUI;

    public TextMeshProUGUI livesText;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        if (pacifierUI != null) { pacifierUI.enabled = false; }
    }

   
    void Update()
    {
        
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, 3);

        heart1.enabled = currentHealth >= 1;
        heart2.enabled = currentHealth >= 2;
        heart3.enabled = currentHealth >= 3;
    }

    public void PacifierEnable(bool visible)
    {
        if (pacifierUI != null) { pacifierUI.enabled = visible; }
         
    }

    public void UpdateLivesDisplay(int currentLives)
    {
        if (livesText != null)
        {
            livesText.text = "x " + currentLives.ToString();
        }
    }

}
