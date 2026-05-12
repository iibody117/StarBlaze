using UnityEngine;

public class GSID : MonoBehaviour
{

    public static GSID instance;
    [SerializeField] int sceneID = -1;

    void Start()
    {
        ManageSingleton();
    }

    void ManageSingleton() // Keep the object on "DontDestroyOnLoad" So we can take it to other Scenes
    {
        if(instance != null && instance != this)// check if already exist
        {
            Debug.Log("if worked");
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;

        }
        //build a new one
        
            Debug.Log("else if worked");
            instance = this;
            DontDestroyOnLoad(gameObject);
    }

    //setters and getters
    public void SetsceneID(int value)
    {
        sceneID = value;
    }
    public int GetsceneID()
    {
        return sceneID;
    }
}
