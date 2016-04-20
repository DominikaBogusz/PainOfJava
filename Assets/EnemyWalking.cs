﻿using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class EnemyWalking : MonoBehaviour
    {

        private PlatformerCharacter2D character;

        public Transform from, to;
        public Transform ceilingCheck, groundCheck;

        float speed;
        public float normalSpeed = 20f;
        public float chaseSpeed = 30f;

        bool crouch = false;
        bool jump = false;
        float h;

        bool facingRight = true;

        GameObject player;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void FixedUpdate()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                speed = normalSpeed;
                if (facingRight)
                {
                    h = speed * Time.fixedDeltaTime;
                    if (transform.position.x > to.position.x)
                    {
                        facingRight = false;
                    }
                }
                else
                {
                    h = -speed * Time.fixedDeltaTime;
                    if (transform.position.x < from.position.x)
                    {
                        facingRight = true;
                    }
                }
            }
            else
            {
                if (facingRight)
                {
                    if (isBetween(player.transform.position.y, groundCheck.position.y, ceilingCheck.position.y))
                    {
                        if (isBetween(player.transform.position.x, this.transform.position.x, to.transform.position.x))
                        {
                            speed = chaseSpeed;
                        }
                    }
                    else
                    {
                        speed = normalSpeed;
                    }
                    h = speed * Time.fixedDeltaTime;
                    if (transform.position.x > to.position.x)
                    {
                        facingRight = false;
                    }
                }
                else
                {
                    if (isBetween(player.transform.position.y, groundCheck.position.y, ceilingCheck.position.y))
                    {
                        if (isBetween(player.transform.position.x, from.transform.position.x, this.transform.position.x))
                        {
                            speed = chaseSpeed;
                        }
                    }
                    else
                    {
                        speed = normalSpeed;
                    }
                    h = -speed * Time.fixedDeltaTime;
                    if (transform.position.x < from.position.x)
                    {
                        facingRight = true;
                    }
                }
            }
            character.Move(h, crouch, jump);
        }

        private bool isBetween(float what, float from, float to)
        {
            if (from < what && what < to)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
