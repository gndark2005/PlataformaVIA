using System.Data;

namespace PlataformaVIA.Data
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
