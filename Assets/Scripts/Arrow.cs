using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction = Vector2.left;
    private bool canDestroy;

    void Start()
    {
        canDestroy = false;
        StartCoroutine(DestroyCoroutine());
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
