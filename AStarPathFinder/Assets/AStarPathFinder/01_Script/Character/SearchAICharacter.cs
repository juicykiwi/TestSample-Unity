using UnityEngine;
using System.Collections;

public class SearchAICharacter : Character
{
    [SerializeField]
    protected float _repeatFindPathTime = 0f;

    void Start()
    {
        InitPathFinder(TileManager.Instance.AStarMapWeight);
    }

    void OnEnable()
    {
        StartCoroutine(FindPathCoroutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator FindPathCoroutine()
    {
        while (true)
        {
            if (_target != null)
            {
                FindPath(_target.transform.position);
            }

            yield return new WaitForSeconds(_repeatFindPathTime);
        }
    }
}
