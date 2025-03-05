using UnityEngine;

public class BeltController : MonoBehaviour {
    [SerializeField] private GameObject beltImage;
    [SerializeField] private float speed = 30f;

    private RectTransform beltRect;
    private RectTransform beltCopyRect;
    private float beltWidth;

    private void Start() {
        // Get the RectTransform of the original belt
        beltRect = beltImage.GetComponent<RectTransform>();

        // Get the width of the belt image
        beltWidth = beltRect.rect.width;

        // Create a second belt image and properly position it at the right edge
        GameObject beltCopy = Instantiate(beltImage.gameObject, transform);
        beltCopyRect = beltCopy.GetComponent<RectTransform>();

        // Align the second belt correctly next to the first one
        beltCopyRect.anchoredPosition = beltRect.anchoredPosition + new Vector2(beltWidth, 0f);
    }

    private void Update() {
        // Move both belt images to the left
        beltRect.anchoredPosition += Vector2.left * speed * Time.deltaTime;
        beltCopyRect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        // Reset position when a belt image moves completely out of view
        if (beltRect.anchoredPosition.x < -beltWidth) {
            beltRect.anchoredPosition = beltCopyRect.anchoredPosition + new Vector2(beltWidth, 0f);
        }

        if (beltCopyRect.anchoredPosition.x < -beltWidth) {
            beltCopyRect.anchoredPosition = beltRect.anchoredPosition + new Vector2(beltWidth, 0f);
        }
    }
}
