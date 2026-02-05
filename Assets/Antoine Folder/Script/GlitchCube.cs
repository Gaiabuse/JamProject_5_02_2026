using UnityEngine;

public class GlitchCube : MonoBehaviour
{
    [SerializeField] Vector2 RandomSizeSmallCube;
    [SerializeField] Vector2 RandomSizeBigCube;

    [SerializeField] Vector2 ChangeSize;
    float timerSize;

    [SerializeField] Vector2 ShakeRandom;
    float shake;
    Vector3 ogPosition;

    private void Start()
    {
        transform.localScale = RandomSize();
        ogPosition = transform.position;
        shake = Random.Range(ShakeRandom.x, ShakeRandom.y);

        timerSize = Random.Range(ChangeSize.x, ChangeSize.y);
    }

    private void FixedUpdate()
    {
        transform.position = ogPosition + RandomPose();

        timerSize -= Time.deltaTime;
        if (timerSize <= 0)
        {
            transform.localScale = RandomSize();
            timerSize = Random.Range(ChangeSize.x, ChangeSize.y);
        }
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

    Vector3 RandomPose()
    {
        float x; float y; float z;

        x = Random.Range(-shake, shake);
        y = Random.Range(-shake, shake);
        z = Random.Range(-shake, shake);

        return new Vector3(x, y, z);
    }
}
