namespace SL.Factory
{
    public interface IVerificadorVerticalRepository
    {
        void SetDVV(string tabla, string dvv);

        string GetDVV(string tabla);
    }
}