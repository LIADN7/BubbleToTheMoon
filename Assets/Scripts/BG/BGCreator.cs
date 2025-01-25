using UnityEngine;

public class BGCreator : MonoBehaviour
{
    public int level; // Current game level
    public int FINAL_LEVEL = 3;
    public int controllerPerLevelAmount = 4;
    private int controllerPerLevelCounter;

    public GameObject[] backgrounds; // Array of background prefabs
    public GameObject[] transitions; // Array of background prefabs

    private GameObject currentBG, nextBG, prevBG;
    public static BGCreator inst;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void ResetParams()
    {
        level = 0;
        controllerPerLevelCounter = controllerPerLevelAmount;
        prevBG = null;
        currentBG = null;
        nextBG = null;
    }

    private void Start()
    {
        ResetParams();

        controllerPerLevelCounter = controllerPerLevelAmount;
        currentBG = FindFirstObjectByType<BGController>().gameObject;
        CreateNext(backgrounds[level]);
    }
    public void CallCreation()
    {
        prevBG = currentBG;
        currentBG = nextBG;
        if (level == FINAL_LEVEL)
        {
            CreateNext(backgrounds[level]);
            return;
        }
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

        nextBG = Instantiate(obj, currentBG.transform.position + topPosition, Quaternion.identity, this.transform);
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