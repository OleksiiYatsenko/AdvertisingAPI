using System.Collections.Generic;
using System.Linq;

namespace AdvertisingServer.Infrastructure.Containers
{
    public class StringsContainer
    {
        private List<string> _container;

        public bool HasValues => _container.Any();

        public StringsContainer()
        {
            _container = new List<string>();
        }

        public void Add(string value)
        {
            _container.Add(value);
        }

        public override string ToString()
        {
            return this.ToString(",");
        }

        public string ToString(string separator)
        {
            return string.Join(separator, _container);
        }
    }
}
