using Contoso.Apps.SportsLeague.Data.Models;
using System.Collections.Generic;

namespace Contoso.Apps.SportsLeague.Admin.Models
{
    public class HomeModel : BaseModel
    {
        public List<Order> Orders { get; set; }
    }
}