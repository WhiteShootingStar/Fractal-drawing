using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trail : MonoBehaviour
{

    string start = PlayerPrefs.fractal.start;
    Fractal fractal;
    private float length = 3;
    List<Rule> list;
    Stack<Save> saves;
    int counter = 0;
    float mult;

    Camera camera;
    float a = 0;
    private void Awake()
    {
        transform.position = new Vector2(0, 0);
        camera = Camera.main;
        saves = new Stack<Save>();
        fractal = PlayerPrefs.fractal;
        list = fractal.rules;
        for (int i = 0; i < PlayerPrefs.generations; i++)
        {

            start = RuleTransformer.TransfromString(start, list);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

        AnimationCurve animationCurve = new AnimationCurve();
        animationCurve.AddKey(0f, 0.05f * PlayerPrefs.generations);
        GetComponent<TrailRenderer>().widthCurve = animationCurve;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.orthographicSize += 2.5f * length;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            camera.orthographicSize -= 2.5f * length;
        }




        if (counter < start.Length)
        {
            proceedInputValue(start[counter].ToString());
            counter++;
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }


        if (counter == start.Length)
        {

            if (Input.GetMouseButton(1))
            {
                camera.transform.Translate(new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * length * length);
            }
        }




    }


    void proceedInputValue(string command)
    {
        for (int i = 0; i < command.Length; i++)
        {

            if (command[i] == 'F' || command[i] == 'A' || command[i] == 'B' || command[i] == 'G')
            {
                GetComponent<TrailRenderer>().emitting = true;
                var Direction = new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * length * 5;
                transform.Translate(Direction, Space.World);

            }
            else if (command[i] == 'C')
            {
                var Direction = new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * length * 5;
                GetComponent<TrailRenderer>().emitting = false;
                transform.Translate(Direction, Space.World);
            }
            else if (command[i] == '+')
            {
                a += fractal.turnRate * Mathf.Deg2Rad;
            }
            else if (command[i] == '-')
            {
                a -= fractal.turnRate * Mathf.Deg2Rad;
            }
            else if (command[i] == '[')
            {

                saves.Push(new Save { a = a, position = transform.position });
                if (fractal.name.Equals("Tree"))
                {
                    a += 45 * Mathf.Deg2Rad;
                }

            }
            else if (command[i] == ']')
            {

                Save save = saves.Pop();
                transform.position = save.position;
                a = save.a;
                if (fractal.name.Equals("Tree"))
                {
                    a -= 45 * Mathf.Deg2Rad;
                }
            }
        }



    }


    public void exit()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void goBack()
    {
        SceneManager.LoadSceneAsync(0);
    }

}


public class Save
{
    public Vector3 position { get; set; }
    public float a { get; set; }

    public static Save getFromSaveList(List<Save> saves)
    {
        Save save = saves[saves.Count - 1];
        saves.RemoveAt(saves.Count - 1);
        return save;

    }
}
