namespace UserManagement.Infrastructure.Persistence.Csv;

public interface ICsvSerializer<T>
{
  IReadOnlyList<T> Deserialize(string content);
  string Serialize(IEnumerable<T> items);
  string SerializeHeaderOnly();

  string SerializeLine(T item);
}
