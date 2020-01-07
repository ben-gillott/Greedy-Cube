using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    public float m_JumpForce = 300f;	
    public Vector3 StartPos = new Vector3(0f, 0f, 0f);
    private bool grounded = true;
    private Rigidbody2D rb;
    public CanvasGroup myCG;
    private bool flash = false;
    public Text scoreTxt;
    private int score = 0;

    private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();

        if (Application.isEditor) {
			for (int i = 0; i < SceneManager.sceneCount; i++) {
				Scene loadedScene = SceneManager.GetSceneAt(i);
				if (loadedScene.name.Contains("Level ")) {
					SceneManager.SetActiveScene(loadedScene);
					return;
				}
			}
		}
        
        StartCoroutine(LoadLevel(1));
	}

	IEnumerator LoadLevel (int levelBuildIndex) {
		enabled = false;
		yield return SceneManager.LoadSceneAsync(
			levelBuildIndex, LoadSceneMode.Additive
		);
		SceneManager.SetActiveScene(
			SceneManager.GetSceneByBuildIndex(levelBuildIndex)
		);
		enabled = true;
	}

	private void Update()
	{

        //Handle left right change
        int horizontalChange = 0;

		if (Input.GetKey("left"))
        {
            horizontalChange --;
        }
        if (Input.GetKey("right"))
        {
            horizontalChange ++;
        }
        Vector3 destination = this.transform.position + new Vector3(horizontalChange, 0, 0);
        this.transform.position = Vector3.Lerp(this.transform.position, destination, (float).1);


        // On jump add a vertical force to the player.
		if (Input.GetKey("space") && grounded)
		{
		    grounded = false;
			rb.AddForce(new Vector2(0f, m_JumpForce));
		}


        //On Death
        if(this.transform.position.y < -10 && flash == false){
            //Setup flash effect
            flash = true;
            myCG.alpha = 1;

            //Reset position and velocity
            this.transform.position = StartPos;
            rb.velocity = new Vector2(0f, 0f);
            
            //Reset the scoreboard
            score ++;
            scoreTxt.text = "Deaths: " + score;
        }
        if (flash)
        {
            myCG.alpha = myCG.alpha - Time.deltaTime;
            
            //Flash done
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }


	}

    void OnCollisionExit2D(Collision2D other) {
        grounded = false;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name == "Endpoint"){
            //Transition to the next level
            Debug.Log("Hit end");
            StartCoroutine(LoadLevel(2));
        }
        grounded = true;
    }


}
