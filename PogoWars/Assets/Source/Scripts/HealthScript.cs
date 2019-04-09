using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    public float startingHealth = 90f;
    public Slider slider;
    public Image fillImage;
    public Color fullHealthColour = Color.green;
    public Color zeroHealthColor = Color.red;
    public int damage = 30;



    private float _currentHealth;
    private bool _isDead;

    void OnEnable()
    {
        //set the current health to the starting health
        _currentHealth = startingHealth;
        _isDead = false;

        SetHealthUI();
    }

    // Update is called once per frame
    public void TakeDamage()
    {
        // If the enemy is dead, exit
        if (_isDead)
            
            return;

        // Reduce the current health by the damage
        _currentHealth -= damage;

        SetHealthUI();
        StartCoroutine(Flasher());
        GetComponent<SpriteRenderer>().material.color = Color.white;


        // If the current health is less than or equal to zero and the player isn't dead call death
        if (_currentHealth <= 0 && !_isDead)
        {
            
            Death();
        }
    }

    //makes the player flash when hit.
    private IEnumerator Flasher()
    {

        GetComponent<SpriteRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(.3f);
        GetComponent<SpriteRenderer>().material.color = Color.white;
        yield return new WaitForSeconds(.3f);

    }

    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        slider.value = _currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColour, _currentHealth / startingHealth);
    }

   public void Death()
    {
       
        _isDead = true;
        // Turn the player off.
        gameObject.SetActive(false);

    }

}
