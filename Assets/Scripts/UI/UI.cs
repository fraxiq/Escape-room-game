using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    
    public GameObject[] arrows;
    public GameObject selectedObj;
    public ObjectBehavior player;
    public GameObject resetButton;
    public GameObject randomButton;
    public GameObject text;
    private CanvasScaler canvasScaler;

    private void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        selectedObj = null;
        AdjustCanvasScaler();
    }

    private void Update()
    {
        if (Screen.width != canvasScaler.referenceResolution.x || Screen.height != canvasScaler.referenceResolution.y)
        {      
            AdjustCanvasScaler();
        }
    }
    private void FixedUpdate()
    {
        if (selectedObj != null)
        {
            UpdateArrows(selectedObj.GetComponent<ObjectBehavior>());
        }
        if (player.finished)
        {
            
            resetButton.SetActive(true);
            text.SetActive(true);
            randomButton.SetActive(false);
        }
    }
    void AdjustCanvasScaler()
    {
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }
    public void FindSelected()
    {
        ObjectBehavior[] objects = FindObjectsOfType<ObjectBehavior>();
        foreach (ObjectBehavior obj in objects)
        {
            if (obj.selected)
            {
                selectedObj = obj.gameObject;
                return;
            }
        }
        
        selectedObj = null;
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }
    private void UpdateArrows(ObjectBehavior obj)
    {
        for (int i = 0; i < obj.spawnpoints.Length; i++)
        {
            GameObject arrow = arrows[i];
            SpawnPoint spawnPoint = obj.spawnpoints[i].GetComponent<SpawnPoint>();
            Image arrowImage = arrow.GetComponent<Image>();

            if (!spawnPoint.collides)
            {
                arrowImage.enabled = true;
                arrow.transform.position = obj.spawnpoints[i].transform.position;
            }
            else
            {
                arrowImage.enabled = false;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
