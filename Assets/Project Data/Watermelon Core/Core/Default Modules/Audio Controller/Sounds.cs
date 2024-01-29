﻿// GENERATED BY CODE! DO NOT MODIFY!
using UnityEngine;
namespace Watermelon
{
	[System.Serializable]
	public class Sounds
	{
        [LineSpacer("Character")]
        public AudioClip[] characterHit;

        [Space(5)]
        public AudioClip shotMinigun;
        public AudioClip shotShotgun;
        public AudioClip shotLavagun;
        public AudioClip shotTesla;

        [LineSpacer("Enemy")]
        public AudioClip[] enemyScreems;

        public AudioClip enemyMeleeHit;
        public AudioClip enemyShot;
        public AudioClip enemySniperShoot;

        [LineSpacer("Boss")]
        public AudioClip jumpLanding;
        public AudioClip bossScream;
        public AudioClip punch1;
        public AudioClip shoot2;
        public AudioClip explode;

        [LineSpacer("Other")]
        public AudioClip door;
        public AudioClip complete;
        public AudioClip chestOpen;
        public AudioClip coinAppear;
        public AudioClip coinPickUp;
        public AudioClip cardPickUp;

        [LineSpacer("UI")]
        public AudioClip buttonSound;
        public AudioClip upgrade;
    }
}