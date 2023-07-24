using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MoveUp : MovementController, IPointerClickHandler
{
    private UI ui;
    private GameObject[] arrowObjects;
    private AudioSource audioSource;
    public AudioClip slidingEffect;
    private void Start()
    {
        ui = FindAnyObjectByType<UI>();
        FindArrowObjects();
        audioSource = GetComponent<AudioSource>();
    }

    private void FindArrowObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> arrowList = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("arrow"))
            {
                arrowList.Add(obj);
            }
        }

        arrowObjects = arrowList.ToArray();
    }
   
    public override void Move()
    {
        if (ui.selectedObj != null)
        {
            StartCoroutine(MoveSmoothly(ui.selectedObj.transform));
        }
        for (int i = 0; i < arrowObjects.Length; i++)
        {
            arrowObjects[i].transform.Translate(Vector2.up * moveDistance * Time.deltaTime);
        }
        ui.FindSelected();
    }
    private IEnumerator MoveSmoothly(Transform targetTransform)
    {
        Vector3 targetPosition = targetTransform.position + Vector3.up * moveDistance;
        float duration = 0.3f;
        float elapsedTime = 0.0f;

        Vector3 initialPosition = targetTransform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            targetTransform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Move();
        audioSource.PlayOneShot(slidingEffect);
    }
}
