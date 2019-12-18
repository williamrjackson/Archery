using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    [SerializeField]
    private DrawManager drawManager = null;
    [SerializeField]
    private ArrowBehavior arrowTemplate = null;
    [SerializeField]
    private float drawDuration = 3f;
    [SerializeField]
    private bool pingPongPower = false;

    private Coroutine drawRoutine;

    // Start is called before the first frame update
    void Start()
    {
        if (drawManager == null)
        {
            FindObjectOfType<DrawManager>();
        }
        // Disable the arrow template
        arrowTemplate.gameObject.SetActive(false);
    }

    void Update()
    {
        // Enter draw mode
        if (Input.GetMouseButtonDown(0))
        {
            drawRoutine = StartCoroutine(DrawBow());
        }
        // Shoot
        else if (Input.GetMouseButtonUp(0))
        {
            if (drawRoutine != null)
            {
                StopCoroutine(drawRoutine);
                drawRoutine = null;
            }
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        // Spawn a new arrow and shoot it with the current drawstrength
        ArrowBehavior spawnedArrow = Instantiate(arrowTemplate);
        spawnedArrow.gameObject.SetActive(true);
        spawnedArrow.Shoot(drawManager.DrawStrength);
        drawManager.DrawStrength = 0f;
    }

    private IEnumerator DrawBow()
    {
        // Bounce the power back and forth
        if (pingPongPower)
        {
            float startTime = Time.time;

            while (true)
            {
                drawManager.DrawStrength = Mathf.PingPong((Time.time - startTime) * .5f, 1f);
                yield return new WaitForEndOfFrame();
            }
        }
        // Or just increase it over time
        else
        {
            float elapsedTime = 0f;

            while (elapsedTime < drawDuration)
            {
                elapsedTime += Time.deltaTime;
                drawManager.DrawStrength = Mathf.InverseLerp(0f, drawDuration, elapsedTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
