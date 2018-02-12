using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvtoNetNotifier
{
    class MyObservableCollection<T> : ObservableCollection<T>
    {
        public MyObservableCollection() : base()
        {}

        public MyObservableCollection(IEnumerable<T> list) : base(list)
        {}

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public bool TryGetValue(T inValue, out T outValue)
        {
            outValue = default(T);

            int index = IndexOf(inValue);
            if (index < 0)
                return false;

            outValue = Items[index];
            return true;
        }
    }
}
