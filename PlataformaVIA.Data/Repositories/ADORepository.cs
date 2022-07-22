namespace PlataformaVIA.Data.Repositories
{
    using Core.Data;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ADORepository<TEntidad> : IRepository<TEntidad> where TEntidad : class
    {
        protected readonly IUnitOfWork _uow;

        public void Add(TEntidad entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<TEntidad> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntidad> GetAll(string SPName)
        {
            throw new NotImplementedException();
        }

        public TEntidad GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntidad entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntidad entity)
        {
            throw new NotImplementedException();
        }
    }
}
