using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Slingshot slingshot;
    
    //private Animator levelAnimator;

    //public static readonly int hashReady = Animator.StringToHash("Ready");
    //public static readonly int hashShot = Animator.StringToHash("Shot");
    //public static readonly int hashFinish = Animator.StringToHash("Finish");

    private void Start()
    {
        //levelAnimator = GetComponent<Animator>();
        //cameraController.mainCamera.DOOrthoSize(8f, 3f);

        slingshot.Shot += OnShot;
        slingshot.Reload();
        //levelAnimator.SetTrigger(hashReady);
    }

    private void OnShot()
    {
        cameraController.MoveEnd();
        slingshot.Invoke("Reload", 4f);
        cameraController.Invoke("MoveStart", 4f);
    }
}
