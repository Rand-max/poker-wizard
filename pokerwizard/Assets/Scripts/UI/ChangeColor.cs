using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

    // Reference to Sprite Renderer component
    private Renderer rend;

    // Color value that we can set in Inspector
    // It's White by default
    [SerializeField]
    private Color colorToTurnTo = Color.white;

    // Use this for initialization
    public void Colorchange() {
        rend = GetComponent<Renderer>();
        rend.material.color = colorToTurnTo;
    }	
}
