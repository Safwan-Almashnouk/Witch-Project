using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterSwap : MonoBehaviour
{
    private GameObject character;
    [SerializeField] List<GameObject> possibleCharacters;
    private int whichCharacter;

    [SerializeField] FollowPlayer camera;

  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwapCharacter();
        }
    }

    void SwapCharacter()
    {
        character = possibleCharacters[whichCharacter];
        Vector3 currentPosition = character.transform.position;
        character.SetActive(false);
        whichCharacter = (whichCharacter + 1) % possibleCharacters.Count;
        character = possibleCharacters[whichCharacter];
        character.transform.position = currentPosition;
        character.SetActive(true);

        camera.player = character.transform;


    }
}
