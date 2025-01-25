using UnityEngine;

public class BGElementsSpawner : MonoBehaviour
{

    [SerializeField] int spawnCount;
    [SerializeField] BGElement[] elements;

    BGController controller;
    BGCreator creator;


    private void Start()
    {
        controller = GetComponentInParent<BGController>();
        creator = BGCreator.inst;
        if (elements.Length == 0)
            return;
        for (int i = 0; i < spawnCount; i++)
        {
            Spawn();
        }
    }
    private BGElement Spawn()
    {
        BGElement elementToCreate = elements[Random.Range(0, elements.Length)];
        BGElement elemnetCretaed = Instantiate(elementToCreate, controller.transform) as BGElement;
        Vector3 pos = WhereToSpawn();
        elemnetCretaed.transform.position = pos;

        return elemnetCretaed;
    }

    private Vector3 WhereToSpawn()
    {

        Renderer currentBGrenderer = controller.GetComponent<Renderer>();
        if (currentBGrenderer == null)
            return new Vector3(0, 0, 0);

        float boundryT = currentBGrenderer.bounds.max.y;
        float boundryB = currentBGrenderer.bounds.min.y;
        float boundryR = currentBGrenderer.bounds.max.x;
        float boundryL = currentBGrenderer.bounds.min.x;
        float randomY = Random.Range(boundryB, boundryT);
        float randomX = Random.Range(boundryL, boundryR);
        if (creator.level == creator.FINAL_LEVEL)
        {
            return new Vector3(currentBGrenderer.transform.position.x, currentBGrenderer.transform.position.y, 0);
        }
        return new Vector3(randomX, randomY, 0);

    }

}
