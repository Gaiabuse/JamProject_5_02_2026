using UnityEngine;

public class PlatfornVisualGlitch : MonoBehaviour
{
    [SerializeField] GameObject GlitchCube;

    bool trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !trigger)
        {
            for (int i = 0; i < 12; i++)
            {
                Instantiate(GlitchCube, transform.position + RandomPosCube(), Quaternion.identity);
            }

            trigger = true;
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
