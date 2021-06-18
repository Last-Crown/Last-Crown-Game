using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        animator.SetBool("Fade", true);
        StartCoroutine(WaitSec(1, sceneName));
    }

    IEnumerator WaitSec(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
