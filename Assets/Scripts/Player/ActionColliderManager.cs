using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionColliderManager : MonoBehaviour
{
    [SerializeField] private BoxCollider managedCollider;

    [Header("Body Transforms")]
    [SerializeField] private Transform colliderTop;
    [SerializeField] private Transform colliderBottom;
    [SerializeField] private Transform colliderLeft;
    [SerializeField] private Transform colliderRight;

    [Header("MeasurePoints")]
    [SerializeField] private Transform topMatcher;
    [SerializeField] private Transform bottomMatcher;

    [Header("Debug")]
    [SerializeField] private Vector3 topPosition;
    [SerializeField] private Vector3 bottomPosition;

    private void Start()
    {
        topPosition = colliderTop.position;
        bottomPosition = colliderBottom.position;
    }
    //private void Update()
    //{
    //    topMatcher.position = colliderTop.position;
    //    bottomMatcher.position = colliderBottom.position;

    //    topPosition = topMatcher.position;
    //    bottomPosition = bottomMatcher.position;
    //    managedCollider.size = new Vector3(managedCollider.size.x, topMatcher.position.y-bottomMatcher.position.y , 0);
    //}
}
