using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheOurKids.Model
{
    public class WhoWeServeRepository : IWhoWeServeRepository, IDisposable
    {
        private ClotheOurKidsEntities context;

        public WhoWeServeRepository(ClotheOurKidsEntities context)
        {
            this.context = context;
        }

        public IList<Office> GetAllOffices()
        {
            var query = from offices in context.Offices
                        select offices;
            var content = query.ToList<Office>();
            return content;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
