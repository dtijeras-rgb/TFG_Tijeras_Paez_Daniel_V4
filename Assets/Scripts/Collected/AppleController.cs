using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;


public class AppleController : MonoBehaviour
{
   public static AppleController instance;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioSource audioSource;

    public int count = 0;
    public int maxCount = 20;
    public int healAmount = 1;

    public TextMeshProUGUI appleCountText;

    public GameObject oneUpMessage;

   
    private void Awake()
    {
        
        instance = this;
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void CountApple()
    {
        count++;
        if(appleCountText != null)
        {
            appleCountText.text = count.ToString() + " / " + maxCount.ToString();
        }
        if (count >= maxCount)
        {
           OneUP();
           count = 0;

            appleCountText.text = "0";
        }
    }

    private void OneUP()
    {
       
        if(audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        if (PlayerHealthController.instance != null)
        {

            PlayerHealthController.instance.HealPlayer(healAmount);


        }
        StartCoroutine(ShowOneUpMessage());
    }

    private IEnumerator ShowOneUpMessage()
    {
        if (oneUpMessage != null)
        {
            oneUpMessage.SetActive(true);
            yield return new WaitForSeconds(2f);
            oneUpMessage.SetActive(false);
        }
    }
}
