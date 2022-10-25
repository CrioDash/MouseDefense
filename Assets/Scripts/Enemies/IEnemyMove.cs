using System.Collections;

namespace Enemies
{
    public interface IEnemyMove
    {
        public IEnumerator Move(Enemy enemy);
    }
}