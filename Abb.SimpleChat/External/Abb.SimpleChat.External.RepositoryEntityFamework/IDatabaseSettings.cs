using System;
using System.Collections.Generic;
using System.Text;

namespace Abb.SimpleChat.External.RepositoryEntityFamework
{
    public interface IDatabaseSettings
    {
        string ConnnectionString { get; set; }
    }
}
