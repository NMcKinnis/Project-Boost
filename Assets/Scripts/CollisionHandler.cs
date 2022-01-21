using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] GameObject winParticles;
    [SerializeField] GameObject loseParticles;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] Transform particleOrigin;
    
    AudioSource audioSource;
    Movement myMover;
    bool isTransitioning = false;
    private void Awake()
    {
        myMover = GetComponent<Movement>();
        if (myMover.enabled == false)
        {
            myMover.enabled = true;
        }
        audioSource = GetComponent<AudioSource>();
       }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {

            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                DisableControlScript();
                if (winParticles) { Instantiate(winParticles, particleOrigin); }
                audioSource.Stop();
                audioSource.PlayOneShot(winSound);
                isTransitioning = true;
                StartCoroutine(HandleWin());
                break;

            default:
                DisableControlScript();
                if (loseParticles) { Instantiate(loseParticles, particleOrigin); }
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
                isTransitioning = true;

                StartCoroutine(HandleLoss());
                break;
        }
    }
    void DisableControlScript()
    {
        myMover.enabled = false;
    }
    IEnumerator HandleLoss()
    {
        yield return new WaitForSeconds(waitTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(waitTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            LoadStartScene();
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    private void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
