using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheOurKids.Model
{
    public class HomeRepository : IHomeRepository, IDisposable
    {
        private ClotheOurKidsEntities context;

        public HomeRepository(ClotheOurKidsEntities context)
        {
            this.context = context;
        }

        public string CountRequests()
        {
            int requestCount = (from requests in context.Requests
                                select requests).Count();

            requestCount = requestCount + 346;

            return requestCount.ToString();
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
