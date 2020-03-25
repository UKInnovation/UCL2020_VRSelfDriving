using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rotate_demo : MonoBehaviour
{
    public GameObject Panel;
    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public Material sticker0;
    public Material sticker1;
    public Material sticker2;
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
        currentRotatorAngle += 179;
        currentCarIndex++;
        currentCarIndex = currentCarIndex % 2;
        createDiscription();

    }

    public void prevCar()
    {
        currentRotatorAngle -= 179;
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
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_apperance = cars[j].transform.Find("Appearance").gameObject;
            print(car_apperance.name);
            Renderer car_renderer = car_apperance.GetComponentInChildren<Renderer>();
            print(car_renderer);
            string materialPath = "Models/" + cars[j].name;
            Material[] materials = Resources.LoadAll<Material>(materialPath);
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name == "gray")
                {
                    car_renderer.material.color = materials[i].color;
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
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name == "brown")
                {
                    car_renderer.material.color = materials[i].color;
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
                    car_renderer.material.color = materials[i].color;
                }
            }
        }
    }

    public void resetColour()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_apperance = cars[j].transform.Find("Appearance").gameObject;
            Renderer car_renderer = car_apperance.GetComponentInChildren<Renderer>();
            GameObject reset_apperance = cars[j].transform.Find("reset").gameObject;
            Renderer reset_renderer = reset_apperance.GetComponentInChildren<Renderer>();
            car_renderer.material.color = reset_renderer.material.color;
        }
    }

    public void resetDecal()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_apperance = cars[j].transform.Find("DecalComponents").gameObject;
            Renderer[] car_renderer = car_apperance.GetComponentsInChildren<Renderer>();
            GameObject reset_apperance = cars[j].transform.Find("resetDecal").gameObject;
            Renderer[] reset_renderer = reset_apperance.GetComponentsInChildren<Renderer>();
            for (int k = 0; k < car_renderer.Length; k++)
            {
                car_renderer[k].material = reset_renderer[k].material;
            }
        }
    }

    public void changeDecal0()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_decal = cars[j].transform.Find("DecalComponents").gameObject;
            Renderer[] car_renderer = car_decal.GetComponentsInChildren<Renderer>();
            for (int k = 0; k < car_renderer.Length; k++)
            {
                car_renderer[k].material = sticker0;
            }
        }
    }

    public void changeDecal1()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_decal = cars[j].transform.Find("DecalComponents").gameObject;
            Renderer[] car_renderer = car_decal.GetComponentsInChildren<Renderer>();
            for (int k = 0; k < car_renderer.Length; k++)
            {
                car_renderer[k].material = sticker1;
            }
        }
    }

    public void changeDecal2()
    {
        for (int j = 0; j < cars.Length; j++)
        {
            GameObject car_decal = cars[j].transform.Find("DecalComponents").gameObject;
            Renderer[] car_renderer = car_decal.GetComponentsInChildren<Renderer>();
            for (int k = 0; k < car_renderer.Length; k++)
            {
                car_renderer[k].material = sticker2;
            }
        }
    }

    public void StartGame()
    {
        if (currentCarIndex == 0)
        {
            SceneManager.LoadScene("LoadingSceneTesla");
        }
        else
        {
            SceneManager.LoadScene("LoadingSceneSportCar");
        }
    }
}
