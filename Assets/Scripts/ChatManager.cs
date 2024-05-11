using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public GameObject messaheBoxPrefabRes; // Reference to the prefab to instantiate
    public GameObject messaheBoxPrefabReq; // Reference to the prefab to instantiate
    public Transform chatParent; // Reference to the parent object under which the prefabs will be instantiated
    public TMP_InputField chatInputField;

    public void MessageSend()
    {
        // Instantiate the prefab and set its parent
        GameObject reqMessageBox = Instantiate(messaheBoxPrefabReq, transform.position, Quaternion.identity, chatParent);

        // Access the Text component inside the instantiated prefab
        TMP_Text textComponent = reqMessageBox.GetComponentInChildren<TMP_Text>();

        // Change the text of the Text component
        if (textComponent != null)
        {
            textComponent.text = chatInputField.text;
            StartCoroutine(AsyncReq());
        }
        else
        {
            Debug.LogWarning("Text component not found in the prefab.");
        }
    }
    public void MessageReceive()
    {
        // Instantiate the prefab and set its parent
        GameObject resMessageBox = Instantiate(messaheBoxPrefabRes, transform.position, Quaternion.identity, chatParent);

        // Access the Text component inside the instantiated prefab
        TMP_Text textComponent = resMessageBox.GetComponentInChildren<TMP_Text>();

        // Change the text of the Text component
        if (textComponent != null)
        {
            textComponent.text = chatInputField.text;
        }
        else
        {
            Debug.LogWarning("Text component not found in the prefab.");
        }
    }
    private IEnumerator AsyncReq()
    {
        yield return new WaitForSeconds(2f);
        MessageReceive();
    }
}
