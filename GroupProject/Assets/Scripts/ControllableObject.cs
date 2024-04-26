using UnityEngine;

public class ControllableObject : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Make the object inactive when the scene starts
        gameObject.SetActive(false);
    }

    public void ToggleActiveState()
    {
        // Toggle the active state
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
