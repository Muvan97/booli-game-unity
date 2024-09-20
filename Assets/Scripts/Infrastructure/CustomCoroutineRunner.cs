using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class CustomCoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        Coroutine ICoroutineRunner.StartCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
        void ICoroutineRunner.StopCoroutine(IEnumerator logicLoop) => StopCoroutine(logicLoop);
    }
}
