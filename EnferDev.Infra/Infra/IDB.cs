using System;
using System.Data;

namespace EnferDev.Infra.Infra
{
    public interface IDB : IDisposable
    {
        IDbConnection GetCon();
    }
}
