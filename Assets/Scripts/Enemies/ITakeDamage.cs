using System.Collections;
using Bullets;

namespace Enemies
{
    public interface ITakeDamage
    {
        public void TakeDamage(int dmg, Bullet proj);
    }
}