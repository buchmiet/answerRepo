using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Trier4
{
    public interface ILaunchable
    {
        //Task<Answer> TryAsync(Func<CancellationToken, Task<Answer>> method, CancellationToken ct);
        //Task<Answer> Launch(Func<CancellationToken, Task<Answer>> method, CancellationToken ct);
    }
}
