using Unity.VisualScripting;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    BoxCollider2D boxCollider;

    [SerializeField] float speed = 10;
    [SerializeField] float accelerateAmount = 1;
    [SerializeField] float accelerateWait = 1;

    float timerAccelerate = 0;

    [SerializeField] GameObject GlitchCube;
    [SerializeField] float spawnCubeTimer = 0.15f;
    [SerializeField] Vector2 SpawnCube;
    float timerSpawnCube = 0;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        timerAccelerate += Time.deltaTime;
        timerSpawnCube += Time.deltaTime;

        if (timerAccelerate >= accelerateWait)
        {
            speed += accelerateAmount;
            timerAccelerate = 0;
        }

        if (timerSpawnCube >= spawnCubeTimer)
        {
            for (int i = 0; i < Random.Range(SpawnCube.x, SpawnCube.y); i++)
            {
                Instantiate(GlitchCube, transform.position + RandomPosCube(), Quaternion.identity);
            }

            timerSpawnCube = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Application.Quit();
            Debug.Log("FUCK");
        }
        if (collision.gameObject.CompareTag("DeleteWhenDeathZone"))
        {
            Destroy(collision.gameObject);
        }
    }

    Vector3 RandomPosCube()
    {
        float x = Random.Range(boxCollider.size.x / 2 * -1, boxCollider.size.x / 2);
        float y = Random.Range(boxCollider.size.y / 2 * -1, boxCollider.size.y / 2);
        float z = Random.Range(transform.localScale.z / 2 * -1, transform.localScale.z / 2);

        return new Vector3(x, y, z);
    }
}
