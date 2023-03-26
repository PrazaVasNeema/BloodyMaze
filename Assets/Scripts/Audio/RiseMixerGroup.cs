using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
public static class RiseMixerGroup
{
    public static IEnumerator StartRise(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        audioMixer.SetFloat(exposedParam, -80f);
        Debug.Log("ghjfgjghj");
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.MoveTowards(-80f, 80f, 1f / duration);
            Debug.Log(newVol);
            audioMixer.SetFloat(exposedParam, newVol);
            yield return null;
        }
        yield break;
    }
}