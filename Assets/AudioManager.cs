using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager Instance {
		private set; get;
	}

	public AudioClip shopAudio;

	public AudioClip sceneBGM;

	public AudioClip pickBranchesClip;

	public AudioClip avesCome;

	public AudioClip clickAvesAvatar;

	public AudioSource audioSource;

	private void Awake() {
		Instance = this;
	}



}
