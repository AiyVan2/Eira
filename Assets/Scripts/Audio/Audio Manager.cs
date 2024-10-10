using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Player Audio
    public AudioClip jumpSound;
    public AudioClip slashSound;
    public AudioClip dashSound;
    public AudioClip healSound;
    public AudioClip rangeSound;
    public AudioClip takingdamageSound;


    //Enemy Death Audio
    public AudioClip enemydeathSound;

    //BOSSES
    //Collosal Boss
    public AudioClip collosalmeleeattackSound;
    public AudioClip collosalrangedattackSound;
    public AudioClip collosaldashattackSound;


    //Archivus Boss
    public AudioClip archivusattackSound;


    //Umbra Solis Boss
    public AudioClip umbrasolisattack1Sound;
    public AudioClip umbrasolisattack2Sound;
    public AudioClip umbrasolisattack3impactSound;
    public AudioClip umbrasolisattack3swordSound;
    public AudioClip umbrasolisvanishSound;


    //Corrupted Sentinel Attack Sound
    public AudioClip corruptedsentinelattackSound;

    // Hall of Knowledge Sounds
    public AudioClip enchantedquillAttackSound;
    public AudioClip enchantedquillSummonSound;
    public AudioClip spectralScholarAttackSound;

    // Astronomy Tower Enemy
    public AudioClip meteorstrikerAttackSound;
    public AudioClip astroSentinelAttackSound;


    //Lumin Shard Pickup
    public AudioClip luminshardSound;

    private AudioSource audioSource;

    void Awake()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Method to play sound effects
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Player Sounds
    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayAttackSound()
    {
        PlaySound(slashSound);
    }

    public void PlayTakingDamageSound()
    {
        PlaySound(takingdamageSound);
    }

    public void PlayDashSound()
    {
        PlaySound(dashSound);
    }

    public void PlayHealSound()
    {
        PlaySound(healSound);
    }

    public void PlayRangeSound()
    {
        PlaySound(rangeSound);
    }

    public void PlayEnemyDeathSound()
    {
        PlaySound(enemydeathSound);
    }



    //Enemy Sounds
    public void PlayCorruptedSentinelAttackSound()
    {
        PlaySound(corruptedsentinelattackSound);
    }
    public void PlayEnchanctedQuillAttackSound()
    {
        PlaySound(enchantedquillAttackSound);
    }
    public void PlayEnchanctedQuillSummonSound()
    {
        PlaySound(enchantedquillSummonSound);
    }
    public void PlaySpectralScholarAttackSound()
    {
        PlaySound(spectralScholarAttackSound);
    }

    public void PlayMeteorStrikerAttackSound()
    {
        PlaySound(meteorstrikerAttackSound);
    }

    public void PlayAstroSentinelAttackSound()
    {
        PlaySound(astroSentinelAttackSound);
    }

    //Lumin Shard Pickup Sound
    public void PlayLuminShardPickupSound()
    {
        PlaySound(luminshardSound);
    }

    //Boss Void
    //Collosal Sounds
    public void PlayCollosalMeleeAttackSound()
    {
        PlaySound(collosalmeleeattackSound);
    }
    public void PlayCollosalRangedAttackSound()
    {
        PlaySound(collosalrangedattackSound);
    }
    public void PlayCollosalDashAttackSound()
    {
        PlaySound(collosaldashattackSound);
    }


    //Archivus Sound
    public void PlayArchivusAttackSound()
    {
        PlaySound(archivusattackSound);
    }


    //Umbra Solis Sound
    public void PlayUmbraSolisFirstAttackSound()
    {
        PlaySound(umbrasolisattack1Sound);
    }
    public void PlayUmbraSolisSecondAttackSound()
    {
        PlaySound(umbrasolisattack2Sound);
    }

    public void PlayUmbraSolisThirdAttackImpactSound()
    {
        PlaySound(umbrasolisattack3impactSound);
    }

    public void PlayUmbraSolisThirdAttackSwordSound()
    {
        PlaySound(umbrasolisattack3swordSound);
    }

    public void PlayUmbraSolisVanishSound()
    {
        PlaySound(umbrasolisvanishSound);
    }



    // Method to play background music
    public void PlayBackgroundMusic()
    {
        audioSource.loop = true;
        audioSource.Play();
    }
}