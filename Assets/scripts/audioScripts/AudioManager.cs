using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundsEffectsSO allSoundEffectsSO;
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static AudioManager Instance {get; private set;}

    private float volume = 1f;
    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,.3f);

        
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += Instance_OnDeliverySuccess;
        DeliveryManager.Instance.onDeliveryFail += Instance_onDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickAnything += Instance_OnPickAnything;
        TrashCan.OnTrashAnything += TrashCan_OnTrashAnything;
        BaseCounter.OnDropAnything += Instance_OnDropAnything;
    }

    private void Instance_OnDropAnything(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(allSoundEffectsSO.dropObject[Random.Range(0, allSoundEffectsSO.dropObject.Length)], baseCounter.transform.position);
    }

    private void TrashCan_OnTrashAnything(object sender, System.EventArgs e)
    {
        TrashCan trashCan = sender as TrashCan;
        PlaySound(allSoundEffectsSO.trash[Random.Range(0, allSoundEffectsSO.trash.Length)], trashCan.transform.position);
    }

    private void Instance_OnPickAnything(object sender, System.EventArgs e)
    {
        Player player = sender as Player;
        PlaySound(allSoundEffectsSO.pickupObject[Random.Range(0, allSoundEffectsSO.pickupObject.Length)], player.transform.position);

    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier=1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }


    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(allSoundEffectsSO.cut[Random.Range(0, allSoundEffectsSO.cut.Length)], cuttingCounter.transform.position);
    }

    private void Instance_onDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = sender as DeliveryManager;
        PlaySound(allSoundEffectsSO.deliveryFailed[Random.Range(0, allSoundEffectsSO.deliveryFailed.Length)], deliveryManager.transform.position);
        
    }

    private void Instance_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = sender as DeliveryManager;
        PlaySound(allSoundEffectsSO.deliverySuccess[Random.Range(0, allSoundEffectsSO.deliverySuccess.Length)], deliveryManager.transform.position);

    }

    public void PlayFootStepSound(Vector3 position, float volume=1f)
    {
        AudioSource.PlayClipAtPoint(allSoundEffectsSO.footstep[Random.Range(0, allSoundEffectsSO.footstep.Length)], position);
    }

    public void PlayWarnigSound(Vector3 position)
    { PlaySound(allSoundEffectsSO.warning[0], Vector3.zero); }
    public void PlayCountDownSound()
    { PlaySound(allSoundEffectsSO.warning[Random.Range(0,allSoundEffectsSO.warning.Length)], Vector3.zero); }
    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        { volume = 0f; }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
    }

    public float GetVolume()
    { return volume; }
}
