using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using yaSingleton;


namespace Demo.Effects
{
    [CreateAssetMenu(fileName = "Audio Manager", menuName = "Singletons/AudioManager")]
    public class AudioManager : Singleton<AudioManager>
    {

        // public static AudioManager Instance;
        public AudioMixerGroup masterGroup;
        public AudioMixer masterMixer;
        public AudioMixerGroup musicGroup;

        public AudioMixerGroup soundGroup;

        public int lowestDeciblesBeforeMute = -20;


        #region Public Enums

        public enum AudioChannel { Master, Sound, Music }

        #endregion Public Enums

        #region Public Methods

        
        // Plays a sound at the given point in space by creating an empty game object with an
        // AudioSource in that place and destroys it after it finished playing.
        public AudioSource CreatePlaySource(AudioClip clip, Transform emitter, float volume, float pitch, bool music = false)
        {
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = emitter.position;
            go.transform.parent = emitter;

            //Create the source
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            // Output sound through the sound group or music group
            if (music)
                source.outputAudioMixerGroup = musicGroup;
            else
                source.outputAudioMixerGroup = soundGroup;

            source.Play();
            return source;
        }

        public AudioSource Play(AudioClip clip, Transform emitter)
        {
            return Play(clip, emitter, 1f, 1f);
        }

        public AudioSource Play(AudioClip clip, Transform emitter, float volume)
        {
            return Play(clip, emitter, volume, 1f);
        }

        // Plays a sound by creating an empty game object with an AudioSource and attaching it to
        // the given transform (so it moves with the transform). Destroys it after it finished playing.
        public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
        {
            //Create an empty game object
            AudioSource source = CreatePlaySource(clip, emitter, volume, pitch);
            Destroy(source.gameObject, clip.length);
            return source;
        }

        public AudioSource Play(AudioClip clip, Vector3 point)
        {
            return Play(clip, point, 1f, 1f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume)
        {
            return Play(clip, point, volume, 1f);
        }

        
        // Plays a sound at the given point in space by creating an empty game object with an
        // AudioSource in that place and destroys it after it finished playing.

        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
        {
            AudioSource source = CreatePlaySource(clip, point, volume, pitch);
            Destroy(source.gameObject, clip.length);
            return source;
        }

        // Plays the sound effect in a loop. Should destroy the audio source in your script when it
        // is ready to end.

        public AudioSource PlayLoop(AudioClip clip, Transform emitter, float volume = 1f, float pitch = 1f, bool music = true)
        {
            AudioSource source = CreatePlaySource(clip, emitter, volume, pitch, true);
            source.loop = true;
            return source;
        }

        // Plays the sound effect in a loop. Should destroy the audio source in your script when it
        // is ready to end.
        public AudioSource PlayLoop(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, bool music = true)
        {
            AudioSource source = CreatePlaySource(clip, point, volume, pitch, true);
            source.loop = true;
            return source;
        }

        // Adjusts the volume on the audio channel in the unity audio mixer
        public void SetVolume(AudioChannel channel, int volume)
        {
            // Converts the 0 - 100 input into decibles | volume of 0 will mute, 1 should be ~the lowestDecibles set,
            // and 100 should be 0 DB offset from the base volume on the channel
            float adjustedVolume = lowestDeciblesBeforeMute + (-lowestDeciblesBeforeMute / 5 * volume / 20);

            // Effectively completed muted if volume if 0
            if (volume == 0)
            {
                adjustedVolume = -100;
            }

            switch (channel)
            {
                case AudioChannel.Master:
                    masterMixer.SetFloat("MasterVolume", adjustedVolume);
                    break;

                case AudioChannel.Sound:
                    masterMixer.SetFloat("SoundVolume", adjustedVolume);
                    break;

                case AudioChannel.Music:
                    masterMixer.SetFloat("MusicVolume", adjustedVolume);
                    break;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private AudioSource CreatePlaySource(AudioClip clip, Vector3 point, float volume, float pitch, bool music = false)
        {
            //Create an empty game object
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = point;

            //Create the source
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            // Output sound through the sound group or music group
            if (music)
                source.outputAudioMixerGroup = musicGroup;
            else
                source.outputAudioMixerGroup = soundGroup;

            source.Play();
            return source;
        }

        // Set up audio levels
                private void Start()
        {
            // Set the audio levels from player preferences
            int masterVolume = PlayerPrefs.GetInt("MasterVolume", 100);
            int soundVolume = PlayerPrefs.GetInt("SoundVolume", 100);
            int musicVolume = PlayerPrefs.GetInt("MusicVolume", 100);

            // Update the audio mixer
            SetVolume(AudioChannel.Master, masterVolume);
            SetVolume(AudioChannel.Sound, soundVolume);
            SetVolume(AudioChannel.Music, musicVolume);
        }

        #endregion Private Methods
    }

}
