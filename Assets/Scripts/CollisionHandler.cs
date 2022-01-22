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

    // dev mode timer
    float devModeToggleWaitTime = 3f;
    float timeSinceDevModeWasToggled = Mathf.Infinity;

    //state
    bool noCollision = false;
    bool developerMode = false;
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
    private void Update()
    {
        ToggleDevMode();
        if (developerMode)
        {
            DevModeCollision();
            DevModeWinLevel();
        }
        timeSinceDevModeWasToggled += Time.deltaTime;
    }

    private void DevModeWinLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("An easy path to victory eh?");
            StartCoroutine(HandleWin());
        }
    }

    private void DevModeCollision()
    {
        if (!noCollision && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Collisions turned off");
            noCollision = true;
        }
        else if (noCollision && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Collisions turned on");
            noCollision = false;
        }
    }

    private void ToggleDevMode()
    {
        if (!developerMode && timeSinceDevModeWasToggled > devModeToggleWaitTime && Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Dev mode on");
            timeSinceDevModeWasToggled = 0f;
            developerMode = true;
        }
        else if (developerMode && timeSinceDevModeWasToggled > devModeToggleWaitTime && Input.GetKeyDown(KeyCode.M))
        {

            Debug.Log("Dev mode off");
            timeSinceDevModeWasToggled = 0f;
            developerMode = false;
            noCollision = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
        {
            return;
        }
        if (noCollision)
        {
            return;
        }
        else
        {
            switch (collision.gameObject.tag)
            {

                case "Friendly":
                    Debug.Log("This thing is Friendly");
                    break;
                case "Finish":
                    DisableControlScript();
                    if (winParticles) { Instantiate(winParticles, particleOrigin); }
                    audioSource.Stop();
                    audioSource.clip = winSound;
                    audioSource.Play();
                    isTransitioning = true;
                    StartCoroutine(HandleWin());
                    break;

                default:
                    DisableControlScript();
                    if (loseParticles) { Instantiate(loseParticles, particleOrigin); }
                    audioSource.Stop();
                    audioSource.clip = deathSound;
                    audioSource.Play();
                    isTransitioning = true;
                    StartCoroutine(HandleLoss());
                    break;
            }

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
