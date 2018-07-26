﻿using System;

namespace VacationsDAL.Interfaces
{
    public interface IRepository<T>: IDisposable
    {
        void Add(T employee);
        T[] Get(Func<T, bool> condition = null);
        T GetById(string id);
        void Remove(string id);
    }
}