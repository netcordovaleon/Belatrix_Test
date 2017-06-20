using Belatrix.Plugin.Infraestructure.OperationalManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Infraestructure.DataBase.Infraestructure
{
 
    interface IDatabaseFactory : IDisposable
    {
        BelatrixLoggerContext Get();
    }

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private BelatrixLoggerContext ctx;

        public BelatrixLoggerContext Get()
        {
            return ctx ?? (ctx = new BelatrixLoggerContext());
        }

        protected override void DisposeCore()
        {
            if (ctx != null)
                ctx.Dispose();
        }
 
    }
}
