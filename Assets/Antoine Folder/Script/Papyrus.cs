using UnityEngine;

public class Papyrus : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector2 ActionWait;

    Animator animator;
    Camera cam;

    Vector3 GoTo;
    Vector3 CurrentVelocity;

    float maxY;
    float currentspeed;

    [SerializeField] float timerMove = 0;
    [SerializeField] Vector2 TravelSpeed;
    string move = "0";


    private void Start()
    {
        cam = Camera.main;
        timerMove = Random.Range(ActionWait.x, ActionWait.y);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 PlayerPos = Player.transform.position;
        transform.LookAt(PlayerPos);

        if (maxY < PlayerPos.y) maxY = Player.transform.position.y;

        timerMove -= Time.deltaTime;
        if (timerMove <= 0)
        {
            if (move == "0")
            {
                currentspeed = Random.Range(TravelSpeed.x, TravelSpeed.y);

                if (Random.Range(0, 2) == 0)
                {
                    GoTo = new Vector3(Random.Range(-2.5f, 2.5f), maxY + Random.Range(-1.5f, 5f), Random.Range(-5, 5));
                }
                else
                {
                    if (transform.position.x > 0)
                    {
                        GoTo = new Vector3(Random.Range(-6.5f, -3f), maxY + Random.Range(-2f, 5f), Random.Range(-5f, 5f));
                    }
                    else
                    {
                        GoTo = new Vector3(Random.Range(3f, 6.5f), maxY + Random.Range(-2f, 5f), Random.Range(-5f, 5f));
                    }
                }

                move = "move";
            }
        }

        if (move == "move")
        {
            transform.position = Vector3.SmoothDamp(transform.position, GoTo, ref CurrentVelocity, currentspeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, GoTo) <= 0.5f)
            {
                Wait();
            }
        }
    }
    void Wait()
    {
        move = "0";
        timerMove = Random.Range(ActionWait.x, ActionWait.y);
    }
}
