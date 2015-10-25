using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

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
	
	}

    public void AddScore(int sc)
    {
        score += sc * combo;
    }

    public void AddCombo(int co)
    {
        combo += combo;
        Debug.Log(combo);
    }

    public void ResetCombo()
    {
        combo = 1;
    }
}
