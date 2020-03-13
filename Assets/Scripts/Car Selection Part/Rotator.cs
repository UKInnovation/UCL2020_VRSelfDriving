using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    public GameObject CanvasBottom;
    public GameObject CanvasLeft;
    public GameObject CanvasRight;
    public GameObject buttonPrefeb;
    public float rotateSpeed = 5;
    public TextAsset text;

    public GameObject[] cars;
    private int currentCarIndex = 0;
    private float currentRotatorAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        createButtons();
        createDiscription();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.y != currentRotatorAngle)
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
        currentRotatorAngle = currentRotatorAngle + 180;
        currentCarIndex++;
        currentCarIndex = currentCarIndex % 4;
        createDiscription();
        destoryButtons();
        createButtons();
    }

    public void prevCar()
    {
        currentRotatorAngle = currentRotatorAngle - 180;
        currentCarIndex--;
        if(currentCarIndex < 0){
            currentCarIndex += 4;
        }
        createDiscription();
        destoryButtons();
        createButtons();
    }

    public void switchColor()
    {
        GameObject car = cars[currentCarIndex];
        GameObject car_apperance = car.transform.Find("Appearance").gameObject;
        GameObject buttonPresed = EventSystem.current.currentSelectedGameObject;
        string buttonName = buttonPresed.GetComponentInChildren<Text>().text;
        // CanvasLeft.GetComponentInChildren<Text>().text = buttonName;
        string materialName = buttonName;
        // Renderer renderer = car.GetComponent<Renderer>();
        Renderer[] renderers = car_apperance.GetComponentsInChildren<Renderer>();
        string materialPath = "Models/" + car.name;
        Material[] materials = Resources.LoadAll<Material>(materialPath);

        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name == materialName)
            {
                for(int j = 0; j < renderers.Length; j++){
                    renderers[j].material = materials[i];
                }
            }
        }
    }

    public void createButtons()
    {
        GameObject car = cars[currentCarIndex];

        string materialPath = "Models/" + car.name + "/Colors";
        Material[] carMaterials = Resources.LoadAll<Material>(materialPath);
        GameObject[] buttonPlaceholders = GameObject.FindGameObjectsWithTag("button-placeholder");

        for(int i = 0; i < carMaterials.Length && i < buttonPlaceholders.Length; i++)
        {
            GameObject button = GameObject.Instantiate(buttonPrefeb);
            GameObject buttonPlaceholder = buttonPlaceholders[i];

            button.transform.SetParent(CanvasBottom.transform);
            button.GetComponent<RectTransform>().localPosition = buttonPlaceholder.GetComponent<RectTransform>().localPosition;
            button.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            button.GetComponentInChildren<Text>().text = carMaterials[i].name;
            button.GetComponentInChildren<Text>().color = new Color(0,0,0,0);
            button.GetComponent<Image>().color = carMaterials[i].color;
            button.GetComponent<Button>().onClick.AddListener(delegate { switchColor(); });
            button.tag = "bottom-canvas-button";
        }
    }

    private void destoryButtons()
    {
        for (int i = 0; i < CanvasBottom.transform.childCount; i++)
        {
            GameObject button = CanvasBottom.transform.GetChild(i).gameObject;
            if(button.tag == "bottom-canvas-button")
            {
                Destroy(button);
            }
        }
    }

    private void createDiscription()
    {
        GameObject car = cars[currentCarIndex];
        string filePath = "Models/" + car.name;

        TextAsset carNameText = (TextAsset)Resources.Load(filePath +  "/name");
        TextAsset carDiscriptionText = (TextAsset)Resources.Load(filePath +  "/description");
        TextAsset carStatisticsText = (TextAsset)Resources.Load(filePath +  "/statistics");

        GameObject carName = CanvasLeft.transform.Find("CarName").gameObject;
        GameObject carDiscription = CanvasLeft.transform.Find("CarDiscription").gameObject;
        GameObject CarStatistics = CanvasRight.transform.Find("CarProperties").gameObject;

        carName.GetComponent<Text>().text = carNameText.text;
        carDiscription.GetComponent<Text>().text = carDiscriptionText.text;

        string[] stats = carStatisticsText.text.Split('\n');

        for(int i = 0; i < stats.Length; i++){
            string property = stats[i].Split(':')[0];
            int value = System.Int32.Parse(stats[i].Split(':')[1]);    

            GameObject image = CarStatistics.transform.Find(property).Find("Image").gameObject;
            image.GetComponentInChildren<RectTransform>().offsetMax = new Vector2(value * 4, image.GetComponent<RectTransform>().offsetMax.y);
        }
    }
}
