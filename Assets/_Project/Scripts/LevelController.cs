using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Slingshot slingshot;
    

    //private Animator levelAnimator;

    //public static readonly int hashReady = Animator.StringToHash("Ready");
    //public static readonly int hashShot = Animator.StringToHash("Shot");
    //public static readonly int hashFinish = Animator.StringToHash("Finish");

    private void Start()
    {
        //levelAnimator = GetComponent<Animator>();

        slingshot.Shot += OnShot;
        slingshot.Reload();
        //levelAnimator.SetTrigger(hashReady);
    }

    private void OnShot()
    {
        slingshot.Invoke("Reload", 3f);
    }
}
