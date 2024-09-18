using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder2D : MonoBehaviour
{
    public SpriteRenderer targetRenderer;
    public string sortingLayer = "Default";
    public bool update = false;

    private void Start()
    {
        Setup();

        if (update)
            StartCoroutine(UpdateCo());
    }

    void Setup()
    {
        targetRenderer.sortingLayerName = sortingLayer;
        targetRenderer.sortingOrder = (int) (transform.position.y * -100f);
    }

    IEnumerator UpdateCo()
    {
        while (true)
        {
            Setup();
            yield return null;
        }
    }
}
