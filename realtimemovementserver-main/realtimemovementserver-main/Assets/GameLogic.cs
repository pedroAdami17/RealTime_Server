using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    Vector2 characterPositionInPercent;
    Vector2 characterVelocityInPercent;
    const float CharacterSpeed = 0.25f;
    float DiagonalCharacterSpeed;
    void Start()
    {
        NetworkServerProcessing.SetGameLogic(this);
        DiagonalCharacterSpeed = Mathf.Sqrt(CharacterSpeed * CharacterSpeed + CharacterSpeed * CharacterSpeed) / 2f;
    }

    void Update()
    {
        characterPositionInPercent += (characterVelocityInPercent * Time.deltaTime);
    }

    public void UpdateKeyboardInput(int d, int clientId)
    {
        characterVelocityInPercent = Vector2.zero;

        if (d == KbInputDirections.UpRight)
        {
            characterVelocityInPercent.x = DiagonalCharacterSpeed;
            characterVelocityInPercent.y = DiagonalCharacterSpeed;
        }
        else if (d == KbInputDirections.UpLeft)
        {
            characterVelocityInPercent.x = -DiagonalCharacterSpeed;
            characterVelocityInPercent.y = DiagonalCharacterSpeed;
        }
        else if (d == KbInputDirections.DownRight)
        {
            characterVelocityInPercent.x = DiagonalCharacterSpeed;
            characterVelocityInPercent.y = -DiagonalCharacterSpeed;
        }
        else if (d == KbInputDirections.DownLeft)
        {
            characterVelocityInPercent.x = -DiagonalCharacterSpeed;
            characterVelocityInPercent.y = -DiagonalCharacterSpeed;
        }
        else if (d == KbInputDirections.Right)
        {
            characterVelocityInPercent.x = CharacterSpeed;
        }
        else if (d == KbInputDirections.Left)
        {
            characterVelocityInPercent.x = -CharacterSpeed;
        }
        else if (d == KbInputDirections.Up)
        {
            characterVelocityInPercent.y = CharacterSpeed;
        }
        else if (d == KbInputDirections.Down)
        {
            characterVelocityInPercent.y = -CharacterSpeed;
        }
        else if (d == KbInputDirections.NoInput)
        {

        }

        NetworkServerProcessing.SendMessageToClient(ServerToClientSignifiers.VelocityAndPosition + ","
            + characterVelocityInPercent.x + "," + characterVelocityInPercent.y + ","
            + characterPositionInPercent.x + "," + characterPositionInPercent.y + ",", clientId, TransportPipeline.ReliableAndInOrder);
    }




}
