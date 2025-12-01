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

   public static int sharedAppleCount = 0;  
    private void Awake()
    {
        
        instance = this;
        count = sharedAppleCount;
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if(appleCountText != null)
        {
            appleCountText.text = count.ToString() + " / " + maxCount.ToString();
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
        sharedAppleCount = count;
    }

    private void OneUP()
    {
       
        if(audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        if (PlayerHealthController.instance != null)
        {

            PlayerHealthController.instance.AddLife(1);


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

    public static void ResetApples()
    {
        sharedAppleCount = 0;
    }
}
