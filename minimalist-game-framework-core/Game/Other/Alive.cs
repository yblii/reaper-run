using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
class Alive : GameObject
{
    public float _health;
    public float curHealth;

    public float _armor;
    public float curArmor;

    public static List<Alive> toKill = new List<Alive>();

    public Sound OnDeath;

    private Bar _healthBar;
    private Bar _shieldBar;

    public bool invulnerable;

    public Alive(float x, float y, float xs, float ys, Texture t, float health, float armor, float curArmor, int layer) : base(x, y, xs, ys, t, layer)
    {
        layer = Layers.Player;

        this._health = health;
        this.curHealth = health;

        this._armor = armor;
        this.curArmor = curArmor;
    }

    public void SetHealthBar(Vector2 pos, Vector2 size, String filePath, Bounds2 fillBounds)
    {
        this._healthBar = new Bar(pos, size, filePath, _health, fillBounds);
        this._healthBar.UpdateBar(curHealth);
    }

    public void SetShieldBar(Vector2 pos, Vector2 size, String filePath, Bounds2 fillBounds)
    {
        this._shieldBar = new Bar(pos, size, filePath, _armor, fillBounds);
        this._shieldBar.UpdateBar(curArmor);
    }

    public virtual void Kill()
    {
        toKill.Add(this);
    }

    public virtual void Damage(float dmg)
    {
        if(curArmor > 0)
        {
            if(curArmor >= dmg)
            {
                curArmor -= dmg;
            }
            else
            {
                dmg -= curArmor;
                curArmor = 0;
            }

            if(_shieldBar != null) _shieldBar.UpdateBar(curArmor);
        }

        curHealth -= dmg;
        if(curHealth <= 0)
        {
            if (this is Player)
            {
                Camera.Clear();
            }
            this.Kill();
        }

        if(_healthBar != null)
        {
            _healthBar.UpdateBar(this.curHealth);
        }
    }

    public void Heal(float heal)
    {
        if(curHealth + heal > _health)
        {
            curHealth = _health;
        }
        else
        {
            curHealth += heal;
        }

        if (_healthBar != null)
        {
            _healthBar.UpdateBar(this.curHealth);
        }
    }

    public void Shield(float shield)
    {
        if(curArmor + shield > _armor)
        {
            curArmor = _armor;
        }
        else
        {
            curArmor += shield;
        }

        if (_shieldBar != null) _shieldBar.UpdateBar(curArmor);
    }

    public static void UpdateAlive()
    {
        foreach(Alive o in toKill)
        {
            Engine.PlaySound(o.OnDeath);
            o.Destroy();
        }
       toKill.Clear();
    }
}
