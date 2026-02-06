using UnityEngine;

public class PlatformColliderGlitch : MonoBehaviour
{
    [SerializeField] GameObject PlatformVisual;
    [SerializeField] int AddForceUp;
    [SerializeField] GameObject GlitchCube;

    Rigidbody2D RbPlatVis;
    int start = 0;
    float speedX;

    private void Start()
    {
        RbPlatVis = PlatformVisual.GetComponent<Rigidbody2D>();
        RbPlatVis.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        if (start == 1)
        {
            PlatformVisual.transform.position += new Vector3(speedX * Time.deltaTime, 0, 0);
            PlatformVisual.transform.Rotate(0, 0, speedX + RbPlatVis.linearVelocityY * Time.deltaTime);

            if (Vector2.Distance(Camera.main.transform.position, PlatformVisual.transform.position) > 10)
            {
                Destroy(PlatformVisual);
                start = 2;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && start == 0)
        {
            RbPlatVis.bodyType = RigidbodyType2D.Dynamic;
            RbPlatVis.AddForce(Vector2.up * AddForceUp);
            speedX = (Random.Range(-5, 5));

            for (int i = 0; i < 12; i++)
            {
                Instantiate(GlitchCube, transform.position + RandomPosCube(), Quaternion.identity);
            }

            start = 1;
            PlatformVisual.transform.SetParent(null);
        }
    }

    Vector3 RandomPosCube()
    {
        float x = Random.Range(transform.localScale.x / 2 * -1, transform.localScale.x / 2);
        float y = Random.Range(transform.localScale.y / 2 * -1, transform.localScale.y / 2);
        float z = Random.Range(transform.localScale.z / 2 * -1, transform.localScale.z / 2);

        return new Vector3(x, y, z);
    }
}
