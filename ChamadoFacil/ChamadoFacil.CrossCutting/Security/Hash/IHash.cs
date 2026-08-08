namespace ChamadoFacil.CrossCutting.Security.Hash
{
    public interface IHash
    {
        string Generate(string value);

        bool Verify(string hash, string providedPassword);
    }
}