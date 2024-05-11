using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public string tagToDetect = "Player";
    public GameObject infoText;
    public GameObject chatWindow;
    private bool inRange = false;
    public PlayerController playerController;
    public TMP_InputField inputField;

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                chatWindow.SetActive(true);
                //Time.timeScale = 0; // Pause the game
                Cursor.visible = true; // Show cursor
                playerController.canMove = false;
                infoText.SetActive(false);
                inputField.text = string.Empty;
                ResetChat();
            }
        }
        //chatWindow.SetActive(false);
    }

    // This function is called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        // Check if the other GameObject has the specified tag
        if (other.CompareTag(tagToDetect))
        {
            infoText.SetActive(true);
            inRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        infoText.SetActive(false);
        inRange = false;
    }
    public void Close()
    {
        Cursor.visible = true; // Show cursor
        playerController.canMove = true;
        chatWindow.SetActive(false);
        infoText.SetActive(true);
    }
    public void ResetChat()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("MSG");

        // Loop through each object and destroy it
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}
