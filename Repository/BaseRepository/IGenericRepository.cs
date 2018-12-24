using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.BaseRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T Insert(T value);
        List<T> Insert(List<T> values);
        T Update(T value);
        T Update(T value, string updateBy, params Expression<Func<T, object>>[] properties);
        List<T> Update(List<T> value, string updateBy, params Expression<Func<T, object>>[] properties);
        void Delete(object id);
        List<T> Gets(Func<T, bool> condition);
        T GetById(object id);
        T Get(Func<T, bool> condition);
    }
}