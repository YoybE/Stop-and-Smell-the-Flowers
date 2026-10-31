using System;
using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Game Constants")]
    [SerializeField] private SimpleRuntimeSet interactableRuntimeSet;
    [SerializeField] private ListIntVariable colorScore;

    [Header("Game Events")]
    [SerializeField] private IntGameEvent _onUpdateColorScore;

    [Header("Interactable Attributes")]
    private SpriteRenderer renderer;
    private Color color;
    [SerializeField] private int colorIndex;
    [NonSerialized] public Vector3 startPosition;

    void OnAwake()
    {
        interactableRuntimeSet.Initialize();
    }

    void OnEnable()
    {
        interactableRuntimeSet.AddToRuntimeSet(this.gameObject);
    }

    void OnDisable()
    {
        interactableRuntimeSet.RemoveFromRuntimeSet(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        color = renderer.color;
        startPosition = GetComponent<Transform>().position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObject = collision.gameObject;
        if (colObject.CompareTag("Player"))
        {
            SpriteRenderer sr = colObject.GetComponent<SpriteRenderer>();
            sr.color = color;
            colorScore.ApplyChange(1, colorIndex);
            _onUpdateColorScore.Raise(colorIndex);

            #region Fade Interactable
            StartCoroutine(destroyInteractable());

            IEnumerator destroyInteractable()
            {
                for (float alpha = 1.0f; alpha > 0.0f; alpha -= 0.1f)
                {
                    color.a = alpha;
                    renderer.color = color;
                    yield return new WaitForSeconds(0.02f);
                }

                gameObject.SetActive(false);
            }
            #endregion
        }
    }

    public void ResetAlpha()
    {
        color.a = 1.0f;
        renderer.color = color;
    }
}
