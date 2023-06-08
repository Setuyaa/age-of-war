using age_of_war;
using System;
[Serializable]
public class GulyayGorod
{
    private readonly int _health;
    private readonly int _defence;
    private readonly int _cost;
    private int _currentHealth;

    public GulyayGorod(int health, int defence, int cost)
    {
        _defence = defence;
        _health = _currentHealth = health;
        _cost = cost;
    }

    public int GetDefence()
    {
        return _defence;
    }

    public int GetStrength()
    {
        return 0;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetCost()
    {
        return _cost;
    }

    public void TakeDamage(int oppAtt, int ArmyPrice, int i, Army army)
    {
        // oppAtt - сила атаки
        var minus = (int)Math.Round((decimal)((ArmyPrice - _defence) * oppAtt / 100));
        _currentHealth -= minus;
    }

    public bool IsDead
    {
        get { return _currentHealth <= 0; }
    }
}
