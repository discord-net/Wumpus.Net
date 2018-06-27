using System.Collections.Generic;

namespace Wumpus.Requests
{
    public interface IFormData
    {
        IDictionary<string, object> GetFormData();
    }
}
