using UnityEngine;

public class BGElement : MonoBehaviour
{
    private float speed = 5f;
    protected GameManager manager;
    protected UICanvasController canvasController;

    protected virtual void Awake()
    {
        manager = GameManager.inst;
        canvasController = FindFirstObjectByType<UICanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += (Random.Range(0, 2) == 0 ? -1 : 1) * Vector3.right * speed * Time.deltaTime;
    }
}
