using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IEnumerator _changeLaneCoroutine;
    private PlayerController _playerController;

    private void OnCollisionEnter(Collision other)
    {
        _playerController.PlayerHit();
    }

    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
        playerController.OnLaneChanged+= PlayerMoveControllerOnOnLaneChanged;
    }

    private void PlayerMoveControllerOnOnLaneChanged(Vector3 obj)
    {
        if(_changeLaneCoroutine != null)
            StopCoroutine(_changeLaneCoroutine);
        _changeLaneCoroutine = DoChangeLane(obj,  _playerController.GetLaneChangeSpeed());
        StartCoroutine(_changeLaneCoroutine);
    }

    private IEnumerator DoChangeLane(Vector3 vector3, float moveSpeed)
    {
        var moveTime = (transform.position - vector3).magnitude / moveSpeed;
        var totalTime = moveTime;

        while (moveTime > 0f)
        {
            moveTime -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, vector3, 1 - moveTime / totalTime);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}