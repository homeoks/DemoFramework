using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using Entity.Base;
using Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace Repository.BaseRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public T Insert(T value)
        {
            using (var db = new ApplicationDbContext())
            {
                if (value is IAuditEntity auditEntity)
                {
                    auditEntity.TimeCreatedOffset = DateTimeOffset.Now;
                    if (!string.IsNullOrEmpty(StaticUser.UserId))
                        auditEntity.CreateBy = StaticUser.UserId;
                }
                db.Set<T>().Add(value);
                db.SaveChanges();
                return value;
            }
        }

        public List<T> Insert(List<T> values)
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var value in values)
                {
                    if (value is IAuditEntity auditEntity)
                    {
                        auditEntity.TimeCreatedOffset = DateTimeOffset.Now;
                        if (!string.IsNullOrEmpty(StaticUser.UserId))
                            auditEntity.CreateBy = StaticUser.UserId;
                    }
                }
                db.Set<T>().AddRange(values);
                db.SaveChanges();
                return values;
            }
        }

        public T Update(T value, string updateBy)
        {
            using (var db = new ApplicationDbContext())
            {
                if (value is IAuditEntity auditEntity)
                {
                    auditEntity.TimeModifyOffset = DateTimeOffset.Now;
                    auditEntity.ModifyBy = updateBy;
                }
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
                return value;
            }
        }
        public T Update(T value)
        {
            using (var db = new ApplicationDbContext())
            {
                if (value is IAuditEntity auditEntity)
                {
                    auditEntity.TimeModifyOffset = DateTimeOffset.Now;
                    if (!string.IsNullOrEmpty(StaticUser.UserId))
                        auditEntity.ModifyBy = StaticUser.UserId;
                }
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
                return value;
            }
        }

        public T Update(T value, string updateBy, params Expression<Func<T, object>>[] properties)
        {
            using (var db = new ApplicationDbContext())
            {
                if (value is IAuditEntity auditEntity)
                {
                    auditEntity.TimeModifyOffset = DateTimeOffset.Now;
                    auditEntity.ModifyBy = updateBy;
                     if (!string.IsNullOrEmpty(StaticUser.UserId))
                        auditEntity.ModifyBy = StaticUser.UserId;
                }

                db.Entry(value).State = EntityState.Detached;
                db.Attach(value);
                foreach (var pro in properties)
                {
                    db.Entry(value).Property(pro).IsModified = true;
                }
                db.SaveChanges();
                return value;
            }
        }

        public void Delete(object id)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity = db.Set<T>().Find(id);
                switch (entity)
                {
                    case null:
                        return;
                    case IDeleteEntity deleteEntity:
                        if (!string.IsNullOrEmpty(StaticUser.UserId))
                            deleteEntity.DeletedBy = StaticUser.UserId;
                        deleteEntity.IsDeleted = true;
                        deleteEntity.TimeDeletedOffset = DateTimeOffset.Now;
                        db.SaveChanges();
                        return;
                }

                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        public List<T> Gets(Func<T, bool> condition)
        {
            using (var db = new ApplicationDbContext())
            {
                var entities = db.Set<T>().Where(condition)
                    .ToList();
                return entities;
            }
        }

        public T GetById(object id)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity = db.Set<T>().Find(id);
                return entity;
            }
        }

        public T Get(Func<T, bool> condition)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity = db.Set<T>().FirstOrDefault(condition);
                return entity;
            }
        }

        public List<T> Update(List<T> entities, string updateBy, params Expression<Func<T, object>>[] properties)
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var value in entities)
                {
                    var auditEntity = value as IAuditEntity;
                    if (auditEntity != null)
                    {
                        auditEntity.TimeModifyOffset = DateTimeOffset.Now;
                        auditEntity.ModifyBy = updateBy;
                    }

                    db.Entry(value).State = EntityState.Detached;
                    db.Attach(value);
                    foreach (var pro in properties)
                    {
                        db.Entry(value).Property(pro).IsModified = true;
                    }
                }
                db.SaveChanges();
                return entities;
            }
        }
    }
}
