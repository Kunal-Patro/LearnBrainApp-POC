using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States{
    LeftHandRaise,
    RightHandRaise,
    LeftLegRaise,
    RightLegRaise,
    Ideal
};

public class AnimationController : MonoBehaviour
{
    public float speed;
    [SerializeField] Animator player;
    bool isWAnimationPlaying = false;
    bool isAAnimationPlaying = false;
    bool isSAnimationPlaying = false;
    bool isDAnimationPlaying = false;
    public static bool isMovementChanged = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!isWAnimationPlaying)
            {
                player.SetFloat("speed", speed);
                player.Play(States.RightHandRaise.ToString());
                isWAnimationPlaying = true;
                isAAnimationPlaying = false;
                isSAnimationPlaying = false;
                isDAnimationPlaying = false;
                isMovementChanged = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (!isAAnimationPlaying)
            {
                player.SetFloat("speed", speed);
                player.Play(States.LeftHandRaise.ToString());
                isWAnimationPlaying = false;
                isAAnimationPlaying = true;
                isSAnimationPlaying = false;
                isDAnimationPlaying = false;
                isMovementChanged = true;
                
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isSAnimationPlaying)
            {
                player.SetFloat("speed", speed);
                player.Play(States.RightLegRaise.ToString());
                isWAnimationPlaying = false;
                isAAnimationPlaying = false;
                isSAnimationPlaying = true;
                isDAnimationPlaying = false;
                isMovementChanged = true;
                
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isDAnimationPlaying)
            {
                player.SetFloat("speed", speed);
                player.Play(States.LeftLegRaise.ToString());
                isWAnimationPlaying = false;
                isAAnimationPlaying = false;
                isSAnimationPlaying = false;
                isDAnimationPlaying = true;
                isMovementChanged = true;
                
            }
        }
    }
}
