using UnityEngine;
using UnityEngine.SceneManagement;

public class BGCreator : MonoBehaviour
{
    public int level; // Current game level
    public int FINAL_LEVEL = 3;
    public int controllerPerLevelAmount = 4;
    private int controllerPerLevelCounter;

    public GameObject[] backgrounds; // Array of background prefabs
    public GameObject[] transitions; // Array of transition prefabs

    private GameObject currentBG, nextBG, prevBG;
    public static BGCreator inst;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;

            DontDestroyOnLoad(gameObject);

            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ResetParams();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        if (inst == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ResetParams()
    {
        Debug.LogWarning("Resetting Parameters");
        level = 0;
        controllerPerLevelCounter = controllerPerLevelAmount;
        DestroyAllBGs();

        currentBG = FindFirstObjectByType<BGMainSplash>()?.gameObject;
        Debug.LogWarning("New Current BG: " + currentBG);

        if (currentBG != null)
        {
            CreateNext(backgrounds[level]);
        }
        else
        {
            Debug.LogWarning("No BGMainSplash found!");
        }
    }

    private void Start()
    {
        // Optionally, ensure initial setup happens here
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
        if (currentBG == null)
        {
            Debug.LogError("Current BG is null! Cannot create next background.");
            return;
        }

        Renderer currentBGrenderer = currentBG.GetComponent<Renderer>();
        if (currentBGrenderer == null)
        {
            Debug.LogError("Current BG Renderer is missing!");
            return;
        }

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

    public void DestroyAllBGs()
    {
        if (prevBG != null)
        {
            Debug.LogWarning("Destroying previous background");
            Destroy(prevBG);
            prevBG = null;
        }
        if (currentBG != null)
        {
            Debug.LogWarning("Destroying current background");
            Destroy(currentBG);
            currentBG = null;
        }
        if (nextBG != null)
        {
            Debug.LogWarning("Destroying next background");
            Destroy(nextBG);
            nextBG = null;
        }
    }

    // Scene loaded callback
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        ResetParams(); // Reset parameters and destroy BGs
    }
}
