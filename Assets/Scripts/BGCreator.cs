using UnityEngine;

public class BGCreator : MonoBehaviour
{
    public int level; // Current game level
    public GameObject[] backgrounds; // Array of background prefabs
    private GameObject currentBG;
    private GameObject nextBG;
    private GameObject prevBG;
    int counter;

    private void Start()
    {
        // Initialize the first background
        // currentBG = Instantiate(backgrounds[level], Vector3.zero, Quaternion.identity);
        currentBG = FindFirstObjectByType<BGController>().gameObject;
        CreateNext();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     // Check if the camera reached the top
    //     if (other.CompareTag("CameraTop"))
    //     {
    //         CreateNext();
    //     }
    //     // Check if the camera reached the bottom
    //     if (other.CompareTag("CameraBottom"))
    //     {
    //         RemovePrev();
    //     }
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            CallCreation();
        if (Input.GetKeyDown(KeyCode.R))
            RemovePrev();
    }
    public void CallCreation()
    {
        prevBG = currentBG;
        currentBG = nextBG;
        CreateNext();
    }
    public void CreateNext()
    {
        Renderer currentBGrenderer = currentBG.GetComponent<Renderer>();

        if (currentBGrenderer == null)
        {
            return;
        }
        Vector3 topPosition = new Vector3(0, (currentBGrenderer.bounds.max.y - currentBG.transform.position.y) * 2, 0);

        nextBG = Instantiate(backgrounds[level], currentBG.transform.position + topPosition, Quaternion.identity);
        nextBG.name = "created " + counter++;
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