using UnityEngine;

public class BGCreator : MonoBehaviour
{
    protected enum BGState
    {
        Prev,
        Current,
        Next
    }
    public int level;
    public GameObject[] bacgkgrounds; // class with spawner
    private GameObject currentBG;
    private GameObject nextBG;
    private GameObject prevBG;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CameraTop"))
        {

            // When the top reach the next
            // current = prev, next = current
            // create next
        }
        if (other.gameObject.CompareTag("CameraBottom"))
        {
            // ehrn the bottom reach the current
            // remove prev
            // 
        }
    }



    public void CreateNext()
    {

        prevBG = currentBG;
        currentBG = nextBG;
        nextBG = Instantiate(bacgkgrounds[level], currentBG.transform.position + new Vector3(0, 1080, 0), Quaternion.identity);
    }

    public void RemovePrev()
    {
        Destroy(prevBG);
    }
    



}
