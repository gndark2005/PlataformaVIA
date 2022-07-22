namespace PlataformaVIA.Data
{
    using Core.Data.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public abstract class AData<TEntity> where TEntity : class
    {

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            List<TEntity> items = new List<TEntity>();

            try
            {
                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        items.Add(Map<TEntity>(record));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;

        }

        protected TEntidad Map<TEntidad>(IDataRecord record)
        {
            try
            {
                var objT = Activator.CreateInstance<TEntidad>();
                foreach (var property in typeof(TEntidad).GetProperties())
                {
                    if (record.HasColumn(property.Name) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                        property.SetValue(objT, record[property.Name]);


                }
                return objT;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected IEnumerable<T> ToList<T>(IDbCommand command)
        {
            try
            {
                using (var record = command.ExecuteReader())
                {
                    List<T> items = new List<T>();
                    while (record.Read())
                    {
                        items.Add(Map<T>(record));
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
