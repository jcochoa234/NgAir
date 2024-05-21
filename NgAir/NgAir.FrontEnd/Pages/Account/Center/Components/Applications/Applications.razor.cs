using System.Collections.Generic;
using NgAir.FrontEnd.Models;
using Microsoft.AspNetCore.Components;
using AntDesign;

namespace NgAir.FrontEnd.Pages.Account.Center
{
    public partial class Applications
    {
        private readonly ListGridType _listGridType = new ListGridType
        {
            Gutter = 24,
            Column = 4
        };

        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}