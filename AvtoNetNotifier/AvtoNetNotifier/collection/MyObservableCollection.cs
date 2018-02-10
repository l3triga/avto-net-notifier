using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvtoNetNotifier
{
    class MyObservableCollection<T> : ObservableCollection<T>
    {
        public MyObservableCollection() : base()
        {}

        public MyObservableCollection(List<T> list) : base(list)
        {}

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
