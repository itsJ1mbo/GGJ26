using System.Collections;
using UnityEngine;

public class GoalComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer firstSpark;
    [SerializeField] private SpriteRenderer secondSpark;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;
    [SerializeField] private GameObject finalParticleSystem;


    private Animator firstSparkAnimator;
    private Animator secondSparkAnimator;

    private int _playersInside = 0;
    private bool _levelDone = false;


    private void Start()
    {
        firstSpark.material.SetFloat("_Progress", 0f);
        secondSpark.material.SetFloat("_Progress", 0f);

        firstSparkAnimator = firstSpark.GetComponent<Animator>();
        secondSparkAnimator = secondSpark.GetComponent<Animator>();
    }



    private IEnumerator CoroutineTransitionNextLevel(Color secondSparkColor)
    {
        float timer = 0;


        Color ogColor = secondSpark.color;
        // aplicamos color a el otro brillo
        while (timer < 0.5f)
        {
            secondSpark.color = Color.Lerp(ogColor, secondSparkColor, timer / 0.5f);

            timer += Time.deltaTime;

            yield return null;
        }


        timer = 0;
        finalParticleSystem.SetActive(true);
        while (timer < 1f)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        LevelManager.Instance.NextLevel();
    }
    private void TransitionNextLevel(Color secondSparkColor)
    {
        StartCoroutine(CoroutineTransitionNextLevel(secondSparkColor));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_levelDone && other.CompareTag("Player"))
        {
            _playersInside++;
            
            if (_playersInside == 2)
            {
                _levelDone = true;

                Color newColor = Color.white;
                switch (other.GetComponentInParent<AuraComponent>().GetBaseColor())
                {
                    case AuraComponent.AuraColor.RED:
                        newColor = redColor;
                        break;
                    case AuraComponent.AuraColor.BLUE:
                        newColor = blueColor;
                        break;
                    case AuraComponent.AuraColor.GREEN:
                        newColor = greenColor;
                        break;
                }
                TransitionNextLevel(newColor);
            }
            else
            {
                firstSparkAnimator.Play("GoalAppear");
                secondSparkAnimator.Play("GoalAppear");

                switch (other.GetComponentInParent<AuraComponent>().GetBaseColor())
                {
                    case AuraComponent.AuraColor.RED:
                        firstSpark.color = redColor;
                        break;
                    case AuraComponent.AuraColor.BLUE: 
                        firstSpark.color = blueColor;
                        break;
                    case AuraComponent.AuraColor.GREEN:
                        firstSpark.color = greenColor;
                        break;
                }
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playersInside--;

            if (_playersInside == 0)
            {
                firstSparkAnimator.Play("GoalDissapear");
                secondSparkAnimator.Play("GoalDissapear");
            }
        }
    }
}
