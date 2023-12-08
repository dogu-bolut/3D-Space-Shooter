using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject astr1;
    [SerializeField] GameObject astr2;
    [SerializeField] GameObject astr3;
    [SerializeField] Text scoreText;
    [SerializeField] Text deathText;
    bool overCheck = false;
    int score;
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        StartCoroutine(create());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Esc pressed");
        }
        if (Input.GetKeyDown(KeyCode.R) && overCheck)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    IEnumerator create()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (overCheck)
            {
                break;

            }
            for (int i = 0; i < 3; i++)
            {
                Vector3 vec = new Vector3(Random.Range(-4.7f, 4.7f), 0, 9);
                Vector3 vec2 = new Vector3(Random.Range(-4.7f, 4.7f), 0, 9);
                Vector3 vec3 = new Vector3(Random.Range(-4.7f, 4.7f), 0, 9);
                Instantiate(astr1, vec, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                Instantiate(astr2, vec2, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                Instantiate(astr3, vec3, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);

            }
            yield return new WaitForSeconds(1);

        }
    }
    public void MakeScore(int getScore)
    {
        score += getScore;
        scoreText.text = score.ToString();
    }
    public void TakeScore(int takeScore)
    {
        score -= takeScore;
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        deathText.text = "You died... unfortunately" + "\nYour score = " + scoreText.text;
        overCheck = true;
        scoreText.gameObject.SetActive(false);
    }
}
