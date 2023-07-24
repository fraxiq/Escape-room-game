using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectBehavior : MonoBehaviour
{
    public bool selected = false;
    [SerializeField]
    public GameObject[] spawnpoints;
    private UI ui;
    public bool finished;

    private void Start()
    {
        ui = FindObjectOfType<UI>();
    }
    
    private void OnMouseDown()
    {
        if (!selected)
        {
            DeselectAllObjects();
            selected = true;
            ui.FindSelected();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            finished = true;
        }
    }
    void DeselectAllObjects()
    {
        ObjectBehavior[] objects = FindObjectsOfType<ObjectBehavior>();
        foreach (ObjectBehavior obj in objects)
        {
            obj.DeselectObject();
        }
    }
    void DeselectObject()
    {
        selected = false;
    }
}
