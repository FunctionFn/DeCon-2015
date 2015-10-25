using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text comboText;


    public float score;

    public int combo;

    private static GameController _inst;
    public static GameController Inst { get { return _inst; } }

    void Awake()
    {
        _inst = this;
    }


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + score.ToString();
        comboText.text = "Combo: " + combo.ToString();
	}

    public void AddScore(int sc)
    {
        score += sc * combo;
    }

    public void AddCombo(int co)
    {
        combo += co;
    }

    public void ResetCombo()
    {
        combo = 1;
    }
}
