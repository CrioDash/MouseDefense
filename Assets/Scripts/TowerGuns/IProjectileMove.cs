using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;

namespace Towers.TowerGuns
{
    public interface IProjectileMove
    {
        public IEnumerator Move()
        {
            return null;
        }
    }
}