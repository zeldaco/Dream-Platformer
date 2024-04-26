using UnityEngine;

public class InputController : MonoBehaviour
{
    public ControllableObject controllableObject; // Reference to the ControllableObject script.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && controllableObject != null)
        {
            controllableObject.ToggleActiveState();
        }
    }
}
