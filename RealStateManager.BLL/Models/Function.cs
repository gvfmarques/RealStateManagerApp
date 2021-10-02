using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.BLL.Models
{
    public class Function : IdentityRole<string>
    {
        public string Description { get; set; }
    }
}
