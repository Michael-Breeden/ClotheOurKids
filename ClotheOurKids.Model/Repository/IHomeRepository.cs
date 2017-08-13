using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheOurKids.Model
{
    public interface IHomeRepository : IDisposable
    {
        string CountRequests();

    }
}
