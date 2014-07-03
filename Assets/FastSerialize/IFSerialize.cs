namespace FSerialize
{
    public interface IFSerialize
    {
        byte[] Serialize();
        void DeSerialize<T>(byte[] data);
        object BeckDeSerialize<T>(byte[] data);
    }
}
