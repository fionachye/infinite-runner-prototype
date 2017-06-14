﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : MonoBehaviour {

    public float moveSpeed;
    float _relativeSpeedToGround;

    void Start ()
    {

    }

    void Update ()
    {
        if (!InCameraView())
        {
            PoolManager.instance.ReturnObjectToPool(gameObject);
        }
        _relativeSpeedToGround = -1 * (Player.Instance.relativeSpeedToGround + moveSpeed);
        transform.Translate(new Vector2(_relativeSpeedToGround, 0f) * Time.deltaTime);
    }

    public void SetParams(Vector2 position)
    {
        transform.position = position;
    }

    bool InCameraView()
    {
        Vector3 screenPoint = GameManager.instance._cameraRef.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x > -0.2f && screenPoint.y > 0f && screenPoint.y < 1f;
        return onScreen;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.collider.gameObject.transform.parent != null && collision.collider.gameObject.transform.parent.name == "Hurtbox")
            {
                Player.Instance.StatTracker("hornPick");
                //SoundManager.Instance.EnemyPlayOneShot(SoundManager.Instance.explodeBird);
            }
            PoolManager.instance.ReturnObjectToPool(gameObject);
        }
    }
}
