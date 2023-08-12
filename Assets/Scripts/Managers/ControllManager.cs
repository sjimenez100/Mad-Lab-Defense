using UnityEngine;

public class ControllManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            EventManager.main.OnSingleInputMove(Direction.Directions.Up);
        else if(Input.GetKeyDown(KeyCode.A))
            EventManager.main.OnSingleInputMove(Direction.Directions.Left);
        else if (Input.GetKeyDown(KeyCode.S))
            EventManager.main.OnSingleInputMove(Direction.Directions.Down);
        else if (Input.GetKeyDown(KeyCode.D))
            EventManager.main.OnSingleInputMove(Direction.Directions.Right);

        if (Input.GetKeyDown(KeyCode.H))
            EventManager.main.OnSingleInputOption(0);
        else if (Input.GetKeyDown(KeyCode.J))
            EventManager.main.OnSingleInputOption(1);
        else if (Input.GetKeyDown(KeyCode.K))
            EventManager.main.OnSingleInputOption(2);
        else if (Input.GetKeyDown(KeyCode.L))
            EventManager.main.OnSingleInputOption(3);

        if (Input.GetKeyDown(KeyCode.Space))
            EventManager.main.OnSingleInputFire();

        if (Input.GetKeyDown(KeyCode.Escape))
            EventManager.main.OnGameStateToggle();
    }


    

}
