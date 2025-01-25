using UnityEngine;

public class BGCreator : MonoBehaviour
{
    private int level; // Current game level
    private int controllerPerLevelAmount = 4;
    private int controllerPerLevelCounter = 4;

    public GameObject[] backgrounds; // Array of background prefabs
    public GameObject[] transitions; // Array of background prefabs

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
        CreateNext(backgrounds[level]);
    }
    public void CallCreation()
    {
        prevBG = currentBG;
        currentBG = nextBG;
        if (controllerPerLevelCounter == 0)
        {
            controllerPerLevelCounter = controllerPerLevelAmount;
            CreateNext(transitions[level]);
            level++;
        }
        else
        {
            controllerPerLevelCounter--;
            CreateNext(backgrounds[level]);
        }

    }
    private void CreateNext(GameObject obj)
    {
        Renderer currentBGrenderer = currentBG.GetComponent<Renderer>();
        if (currentBGrenderer == null)
            return;

        Vector3 topPosition = new Vector3(0, (currentBGrenderer.bounds.max.y - currentBG.transform.position.y) * 2, 0);

        nextBG = Instantiate(obj, currentBG.transform.position + topPosition, Quaternion.identity);
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