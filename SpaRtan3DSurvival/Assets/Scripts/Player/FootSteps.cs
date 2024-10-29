using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    private AudioSource audioSource;
    private Rigidbody rigid;

    public float footstepTHreshold;
    public float footstepRate;
    private float footstepTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Mathf.Abs(rigid.velocity.y) < 0.1f)
        {
            if(rigid.velocity.magnitude > footstepTHreshold)
            {
                if(Time.time - footstepTime > footstepRate)
                {
                    footstepTime = Time.time;
                    audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]); 
                }
            }
        }
    }
}