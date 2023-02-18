using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    public PlayerController actor;
    public Slider sliderHP;
    public Slider sliderStamina;

    public void Start()
    {
        actor.getHurt += IncreaseHealth;
        actor.actorAttack.OnStaminaChanges += IncreaseStamina;
    }

    public void OnDestroy()
    {
        actor.getHurt -= IncreaseHealth;
        actor.actorAttack.OnStaminaChanges -= IncreaseStamina;
    }

    public void OnDisable()
    {
        actor.getHurt -= IncreaseHealth;
        actor.actorAttack.OnStaminaChanges -= IncreaseStamina;
    }

    private void IncreaseHealth()
    {
        sliderHP.value = actor.actorData.Health;
    }

    private void IncreaseStamina()
    {
        sliderStamina.value = actor.actorAttack.Stamina;
    }
}
