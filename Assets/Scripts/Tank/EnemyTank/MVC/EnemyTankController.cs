public class EnemyTankController
{
    public EnemyTankModel tankModel;

    public EnemyTankView tankView;

    private TankScriptableObject tankObject;
    private float currentHealth;
    private bool isDead;


    public EnemyTankController(EnemyTankModel _model)
    {
        this.tankModel = _model;
        tankObject = tankModel.GetTankObject();
        currentHealth = tankObject.maxHealth;
        isDead = false;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        tankView.SetHealthUI(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }
    private void OnDeath()
    {
        tankView.OnDeath();
        tankView.gameObject.SetActive(false);
    }
    public void SetTankView(EnemyTankView _tankView)
    {
        tankView = _tankView;
    }
}
