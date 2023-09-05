using System;
using System.Collections.Generic;

namespace nba_API.Db.Models;

public partial class TeamPlayer
{
    public int TeamId { get; set; }

    public int PlayerId { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
