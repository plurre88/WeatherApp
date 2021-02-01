using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VäderApp.Services
{
    public interface IAddDataService
    {
        Task<bool> ConvertAndAdd(IBrowserFile browserFile);
    }
}
