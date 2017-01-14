using System.Collections.Generic;
using System.Linq;
using Model;

namespace DbSpyServices
{
    public static class ListExtensions
    {
        public static bool HasItem(this List<ReferenceObject> list, string objectId)
        {
            if (list == null || !list.Any())
                return false;
            return list.Any(o => o.Id == objectId);
        }

        public static ReferenceObject FirstOrDefault(this List<ReferenceObject> list, string objectId)
        {
            return list == null ? null : list.FirstOrDefault(o => o.Id == objectId);
        }

        public static bool IsEmpty<T>(this List<T> list)
        {
            return list == null || !list.Any();
        }
    }
}
