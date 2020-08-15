using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi14_ADONET.Services
{
    public interface IDemo
    {
        Guid GetGuid();
    }

    public class DemoSingleton : IDemo
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }

    public class DemoScoped : IDemo
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }

    public class DemoTransient: IDemo
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }

    public interface IDemoSingleton
    {
        Guid GetGuid();
    }

    public interface IDemoTransient
    {
        Guid GetGuid();
    }

    public interface IDemoScoped
    {
        Guid GetGuid();
    }

    public class DemoAll : IDemoScoped, IDemoSingleton, IDemoTransient
    {
        private Guid _guid;
        public DemoAll()
        {
            _guid = Guid.NewGuid();
        }
        public Guid GetGuid()
        {
            return _guid;
        }
    }
}
