using System.Collections;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Color color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        color = renderer.color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(destroyInteractable());

            IEnumerator destroyInteractable()
            {
                for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
                {
                    color.a = alpha;
                    renderer.color = color;
                    yield return new WaitForSeconds(0.1f);
                }

                Destroy(gameObject);
            }

        }
    }
}
