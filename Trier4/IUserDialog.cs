using System;
using System.Collections.Generic;
using System.Text;

namespace Trier4
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserDialog
    {
        Task<bool> YesNoAsync(string errorMessage, CancellationToken ct);
     
    }

}
