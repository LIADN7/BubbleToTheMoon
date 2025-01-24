using UnityEngine;

public class BGCreator : MonoBehaviour
{
    public int level; // Current game level
    public GameObject[] backgrounds; // Array of background prefabs
    private GameObject currentBG, nextBG, prevBG;
    public static BGCreator inst;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Optionally make the GameManager persist across scenes
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        currentBG = FindFirstObjectByType<BGController>().gameObject;
        CreateNext();
    }
    public void CallCreation()
    {
        prevBG = currentBG;
        currentBG = nextBG;
        CreateNext();
    }
    private void CreateNext()
    {
        Renderer currentBGrenderer = currentBG.GetComponent<Renderer>();
        if (currentBGrenderer == null)
            return;

        Vector3 topPosition = new Vector3(0, (currentBGrenderer.bounds.max.y - currentBG.transform.position.y) * 2, 0);

        nextBG = Instantiate(backgrounds[level], currentBG.transform.position + topPosition, Quaternion.identity);
    }
    public void RemovePrev()
    {
        if (prevBG != null)
        {
            Destroy(prevBG);
            prevBG = null;
        }
        else
        {
            Debug.LogWarning("Previous background is already destroyed or not set.");
        }
    }
}