using UnityEngine;


public class CameraController : MonoBehaviour
{

    public Camera playerCamera;

    public Camera trainingCamera;


    void Start()
    {

        StartTrainingView();

    }



    public void StartTrainingView()
    {

        playerCamera.enabled = false;

        trainingCamera.enabled = true;

    }



    public void StartPlayerView()
    {

        playerCamera.enabled = true;

        trainingCamera.enabled = false;

    }


}