using UnityEngine;

public class moveLoop : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDuration = 2f;
    public float pauseDuration = 1f;

    private float timer = 0f;
    private enum State { Moving, Pausing, Rotating }
    private State currentState = State.Moving;

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case State.Moving:
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                if (timer >= moveDuration)
                {
                    timer = 0f;
                    currentState = State.Pausing;
                }
                break;

            case State.Pausing:
                if (timer >= pauseDuration)
                {
                    timer = 0f;
                    currentState = State.Rotating;
                }
                break;

            case State.Rotating:
                transform.Rotate(0f, 180f, 0f);
                timer = 0f;
                currentState = State.Moving;
                break;
        }
    }
}
