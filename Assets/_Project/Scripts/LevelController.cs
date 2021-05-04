using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Slingshot slingshot;

    [SerializeField] private GameObject finishedPanel;

    private List<GameObject> enemies;
    //private Animator levelAnimator;

    //public static readonly int hashReady = Animator.StringToHash("Ready");
    //public static readonly int hashShot = Animator.StringToHash("Shot");
    //public static readonly int hashFinish = Animator.StringToHash("Finish");

    private void OnEnable()
    {
        slingshot.Shot += OnShot;
    }

    private void OnDisable()
    {
        slingshot.Shot -= OnShot;
    }

    private void Start()
    {
        //levelAnimator = GetComponent<Animator>();
        //cameraController.mainCamera.DOOrthoSize(8f, 3f);

        slingshot.Reload();
        //levelAnimator.SetTrigger(hashReady);

        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void OnShot(Ball shotBall)
    {
        //cameraController.MoveEnd();
        cameraController.FocusLevel();
        StartCoroutine(WaitForResult());
        //slingshot.Invoke("Reload", 4f);
        //cameraController.Invoke("MoveStart", 4f);
    }

    private IEnumerator WaitForResult()
    {
        yield return new WaitForSeconds(4f);

        if(!CheckAliveEnemies())
        {
            // Level cleared
            finishedPanel.SetActive(true);
            yield return null;
        }

        if(slingshot.Reload() == null)
        {
            // Game over
            finishedPanel.SetActive(true);
        }
    }

    private bool CheckAliveEnemies()
    {
        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            if(enemies[i] == null) enemies.RemoveAt(i);
        }

        if(enemies.Count > 0) return true;

        return false;
    }
}
