using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    private float checkpointX;
    private float checkpointY;


    void Start()
    {
        if(PlayerPrefs.GetFloat("checkpointX") != 0) { 
            checkpointX = PlayerPrefs.GetFloat("checkpointX");
            checkpointY = PlayerPrefs.GetFloat("checkpointY");
            transform.position = new Vector3(checkpointX, checkpointY);
        }
        else
        {
                       checkpointX = transform.position.x;
            checkpointY = transform.position.y;
        }
    }

    public void SetCheckpoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkpointX", x);
        PlayerPrefs.SetFloat("checkpointY", y);

        PlayerPrefs.Save();

    }

    public Vector2 GetCheckpoint()
    {
        return new Vector2(checkpointX, checkpointY);
    }

}
