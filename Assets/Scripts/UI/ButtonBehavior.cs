using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float maxTiltAmount = 10f;   
    public float tiltSpeed = 5f;        

    private bool isHovering = false;
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (isHovering)
        {  
            float tiltAmount = maxTiltAmount * Mathf.Sin(Time.time * tiltSpeed);
            Vector3 targetEulerAngles = new Vector3(0f, 0f, tiltAmount);
            Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, tiltSpeed * Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
