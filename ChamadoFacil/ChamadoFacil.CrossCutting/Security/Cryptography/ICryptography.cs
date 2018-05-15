namespace ChamadoFacil.CrossCutting.Security.Cryptography
{
    public interface ICryptography
    {
        string Decrypt(string value);

        string Encrypt(string value);

        void SetKey(string key);
    }
}