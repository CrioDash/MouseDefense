using System.Collections;

namespace Enemies
{
    public interface ITakeDamage
    {
        public void TakeDamage(int dmg, Projectile proj);
    }
}