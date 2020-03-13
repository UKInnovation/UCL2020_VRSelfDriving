using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class rotate_demo : MonoBehaviour
{
    public GameObject Panel;
    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public float rotateSpeed = 5;

    public GameObject[] cars;
    private int currentCarIndex = 0;
    private float currentRotatorAngle = 0;



    // Start is called before the first frame update
    void Start()
    {
        createDiscription();
        //SwitchColour();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.y != currentRotatorAngle)
        {
            rotate();
        }
    }

    private void rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(0, currentRotatorAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void nextCar()
    {
        currentRotatorAngle += 180f;
        currentCarIndex++;
        currentCarIndex = currentCarIndex % 2;
        createDiscription();

    }

    public void prevCar()
    {
        currentRotatorAngle -= 180f;
        currentCarIndex--;
        if (currentCarIndex < 0)
        {
            currentCarIndex += 2;
        }
        createDiscription();
    }

    private void createDiscription()
    {
        GameObject car = cars[currentCarIndex];
        string filePath = "Models/" + car.name;

        TextAsset carNameText = (TextAsset)Resources.Load(filePath + "/name");
        TextAsset carDiscriptionText = (TextAsset)Resources.Load(filePath + "/description");

        GameObject carName = Panel.transform.Find("Car_name").gameObject;
        GameObject carDiscription = Panel.transform.Find("Car_description").gameObject;

        carName.GetComponent<Text>().text = carNameText.text;
        carDiscription.GetComponent<Text>().text = carDiscriptionText.text;
    }

    public void SwitchGray()
    {
        for(int j = 0; j < cars.Length; j++)
        {
            GameObject car_apperance = cars[j].transform.Find("Appearance").gameObject;
            Renderer car_renderer = car_apperance.GetComponentInChildren<Renderer>();
            string materialPath = "Models/" + cars[j].name;
            Material[] materials = Resources.LoadAll<Material>(materialPath);
            Debug.Log("Gray");
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name == "gray")
                {
                    car_renderer.material = materials[i];
                }
            }
        }
    }

    public void SwitchBrown()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_apperance = cars[j].transform.Find("Appearance").gameObject;
            Renderer car_renderer = car_apperance.GetComponentInChildren<Renderer>();
            string materialPath = "Models/" + cars[j].name;
            Material[] materials = Resources.LoadAll<Material>(materialPath);
            Debug.Log(cars[j].gameObject.name);
            Debug.Log(car_renderer.name);
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name == "brown")
                {
                    car_renderer.material = materials[i];
                }
            }
        }
    }

    public void SwitchWhite()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_apperance = cars[j].transform.Find("Appearance").gameObject;
            Renderer car_renderer = car_apperance.GetComponentInChildren<Renderer>();
            string materialPath = "Models/" + cars[j].name;
            Material[] materials = Resources.LoadAll<Material>(materialPath);
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name == "white")
                {
                    car_renderer.material = materials[i];
                }
            }
        }
    }
}
