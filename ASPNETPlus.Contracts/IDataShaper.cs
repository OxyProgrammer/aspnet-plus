using ASPNETPlus.Entities.Models;
using System.Net.Http;

namespace ASPNETPlus.Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString);
        ShapedEntity ShapeData(T entity, string fieldsString);
    }

}
