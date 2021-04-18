using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Slingshot slingshot;

    [SerializeField] private List<GameObject> balls;

    public static readonly int hashReady = Animator.StringToHash("Ready");
    public static readonly int hashShot = Animator.StringToHash("Shot");
    public static readonly int hashFinish = Animator.StringToHash("Finish");
}
