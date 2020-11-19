using common;
using dal;
using model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Dal
{
    public class Crup<T> : Icrup<T> where T : modelBase
    {
        internal DBcontent _db;
        internal DbSet<T> _dbset;
        /* private DbConnection db;*/

        #region 构造函数
        public Crup(DBcontent dbcontent)
        {
            this._db = dbcontent;
            this._dbset = this._db.Set<T>();
        }

        /*public Crup(DbConnection db)
        {
            this.db = db;
        }*/
        #endregion
        #region 添加
        public int Insert(T model)
        {


            this._dbset.Add(model);
            return this._db.SaveChanges();
        }
        public int Insert(List<T> tList)
        {
            this._dbset.AddRange(tList);
            return this._db.SaveChanges();
        }

        #endregion
        #region 删除
        public int Delete(object id)
        {
            this._dbset.Remove(this._dbset.Find(id));
            return this._db.SaveChanges();
        }
        public int BatchDelete(int[] id)
        {
            var students = this._dbset.Where(m => id.Contains(m.Id)).ToList();
            int result = this._dbset.RemoveRange(students).Count();
            return this._db.SaveChanges();
        }

        public int Delete(T model)
        {
            this._dbset.Remove(model);
            return this._db.SaveChanges();
        }
        #endregion
        #region 修改
        public int Update(T model, params string[] noAnend)
        {
            DbEntityEntry entry = this._db.Entry(model);
            entry.State = EntityState.Modified;
            foreach (var item in noAnend)
            {
                entry.Property(item).IsModified = false;
            }
            DbEntityEntry dbEntity = this._db.Entry(model);
            return this._db.SaveChanges();

        }
        #endregion

        #region 查询
        public T GetEntityById(int id)
        {
            return this._dbset.Find(id);
        }

        public T GetEntityFirst(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
                return this._dbset.FirstOrDefault();
            return this._dbset.FirstOrDefault(where);
        }

        public IEnumerable<T> GetEntityList(Expression<Func<T, bool>> where = null, string includes = "")
        {
            
            IQueryable<T> query = this._dbset;
            if (where != null)
                query = query.Where(where);
            foreach (var item in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
            return query;
        }

        public IEnumerable<T> GetEntityListByPage(Expression<Func<T, bool>> where = null, int pageindex = 1, int pageSize = 10)
        {
            IQueryable<T> query = this._dbset;
            if (where != null)
                query = query.Where(where);
            return query.OrderBy(M => M.Id).Skip((pageindex - 1) * pageSize).Take(pageSize);
        }
        #endregion
        public int Getcount()
        {
            return this._dbset.Count();
        }

    }
}
