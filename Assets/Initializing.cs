using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initializing : MonoBehaviour
{
    public Button QUIT, START;
    public InputField input;
    public Dropdown listOfFractals;
    public RawImage image;
    List<string> names;
    List<Rule> rules;
    List<Fractal> options;
    public List<Sprite> pictures;
    Regex regex;
    string pattern = "[1-9]+";
    void Start()

    {   
        options = new List<Fractal>();
        listOfFractals.ClearOptions();
        names = new List<string>();
        rules = new List<Rule>();
        rules.Add(new Rule { input = "F", output = "F-F++F-F" });
        Fractal snowflake = ScriptableObject.CreateInstance<Fractal>();
        snowflake.init("Snowflake", "-F++F++F", rules, 60);
        names.Add(snowflake.name);

        rules=new List<Rule>();
        rules.Add(new Rule { input = "X", output = "F+[[X]-X]-F[-FX]+X" });
        rules.Add(new Rule { input = "F", output = "FF" });
        Fractal grass = ScriptableObject.CreateInstance<Fractal>();
        grass.init("Grass", "X", rules, 25);
        names.Add(grass.name);

        rules = new List<Rule>();
        rules.Add(new Rule { input = "X", output = "X+YF+" });
        rules.Add(new Rule { input = "Y", output = "−FX−Y" });
        Fractal dragon = ScriptableObject.CreateInstance<Fractal>();
        dragon.init("Dragon", "FX", rules, 90);
        names.Add(dragon.name);

        rules = new List<Rule>();
        rules.Add(new Rule { input = "A", output = "AA" });
        rules.Add(new Rule { input = "C", output = "A[C]C" });
        Fractal tree = ScriptableObject.CreateInstance<Fractal>();
        tree.init("Tree", "C", rules, 45);
        names.Add(tree.name);

        rules = new List<Rule>();
        rules.Add(new Rule { input = "A", output = "B−A−B" });
        rules.Add(new Rule { input = "B", output = "A+B+A" });
        Fractal triangle = ScriptableObject.CreateInstance<Fractal>();
        triangle.init("Triangle", "A", rules, 45);
        names.Add(triangle.name);
        options.Add(snowflake); options.Add(grass); options.Add(dragon); options.Add(tree); options.Add(triangle);
        listOfFractals.AddOptions(names);
       
    }

    // Update is called once per frame
    void Update()
    {

    }

  public  void showPicture()
    { var a = pictures[listOfFractals.value ];
        PlayerPrefs.fractal = options[listOfFractals.value];
        image.texture = a.texture;
    }

    public void nextLevel()
    {   
        PlayerPrefs.fractal = options[listOfFractals.value];
        if(!string.IsNullOrEmpty(input.text) ||!string.IsNullOrWhiteSpace(input.text))
        {
            Debug.Log(Regex.IsMatch(input.text, pattern));
            if (Regex.IsMatch(input.text, pattern))
            {   
                PlayerPrefs.generations = int.Parse(input.text);
                SceneManager.LoadSceneAsync(1);
            }
        }
            
       
    }
    public void exit()
    {
        Application.Quit();
    }
}
