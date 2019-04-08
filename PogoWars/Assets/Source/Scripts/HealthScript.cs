using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    public float _startingHealth = 90f;
    public Slider _slider;
    public Image _fillImage;
    public Color _fullHealthColor = Color.green;
    public Color _zeroHealthColor = Color.red;
    public int _damage = 30;



    private float _currentHealth;
    private bool isDead;

    void OnEnable()
    {
        _currentHealth = _startingHealth;
        isDead = false;

        SetHealthUI();
    }

    // Update is called once per frame
    public void TakeDamage()
    {
        // If the enemy is dead, exit
        if (isDead)
            
            return;

        // Reduce the current health by the damage
        _currentHealth -= _damage;

        SetHealthUI();
        StartCoroutine(Flasher());
        GetComponent<SpriteRenderer>().material.color = Color.white;


        // If the current health is less than or equal to zero and the player isn't dead call death
        if (_currentHealth <= 0 && !isDead)
        {
            
            Death();
        }
    }

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
        _slider.value = _currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        _fillImage.color = Color.Lerp(_zeroHealthColor, _fullHealthColor, _currentHealth / _startingHealth);
    }

   public void Death()
    {
       
        isDead = true;
        // Turn the player off.
        gameObject.SetActive(false);

    }

}
