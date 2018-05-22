using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWorldScript : MonoBehaviour {

    [SerializeField] GameObject animalPrefab;
    [SerializeField] GameObject hunter1Prefab;
    [SerializeField] GameObject hunter2Prefab;
    [SerializeField] GameObject trapPrefab;
    [SerializeField] GameObject treePrefab;

    int worldMapHeight = 4;
    int worldMapWidth = 4;
    int[,] worldMap = new int[4, 4];

    // Use this for initialization
    void Start () {
        InitWorldMap();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnMouseDown() {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void PrintWorldMap(string msg) {
        print(msg);
        for (int i = 0; i < worldMapHeight; i++) {
            string str = "";
            for (int j = 0; j < worldMapWidth; j++) {
                str += worldMap[i, j];
                //print(worldMap[i, j]);
            }
            print(str);
            //Debug.Log(str);
        }
        print("________");
    }

    // Инициализация карты
    void InitWorldMap() {
        // Заполнение двумерного массива нулями
        for (int i = 0; i < worldMapHeight; i++) {
            for (int j = 0; j < worldMapWidth; j++) {
                worldMap[i, j] = 0;
            }
        }

        /*
         * Как выглядит понятная челвоеку матрица:
         * 0 0 0 0  
         * 0 0 0 1
         * 0 1 2 0
         * 0 0 3 0
         * 
         * Как описывается координата 1
         * [2, 1]
         * 
         *  - В игре на плюсах координата [2, 1] в матрице соответствует координатам на игровом поле
         *  - В юнити же, координаты обьектов в игровом поле это [0.64f, -1.28f]
         *  - Соответственно, задача: Сделать соответствие float значений с целочисленными значениями матрицы
         *  
         *  0       0.64        1.28        1.92
         *  -0.64   0           0           0
         *  -1.28   0           0           0
         *  -1.92   0           0           0
         */

        // Список соответствия float координат персонажей с целочисленными значениями матрицы
        Dictionary<float, int> coordinateMatching = new Dictionary<float, int>() {
            { 0f, 0 },
            { 0.64f, 1 }, { -0.64f, 1 },
            { 1.28f, 2 }, { -1.28f, 2 },
            { 1.92f, 3 }, { -1.92f, 3 }
        };

        // Во избежание ситуаций когда сгенерированная новая пара уже присвоена некоторому объекту, 
        // создаётся Карта(ИмяИгровогоОбьекта, Пара(координатаX, координатаY)), 
        // после чего проверяется наличие пары с координатами в карте
        Dictionary<KeyValuePair<float, float>, string> mGameObjectPair = new Dictionary<KeyValuePair<float, float>, string>();
        KeyValuePair<float, float> tempCoordinate;
        System.Random rnd = new System.Random();

        tempCoordinate = new KeyValuePair<float, float>(1.92f, -0.64f);//(rnd.Next(100) % worldMapHeight, rnd.Next(100) % worldMapWidth);
        mGameObjectPair.Add(tempCoordinate, "hunter1");

        do {
            tempCoordinate = new KeyValuePair<float, float>(0.64f, -1.28f);// (rnd.Next(100) % worldMapHeight, rnd.Next(100) % worldMapWidth);
        } while (mGameObjectPair.ContainsKey(tempCoordinate));
        mGameObjectPair.Add(tempCoordinate, "hunter2");

        do {
            tempCoordinate = new KeyValuePair<float, float>(1.28f, -1.28f);//(rnd.Next(100) % worldMapHeight, rnd.Next(100) % worldMapWidth);
        } while (mGameObjectPair.ContainsKey(tempCoordinate));
        mGameObjectPair.Add(tempCoordinate, "animal1");

        do {
            tempCoordinate = new KeyValuePair<float, float>(1.28f, -1.92f);//(rnd.Next(100) % worldMapHeight, rnd.Next(100) % worldMapWidth);
        } while (mGameObjectPair.ContainsKey(tempCoordinate));
        mGameObjectPair.Add(tempCoordinate, "trap1");


        // Растановка персонажей в игровом поле
        foreach (KeyValuePair<KeyValuePair<float, float>, string> entry in mGameObjectPair) {
            print(entry.Value + ": " + entry.Key.Key + ", " + entry.Key.Value);

            Vector3 characterPosistion = new Vector3(entry.Key.Key, entry.Key.Value, 0);

            switch (entry.Value) {
                case "hunter1":
                    Instantiate(hunter1Prefab, characterPosistion, Quaternion.identity); // Размещение персонажей в игровом поле 
                    worldMap[coordinateMatching[entry.Key.Value], coordinateMatching[entry.Key.Key]] = 1; // Заполнение матрицы
                    break;
                case "hunter2":
                    Instantiate(hunter2Prefab, characterPosistion, Quaternion.identity);
                    worldMap[coordinateMatching[entry.Key.Value], coordinateMatching[entry.Key.Key]] = 1;
                    break;
                case "animal1":
                    Instantiate(animalPrefab, characterPosistion, Quaternion.identity);
                    worldMap[coordinateMatching[entry.Key.Value], coordinateMatching[entry.Key.Key]] = 2;
                    break;
                case "trap1":
                    Instantiate(trapPrefab, characterPosistion, Quaternion.identity);
                    worldMap[coordinateMatching[entry.Key.Value], coordinateMatching[entry.Key.Key]] = 3;
                    break;
                default:
                    break;
            }
        }

        print("1 - Hunter; 2 - Animal; 3 - Trap");

        // Вывод двумерного массива
        PrintWorldMap("Map created:");
    }
}
