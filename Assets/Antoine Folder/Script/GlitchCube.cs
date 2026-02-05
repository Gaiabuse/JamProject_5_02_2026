using UnityEngine;

public class GlitchCube : MonoBehaviour
{
    [SerializeField] Vector2 RandomSizeSmallCube;
    [SerializeField] Vector2 RandomSizeBigCube;

    private void Start()
    {
        transform.localScale = RandomSize();
    }

    Vector3 RandomSize()
    {
        float x; float y; float z;

        if (Random.Range(0, 3) == 0)
        {
            x = Random.Range(RandomSizeSmallCube.x, RandomSizeSmallCube.y);
            y = Random.Range(RandomSizeSmallCube.x, RandomSizeSmallCube.y);
            z = Random.Range(RandomSizeSmallCube.x, RandomSizeSmallCube.y);
        }
        else
        {
            x = Random.Range(RandomSizeBigCube.x, RandomSizeBigCube.y);
            y = Random.Range(RandomSizeBigCube.x, RandomSizeBigCube.y);
            z = Random.Range(RandomSizeBigCube.x, RandomSizeBigCube.y);
        }

        return new Vector3(x, y, z);
    }
}
