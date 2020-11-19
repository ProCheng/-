using model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace common
{
    public interface Icrup<T> where T : modelBase
    {
        int Insert(T model);
        int Insert(List<T> tList);
        int Delete(object id);
        int BatchDelete(int[] id);
        int Delete(T model);
        int Update(T model, params string[] noAnend);
        T GetEntityById(int id);
        T GetEntityFirst(Expression<Func<T, bool>> where = null);
        IEnumerable<T> GetEntityList(Expression<Func<T, bool>> where = null, string includes = "");

        IEnumerable<T> GetEntityListByPage(Expression<Func<T, bool>> where = null, int pageindex = 1, int pageSize = 10);
        int Getcount();
    }
}
