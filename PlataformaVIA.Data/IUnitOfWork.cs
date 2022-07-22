namespace PlataformaVIA.Data
{
    public interface IUnitOfWork
    {
        void Dispose();
        void SaveChanges();
    }
}
