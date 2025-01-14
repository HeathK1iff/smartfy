using System.Collections;
using System.Text;

namespace Smartfy.DomainModel.Tasks.Utils
{
    public class MessageByGroupBuilder : IEnumerable<KeyValuePair<string, StringBuilder>>
    {
        private Dictionary<string, StringBuilder> _groups = new();
        private string _header;

        public MessageByGroupBuilder(string header)
        {
            _header = header;
        }

        private StringBuilder GetOrCreateGroup(string groupName)
        {
            if (_groups.TryGetValue(groupName, out var group))
            {
                return group;
            }

            var builder = new StringBuilder();
            _groups.Add(groupName, builder);
            return builder;
        }

        public void Add(string groupName, string message)
        {
            var group = GetOrCreateGroup(groupName);
            group.AppendLine(message);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(_header))
                stringBuilder.AppendLine(_header);

            foreach (var group in _groups)
            {
                stringBuilder.AppendLine($"{group.Key}:");
                stringBuilder.Append(group.Value.ToString());
            }

            return stringBuilder.ToString();
        }

        public IEnumerator<KeyValuePair<string, StringBuilder>> GetEnumerator()
        {
            return _groups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _groups.GetEnumerator();
        }
    }
}