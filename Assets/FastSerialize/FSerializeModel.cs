namespace FSerialize
{
    public class FSerializeModel : IFSerialize
    {
        public byte[] Serialize()
        {
            return FastSerialize.Serialize(this);
        }

        public void DeSerialize<T>(byte[] data)
        {
            T serialized_class = (T)FastSerialize.DeSerialize<T>(data);
            FastSerialize.CopyClassData(serialized_class, this);
        }

        public object BeckDeSerialize<T>(byte[] data)
        {
            return (T)FastSerialize.DeSerialize<T>(data); 
        }
    }
}
