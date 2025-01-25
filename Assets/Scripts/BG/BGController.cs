using UnityEngine;

public class BGController : MonoBehaviour
{
    private BGCreator bgCreator;

    void Start()
    {
        bgCreator = BGCreator.inst;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (bgCreator == null)
            return;

        if (other.CompareTag("CameraBottom"))
            bgCreator.RemovePrev();

        else if (other.CompareTag("CameraTop"))
            bgCreator.CallCreation();
    }


}
