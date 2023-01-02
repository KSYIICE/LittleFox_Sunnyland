using UnityEngine;

public class Enter : MonoBehaviour
{
    public GameObject enterText;
    public Animator enterAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterText.SetActive(true);
            enterAnim.SetBool("exit", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterAnim.SetBool("exit", true);
            Invoke("Func", 0.2f);
        }
    }

    private void Func()
    {
        enterText.SetActive(false);
    }
}
