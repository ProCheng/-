using common;
using Dal;
using model;
using System;
using System.Collections.Generic;

namespace dal
{
    public class UnitofWork : IDisposable
    {
        private DBcontent _db = null;
        public UnitofWork()
        {
            this._db = new DBcontent();
        }
        public void Dispose()
        {
            Dispose(true);

        }
        //字典
        private Dictionary<Type, object> Respositorys = new Dictionary<Type, object>();

        public Icrup<T> CreateRespository<T>() where T : modelBase
        {
            Icrup<T> res = null;
            if (Respositorys.ContainsKey(typeof(T)))
            {
                res = Respositorys[typeof(T)] as Icrup<T>;
            }
            else
            {
                res = new Crup<T>(this._db);
                Respositorys.Add(typeof(T), res);
            }
            return res;
        }

        private bool disposeValue = false;  //要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposeValue)
            {
                if (disposing)
                {
                    this._db.Dispose();
                }
                disposeValue = true;
            }
        }

    }
}
