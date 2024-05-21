using System.Collections.Generic;
using NgAir.FrontEnd.Models;
using Microsoft.AspNetCore.Components;

namespace NgAir.FrontEnd.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}